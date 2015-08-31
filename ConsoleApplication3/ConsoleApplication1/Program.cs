using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApplication1
{
	class Program
	{
		static readonly int A = B * 10;
		static readonly int B = 10;
		public static void Main(string[] args)
		{
			volatileTest();

			Console.ReadKey();
		}

		static void constreadonlyTest()
		{
			Console.WriteLine("A is {0},B is {1} ", A, B);
		}

		static void volatileTest()
		{
			VolatileTest test = new VolatileTest();

			Thread th = new Thread(() =>
			{
				while (true)
				{
					test.Wait();
				}
			}
			);
			th.Start();

			Thread th2 = new Thread(() =>
			{
				while (true)
				{
					test.WakeUp();
				}
			}
			);
			th2.Start();
		}
	}
}
