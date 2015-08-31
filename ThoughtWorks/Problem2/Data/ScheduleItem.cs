using Problem2.Utils;

namespace Problem2.Data
{
	public struct ScheduleItem
	{
		public string ScheduleName;
		public int ScheduleTime;

		public override string ToString()
		{
			string time;
			if (ScheduleTime == TextFormatHelper.LIGHTNING_MINS)
				time = TextFormatHelper.LIGHTNING;
			else
				time = ScheduleTime.ToString() + TextFormatHelper.TIMESUFFIX;

			return string.Format("{0} {1}", ScheduleName, time);
		}
	}
}
