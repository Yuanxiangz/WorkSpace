using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace QQApp
{
	public class CallbackBase
	{
		public Dictionary<Guid, BlockingCollection<object>> tasks = new Dictionary<Guid, BlockingCollection<object>>();

		protected void OnResult(Guid transactionId, object returnValue)
		{
			lock (tasks)
			{
				if (!tasks.ContainsKey(transactionId))
					throw new ArgumentException(
						"Cannot find a task with transaction id: + " + transactionId, "transactionId");
				tasks[transactionId].Add(returnValue);
			}
		}

		public void AddPendingTask(AsyncResults outstandingTask)
		{
			lock (tasks)
			{
				if (tasks.ContainsKey(outstandingTask.Id))
					throw new ArgumentException(
						"Cannot add a second task with the same transactionID as a previous one: + " + outstandingTask.Id, "transactionId");

				tasks.Add(outstandingTask.Id, outstandingTask);
			}
		}
	}
}
