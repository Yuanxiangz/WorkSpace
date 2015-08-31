using System.Collections.Generic;
using Problem2.Data;

namespace Problem2.Interface
{
	public interface IStrategy
	{
		void LoadRequests(IList<ScheduleItem> requests);

		IEnumerable<DaySchedule> ScheduleTime();
	}
}
