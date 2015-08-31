using System;
using System.Collections.Generic;
using Problem2.Config;
using Problem2.Utils;

namespace Problem2.Data
{
	public class DaySchedule
	{
		private IList<ScheduleItem> morningPlan;
		private IList<ScheduleItem> afternoonPlan;

		public IList<ScheduleItem> MorningPlan
		{
			get { return morningPlan; }
			set { morningPlan = value; }
		}
		public IList<ScheduleItem> AfternoonPlan
		{
			get { return afternoonPlan; }
			set { afternoonPlan = value; }
		}

		public DaySchedule()
		{
			morningPlan = new List<ScheduleItem>();
			afternoonPlan = new List<ScheduleItem>();
		}

		public void Output()
		{
			DateTime startTime = ConfigurationSetting.MorningStartTime;
			foreach (ScheduleItem item in morningPlan)
			{
				Console.WriteLine(string.Format("{0} {1}", startTime.ToString(TextFormatHelper.TIMEFORMAT), item.ToString()));
				startTime = startTime.AddMinutes(item.ScheduleTime);
			}

			startTime = ConfigurationSetting.AfternoonStartTime;
			foreach (ScheduleItem item in afternoonPlan)
			{
				Console.WriteLine(string.Format("{0} {1}", startTime.ToString(TextFormatHelper.TIMEFORMAT), item.ToString()));
				startTime = startTime.AddMinutes(item.ScheduleTime);
			}

			// if meeting end time is before 04:00 PM, networking event will start at 04:00 PM, else it will start immediately
			if (startTime.ToString(TextFormatHelper.TIMEFORMAT).CompareTo(TextFormatHelper.NETWORKINGEVENTSTARTTIME) < 0)
				Console.WriteLine(string.Format("{0} {1}", TextFormatHelper.NETWORKINGEVENTSTARTTIME, TextFormatHelper.NETWORKINGEVENTSTR));
			else
				Console.WriteLine(string.Format("{0} {1}", startTime.ToString(TextFormatHelper.TIMEFORMAT), TextFormatHelper.NETWORKINGEVENTSTR));

			Console.WriteLine();
		}
	}
}
