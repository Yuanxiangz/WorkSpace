using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ECS.Common.Transport;
using ECS.Common.Transport.ServerProxy;
using Eze.Infrastructure.Core.Threading;
using Action = System.Action;

namespace QQApp
{
	/// <summary>
	/// Provides code to map callbacks to their appropriate data type. 
	/// </summary>
	/// <remarks>If a data type implements IFaulted it is handled specially. This is the mechanism provided
	/// to pass exception information from the service back to the client.</remarks>
	public abstract class ServiceProxyBase
	{
		private CallbackBase callBack; // this would be registered on a background thread with net connection mgr 
		protected delegate TResponse SvcGatewayDelegate<TRequest, TResponse>(TRequest request)
			where TRequest : ICommonRequest
			where TResponse : ICommonResponse;

		public ServiceProxyBase(CallbackBase callbackBase)
		{
			callBack = callbackBase;
		}

		/// <summary>
		/// Provides a synchronous wait version of a server request and callback. 
		/// If the caller thread is UI thread, it executes asynchronously.
		/// If the caller thread is not UI thread, it blocks the caller thread.
		/// By Default it times out after 2 minutes.
		/// </summary>
		/// <typeparam name="TResponse"></typeparam>
		/// <typeparam name="TRequest"></typeparam>
		/// <param name="request"></param>
		/// <param name="svcGatewayMethod"></param>
		/// <returns></returns>
		protected TResponse ServerRequest<TResponse, TRequest>(TRequest request, SvcGatewayDelegate<TRequest, TResponse> svcGatewayMethod)
			where TResponse : ServiceResponse
			where TRequest : ICommonRequest
		{
			return ServerRequest(request, svcGatewayMethod, TimeSpan.FromMinutes(2));
		}

		/// <summary>
		/// Provides a synchronous wait version of a server request and callback. 
		/// If the caller thread is UI thread, it executes asynchronously.
		/// If the caller thread is not UI thread, it blocks the caller thread.
		/// </summary>
		/// <typeparam name="T">The type expected via callback.</typeparam>
		/// <typeparam name="TResponse"></typeparam>
		/// <typeparam name="TRequest"></typeparam>
		/// <param name="request"></param>
		/// <param name="svcGatewayMethod"></param>
		/// <param name="timeout">The time to wait before giving up on the service response.</param>
		/// <returns></returns>
		/// <remarks>Note that this does not currently support progress reporting. If the service provides progress 
		/// reports this will need to be amended.</remarks>
		protected TResponse ServerRequest<TResponse, TRequest>(TRequest request, SvcGatewayDelegate<TRequest, TResponse> svcGatewayMethod, TimeSpan timeout)
			where TResponse : ServiceResponse
			where TRequest : ICommonRequest
		{
			AsyncResults asyncResults = AddToResponseQueue(request.RequestID);

			TimeSpan defaultTimeout = TimeSpan.FromMinutes(2);
			if (defaultTimeout < timeout)
				timeout = defaultTimeout;
			if (UIThreadSingleton.IsCreated && UIThreadSingleton.IsCallFromUIThread)
			{
				Task<TResponse> callCompleted = TaskFactoryHelper<TResponse>.StartNew(
					() => ExecServerRequest(request, svcGatewayMethod, timeout, asyncResults), BackgroundTaskScheduler.OnBackground());
				while (!callCompleted.IsFinishedRunning())
					Application.DoEvents();

				if (callCompleted.Status != TaskStatus.RanToCompletion)
				{
					if (callCompleted.Exception != null)
					{
						AggregateException aggregateException = callCompleted.Exception.Flatten();
						if (aggregateException.InnerException != null)
							throw aggregateException.InnerException;
					}
				}
				return callCompleted.Result;
			}
			else
			{
				return ExecServerRequest(request, svcGatewayMethod, timeout, asyncResults);
			}
		}

