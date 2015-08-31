using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApplication1
{
	public class VolatileTest
	{
		public bool flag;

		public void Wait()
		{
			flag = false;
			if (flag)
				Console.WriteLine("volatile success!");
		}

		public void WakeUp()
		{
			Thread.Sleep(100);
			flag = true;
			Console.WriteLine(System.Threading.Thread.CurrentThread.Name + " wakeup other thread.");
		}
	}
}
