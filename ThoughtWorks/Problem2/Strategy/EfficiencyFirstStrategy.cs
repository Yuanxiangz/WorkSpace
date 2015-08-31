using System.Collections.Generic;
using Problem2.Config;
using Problem2.Data;

namespace Problem2.Strategy
{
	public class EfficiencyFirstStrategy : AbstractStrategry
	{
		/// <summary>
		/// Fill bigger time into scheduleList first, until there is no time smaller than [morning/afternoon]TimeLeft
		/// </summary>
		/// <param name="requests"></param>
		/// <returns></returns>
		public override IEnumerable<DaySchedule> Schedule(IList<ScheduleItem> requests)
		{
			if (requests.Count == 0) return null;

			List<DaySchedule> scheduleList = new List<DaySchedule>();

			while (requests.Count > 0)
			{
				DaySchedule daySchedule = new DaySchedule();
				// Schedule morning meeting
				int moringTimeLeft=ConfigurationSetting.MorningDuration;
				for (int i = requests.Count-1; i >=0; i--)
				{
					if (requests[i].ScheduleTime == moringTimeLeft)
					{
						daySchedule.MorningPlan.Add(requests[i]);
						requests.RemoveAt(i);
						break;
					}
					else if (requests[i].ScheduleTime < moringTimeLeft)
					{
						daySchedule.MorningPlan.Add(requests[i]);
						moringTimeLeft -= requests[i].ScheduleTime;
						requests.RemoveAt(i);
					}
				}

				// Schedule afternoon meeting
				int afternoonTimeLeft = ConfigurationSetting.AfternoonDuration;
				for (int i = requests.Count - 1; i >= 0; i--)
				{
					if (requests[i].ScheduleTime == afternoonTimeLeft)
					{
						daySchedule.AfternoonPlan.Add(requests[i]);
						requests.RemoveAt(i);
						break;
					}
					else if (requests[i].ScheduleTime < afternoonTimeLeft)
					{
						daySchedule.AfternoonPlan.Add(requests[i]);
						afternoonTimeLeft -= requests[i].ScheduleTime;
						requests.RemoveAt(i);
					}
				}

				scheduleList.Add(daySchedule);
			}

			return scheduleList;
		}
	}
}
