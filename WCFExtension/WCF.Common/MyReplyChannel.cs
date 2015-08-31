using System;
using System.ServiceModel.Channels;

namespace WCF.Common
{
	public class MyReplyChannel : ChannelBase, IReplyChannel
	{
		private IReplyChannel InnerChannel
		{ get; set; }

		public MyReplyChannel(ChannelManagerBase channelManager, IReplyChannel innerChannel)
			: base(channelManager)
		{
			this.InnerChannel = innerChannel;
		}

		#region ChannelBase Members
		protected override void OnAbort()
		{
			Console.WriteLine("MyReplyChannel.OnAbort()");
			this.InnerChannel.Abort();
		}

		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			Console.WriteLine("MyReplyChannel.OnBeginClose()");
			return this.InnerChannel.BeginClose(timeout, callback, state);
		}

		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			Console.WriteLine("MyReplyChannel.OnBeginOpen()");
			return this.InnerChannel.BeginOpen(timeout, callback, state);
		}

		protected override void OnClose(TimeSpan timeout)
		{
			Console.WriteLine("MyReplyChannel.OnClose()");
			this.Close(timeout);
		}

		protected override void OnEndClose(IAsyncResult result)
		{
			Console.WriteLine("MyReplyChannel.OnEndClose()");
			this.InnerChannel.EndClose(result);
		}

		protected override void OnEndOpen(IAsyncResult result)
		{
			Console.WriteLine("MyReplyChannel.OnEndOpen()");
			this.InnerChannel.EndOpen(result);
		}

		protected override void OnOpen(TimeSpan timeout)
		{
			Console.WriteLine("MyReplyChannel.OnOpen()");
			this.InnerChannel.Open(timeout);
		}
		#endregion

		#region IReplyChannel
		public System.IAsyncResult BeginReceiveRequest(System.TimeSpan timeout, System.AsyncCallback callback, object state)
		{
			Console.WriteLine("MyReplyChannel.BeginReceiveRequest(3)");
			return InnerChannel.BeginReceiveRequest(timeout, callback, state);
		}

		public System.IAsyncResult BeginReceiveRequest(System.AsyncCallback callback, object state)
		{
			Console.WriteLine("MyReplyChannel.BeginReceiveRequest(2)");
			return InnerChannel.BeginReceiveRequest(callback, state);
		}

		public System.IAsyncResult BeginTryReceiveRequest(System.TimeSpan timeout, System.AsyncCallback callback, object state)
		{
			Console.WriteLine("MyReplyChannel.BeginTryReceiveRequest()");
			return InnerChannel.BeginTryReceiveRequest(timeout, callback, state);
		}

		public System.IAsyncResult BeginWaitForRequest(System.TimeSpan timeout, System.AsyncCallback callback, object state)
		{
			Console.WriteLine("MyReplyChannel.BeginWaitForRequest()");
			return InnerChannel.BeginWaitForRequest(timeout, callback, state);
		}

		public RequestContext EndReceiveRequest(System.IAsyncResult result)
		{
			Console.WriteLine("MyReplyChannel.EndReceiveRequest()");
			return InnerChannel.EndReceiveRequest(result);
		}

		public bool EndTryReceiveRequest(System.IAsyncResult result, out RequestContext context)
		{
			Console.WriteLine("MyReplyChannel.EndTryReceiveRequest()");
			return InnerChannel.EndTryReceiveRequest(result, out context);
		}

		public bool EndWaitForRequest(System.IAsyncResult result)
		{
			Console.WriteLine("MyReplyChannel.EndWaitForRequest()");
			return InnerChannel.EndWaitForRequest(result);
		}

		public System.ServiceModel.EndpointAddress LocalAddress
		{
			get
			{
				Console.WriteLine("MyReplyChannel.LocalAddress");
				return InnerChannel.LocalAddress;
			}
		}

		public RequestContext ReceiveRequest(System.TimeSpan timeout)
		{
			Console.WriteLine("MyReplyChannel.ReceiveRequest(1)");
			return InnerChannel.ReceiveRequest(timeout);
		}

		public RequestContext ReceiveRequest()
		{
			Console.WriteLine("MyReplyChannel.ReceiveRequest(0)");
			return InnerChannel.ReceiveRequest();
		}

		public bool TryReceiveRequest(System.TimeSpan timeout, out RequestContext context)
		{
			Console.WriteLine("MyReplyChannel.TryReceiveRequest()");
			return InnerChannel.TryReceiveRequest(timeout, out context);
		}

		public bool WaitForRequest(System.TimeSpan timeout)
		{
			Console.WriteLine("MyReplyChannel.WaitForRequest()");
			return InnerChannel.WaitForRequest(timeout);
		}
		#endregion
	}
}
