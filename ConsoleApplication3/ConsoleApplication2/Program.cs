using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
	public class VolatileObjectTest
	{
		public static void Main(String[] args)
		{
			VolatileObjectTest volObj = new VolatileObjectTest();
			Thread t2 = new Thread(
				() =>
				{
					Console.WriteLine("t1 start");
					for (; ; )
					{
						volObj.waitToExit();
					}
				}
			);
			t2.Start();
			Thread t1 = new Thread(
				() =>
				{
					Console.WriteLine("t2 start");
					for (; ; )
					{
						volObj.swap();
					}
				}
			);
			t1.Start();
		}

		bool boolValue;

		public void waitToExit()
		{
			if (boolValue == !boolValue)
			{
				Console.WriteLine("Interupt!!!");
			}
		}

		public void swap()
		{
			try
			{
				Thread.Sleep(100);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}

			boolValue = !boolValue;
			Console.WriteLine(boolValue);
		}
	}
}
