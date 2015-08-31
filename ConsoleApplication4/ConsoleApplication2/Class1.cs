using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
	public class Class1
	{
		static int count = 0;

		public Task Initialization { get; private set; }

		public async Task Test()
		{
			await Task.Delay(4000);
			count++;
			Console.WriteLine(count);
		}

		public async void Run()
		{
			Console.WriteLine("before ThreadID: " + System.Threading.Thread.CurrentThread.ManagedThreadId);
			//if (Initialization != null)
			//{
			//	await Initialization;
			//}

			//Initialization = Test();
			//await Initialization;

			await Test();
			Console.WriteLine("after ThreadID: " + System.Threading.Thread.CurrentThread.ManagedThreadId);
		}
	}
}
