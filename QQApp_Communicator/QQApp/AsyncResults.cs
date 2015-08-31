using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Concurrent;

namespace QQApp
{
	public class AsyncResults : BlockingCollection<object>
	{
		private Guid _id;

		public AsyncResults(Guid requestId)
		{
			_id = requestId;
		}

		public Guid Id
		{
			get { return _id; }
		}
	}
}
