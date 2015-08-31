using System.Collections.Generic;
using Problem2.Data;
using Problem2.Interface;

namespace Problem2.Strategy
{
	public abstract class AbstractStrategry : IStrategy
	{
		protected IList<ScheduleItem> requests = null;

		public void LoadRequests(IList<ScheduleItem> requests)
		{
			this.requests = requests;
		}

		public IEnumerable<DaySchedule> ScheduleTime()
		{
			List<ScheduleItem> temprequest = new List<ScheduleItem>(requests);
			temprequest.Sort(CompareTime);
			return Schedule(temprequest);
		}

		/// <summary>
		/// Schedule algorithm
		/// </summary>
		/// <param name="requests"></param>
		/// <returns></returns>
		public abstract IEnumerable<DaySchedule> Schedule(IList<ScheduleItem> requests);

		/// <summary>
		/// Sort DESC
		/// </summary>
		/// <param name="item1"></param>
		/// <param name="item2"></param>
		/// <returns></returns>
		private int CompareTime(ScheduleItem item1, ScheduleItem item2)
		{
			if (item1.ScheduleTime > item2.ScheduleTime)
				return 1;
			else
				return -1;
		}
	}
}
