using System;
using Problem2.Config;
using Problem2.Strategy;

namespace Problem2.Biz
{
	public class TimeScheduleWorkflowManager
	{
		private const string UNEXPECTEDFAILURE = "Unexpected error happened!";

		private StrategyFactory strategryFactory;
		private TimeSchedule timeSchedule;

		public TimeScheduleWorkflowManager()
		{
			try
			{
				ConfigurationSetting.Initialize();
				strategryFactory = new StrategyFactory();
				timeSchedule = new TimeSchedule(strategryFactory.Create());
				timeSchedule.LoadRequests(ConfigurationSetting.InputFile);
			}
			catch (Exception ex)
			{
				Console.WriteLine(UNEXPECTEDFAILURE);
				Console.WriteLine(ex);
			}
		}

		public void Run()
		{
			try
			{
				timeSchedule.ScheduleTime();
			}
			catch (Exception ex)
			{
				Console.WriteLine(UNEXPECTEDFAILURE);
				Console.WriteLine(ex);
			}
		}
	}
}
