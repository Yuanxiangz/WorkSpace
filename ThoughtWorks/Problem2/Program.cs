using System;
using Problem2.Biz;

namespace Problem2
{
	class Program
	{
		static void Main(string[] args)
		{
			TimeScheduleWorkflowManager scheduleManager = new TimeScheduleWorkflowManager();
			scheduleManager.Run();

			Console.ReadKey();
		}
	}
}
