using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication1
{
	public class AsyncTest
	{
		public void Start()
		{
			using (StreamWriter sw = new StreamWriter(@"D:\log.txt", false))
			{
				for (int i = 0; i < 10000; i++)
				{
					Process(sw, i);
				}
			}
		}

		public async Task Process(StreamWriter sw, int count)
		{
			sw.WriteLine("ThreadID: " + System.Threading.Thread.CurrentThread.ManagedThreadId.ToString());
			Task task = Task.Factory.StartNew(() =>
				{
					//lock (sw)
					//{
						sw.WriteLine(string.Format("{0} Count: {1}, ThreadID: {2}", DateTime.Now.Millisecond.ToString(), count.ToString(), System.Threading.Thread.CurrentThread.ManagedThreadId.ToString()));
					//}
				});
			await task;
		}
	}
}
