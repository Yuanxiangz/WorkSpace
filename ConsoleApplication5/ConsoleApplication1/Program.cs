using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	class Program
	{
		static int count = 0;
		static object lockobj = new object();
		static void Main(string[] args)
		{
			Console.WriteLine("Inprogress!");
			Console.ForegroundColor = ConsoleColor.Green;

			ThreadPool.QueueUserWorkItem(new WaitCallback(Work));
			ThreadPool.QueueUserWorkItem(new WaitCallback(Work));
			ThreadPool.QueueUserWorkItem(new WaitCallback(Work));
			ThreadPool.QueueUserWorkItem(new WaitCallback(Work));

			Console.ReadKey();
		}

		static void Work(object obj)
		{
			while (true)
			{
				lock (lockobj)
				{
					Console.SetCursorPosition(0, 1);
					Console.Write("{0}%", ++count);
					//Thread.Sleep(200);
				}
			}
		}
	}
}
