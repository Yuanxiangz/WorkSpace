using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Concurrent;

namespace AsyncTest
{
	public class BlockCollectionTest
	{
		static BlockingCollection<object> collection = new BlockingCollection<object>();
		Timer timer;
		static BlockCollectionTest()
		{
			
		}

		public BlockCollectionTest()
		{
			timer = new Timer(Callback);
		}

		private void Callback(object state)
		{ 

		}
	}
}