		private TResponse ExecServerRequest<TResponse, TRequest>(TRequest request,
			SvcGatewayDelegate<TRequest, TResponse> svcGatewayMethod, TimeSpan timeout, AsyncResults asyncResults)
			where TResponse : ServiceResponse
			where TRequest : ICommonRequest
		{
			TResponse response = svcGatewayMethod(request);
			if (response.ResponseCode != ResponseCode.Succeeded)
				return response;
			object serviceResponse;
			if (asyncResults.TryTake(out serviceResponse, timeout))
			{
				if (serviceResponse is IFaulted && ((IFaulted)serviceResponse).IsFaulted)
					throw new AggregateException(((IFaulted)serviceResponse).Message);
				response = (TResponse)serviceResponse;
				if (response != null && response.ResponseCode != ResponseCode.Succeeded &&
					!string.IsNullOrWhiteSpace(response.ErrorMessage))
					throw new ServerException(response.ErrorMessage);
				return response;
			}
			string timeOutString = TimeSpan.FromMinutes(2) > timeout ? timeout.TotalSeconds + "seconds" : "2 minutes";
			throw new TimeoutException("We did not receive of reply from the server after " + timeOutString +
									   " for transaction " + asyncResults.Id);
		}

		/// <summary>
		/// Wires up the given callbacks with the internal implementation of network callbacks. Note this should be called only once
		/// per asyncResult, otherwise data will be lost.
		/// </summary>
		/// <typeparam name="TProgress">The type of data contract that is returned to show progress.</typeparam>
		/// <typeparam name="TResult">The type of data contract returned when the operation completes or fails.</typeparam>
		/// <param name="userCallback">The callback to wire up.</param>
		/// <param name="asyncResults">The <see cref="AsyncResults"/> associated with the current call.</param>
		protected void SetupCallback<TProgress, TResult>(Callback<TProgress, TResult> userCallback, AsyncResults asyncResults)
		{
			ProgressReportingTask<TResult, TProgress> responseWithProgress = GetAsynchronousResponseWithProgress<TResult, TProgress>(asyncResults, userCallback.CancellationToken);
			((Task)responseWithProgress).OnCancelled(
					canceled =>
					{
						CancelTransaction(asyncResults.Id);
						userCallback.OnCanceled();
					}, userCallback.CanceledScheduler); // bug here - not being called on cancel
			Task<TResult> result = responseWithProgress.OnProgress(
				progressData => userCallback.OnProgress(progressData),
				userCallback.ProgressScheduler);
			result.Pipeline(
				finished =>
				{
					if (finished.Result is IFaulted && ((IFaulted)finished.Result).IsFaulted)
						throw new AggregateException(((IFaulted)finished.Result).Message);

					userCallback.OnCompleted(finished.Result);
				}, userCallback.CompletedScheduler)
				.OnError(
					exception => userCallback.OnError(exception)
						, userCallback.ErrorScheduler);
		}

		/// <summary>
		/// Wires up the given callbacks with the internal implementation of network callbacks. Note this should be called only once
		/// per asyncResult, otherwise data will be lost.
		/// </summary>
		/// <typeparam name="TResult">The type of data contract returned when the operation completes or fails.</typeparam>
		/// <param name="userCallback">The callback to wire up.</param>
		/// <param name="asyncResults">The <see cref="AsyncResults"/> associated with the current call.</param>
		protected void SetupCallback<TResult>(Callback<TResult> userCallback, AsyncResults asyncResults)
		{
			Task<TResult> result = GetAsynchronousResponse<TResult>(asyncResults, userCallback.CancellationToken);
			result.Pipeline(
				finished => userCallback.OnCompleted(finished.Result),
				userCallback.CompletedScheduler).OnCancelled(
					canceled =>
					{
						CancelTransaction(asyncResults.Id);
						userCallback.OnCanceled();
					}, // bug here - not being called on cancel
				userCallback.CanceledScheduler).OnError(
					exception => userCallback.OnError(exception)
						, userCallback.ErrorScheduler); // bug here - not being called on cancel*/
		}


		/// <summary>
		/// Provides error handling for <paramref name="actionToRetry"/>. It will call <paramref name="actionToRetry"/>
		/// at least once and at most <paramref name="retryCount"/>. It handles errors if the calling
		/// the action throws exceptions or if the callback for the action contains errors.
		/// </summary>
		/// <typeparam name="P"></typeparam>
		/// <typeparam name="T"></typeparam>
		/// <param name="userCallback"></param>
		/// <param name="actionToRetry"></param>
		/// <param name="retryCount"></param>
		protected void SetupRetryOnError<P, T>(Callback<P, T> userCallback, Action actionToRetry, int retryCount)
		{
			int retries = retryCount; // capture this in the closure below
			List<Exception> exceptions = null;
			bool handled = false;
			Action<Exception> previousHandler = userCallback.OnError; // one potential way to implement retry
			userCallback.OnError =
				exception =>
				{
					if (exception != null)
						exceptions.Add(exception);
					bool exceptionThrown;
					do
					{
						exceptionThrown = false;
						if (retries > 0)
						{
							try
							{
								actionToRetry();
							}
							catch (Exception e)
							{
								if (exceptions == null)
									exceptions = new List<Exception>();
								exceptions.Add(e);
								exceptionThrown = true;
							}
							retries--;
						}
					} while (exceptionThrown);

					if (retries <= 0 && !handled)
					{
						handled = true;
						previousHandler(new AggregateException(exceptions));
					}
				};
			userCallback.OnError(null);
		}

		/// <summary>
		/// Setup retry logic for a synchronous workflow. 
		/// </summary>
		/// <param name="actionToRetry"></param>
		/// <param name="retryCount"></param>
		protected void SetupRetryOnError(Action actionToRetry, int retryCount)
		{
			Callback<object, object> callback = new Callback<object, object>();
			callback.OnError = exception => { throw exception; };
			SetupRetryOnError(callback, actionToRetry, retryCount);
		}

		/// <summary>
		/// Allows an implementation of canceling for transactions.
		/// </summary>
		/// <param name="id"></param>
		protected virtual void CancelTransaction(Guid id)
		{

		}

		private ProgressReportingTask<TResult, TProgress> GetAsynchronousResponseWithProgress<TResult, TProgress>(AsyncResults taskCompletionSource, CancellationToken cancellationToken)
		{
			ProgressReportingTask<TResult, TProgress> progressReportingTask = new ProgressReportingTask<TResult, TProgress>(
				progress =>
				{
					object obj;
					while (taskCompletionSource.TryTake(out obj, (int)TimeSpan.FromMinutes(2).TotalMilliseconds, cancellationToken))
					{
						progress.MakeProgress((TProgress)obj);
						if (obj is TProgress)
							progress.MakeProgress((TProgress)obj);
						else if (obj is TResult)
							return (TResult)obj;
						else throw new ArgumentException("Cannot process result type of " + taskCompletionSource.GetConsumingEnumerable(cancellationToken).First().GetType());
					}
					throw new AbandonedMutexException("We did not receive of reply from the server after 2 minutes for transaction " + taskCompletionSource.Id);
				}, cancellationToken, TaskCreationOptions.None);
			progressReportingTask.Start(BackgroundTaskScheduler.OnBackground());
			Task tt = progressReportingTask;
			tt.OnCancelled(
					canceled =>
					{
						Trace.WriteLine("Got some more cancels");
					}, UITaskScheduler.InvokeAsync()); // bug here - not being called on cancel
			return progressReportingTask;
		}

		private Task<TResult> GetAsynchronousResponse<TResult>(AsyncResults taskCompletionSource, CancellationToken cancellationToken)
		{
			return TaskFactoryHelper<TResult>.StartNew(
				() =>
				{
					object obj;
					while (!(taskCompletionSource.TryTake(out obj, (int)TimeSpan.FromMinutes(2).TotalMilliseconds, cancellationToken)))
					{
						if (obj is TResult)
							return (TResult)obj;
					}
					throw new AbandonedMutexException("We did not receive of reply from the server after 2 minutes for transaction " + taskCompletionSource.Id);
				}, BackgroundTaskScheduler.OnBackground(), cancellationToken);
		}

		protected AsyncResults AddToResponseQueue(Guid requestId)
		{
			AsyncResults asyncResults = new AsyncResults(requestId);
			callBack.AddPendingTask(asyncResults);
			return asyncResults;
		}
	}
}
