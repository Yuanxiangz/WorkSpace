using System;
using System.Collections.Generic;
using System.IO;
using Problem2.Config;
using Problem2.Data;
using Problem2.Interface;
using Problem2.Utils;

namespace Problem2.Biz
{
	public class TimeSchedule
	{
		private IStrategy strategry = null;

		#region Public Method
		public TimeSchedule(IStrategy strategry)
		{
			this.strategry = strategry;
		}

		public void LoadRequests(string path)
		{
			List<string> lines = new List<string>();
			using (StreamReader sr = new StreamReader(path))
			{
				while (!sr.EndOfStream)
				{
					lines.Add(sr.ReadLine().Trim());
				}
			}

			strategry.LoadRequests(ParseInput(lines));
		}

		public void ScheduleTime()
		{
			IEnumerable<DaySchedule> scheduleList = strategry.ScheduleTime();
			Output(scheduleList);
		}
		#endregion

		#region Private Method
		private List<ScheduleItem> ParseInput(List<string> lines)
		{
			List<ScheduleItem> requests = new List<ScheduleItem>();
			if (lines.Count <= 0)
				return requests;
			else
			{
				foreach (string line in lines)
				{
					ScheduleItem schedule = new ScheduleItem();
					string[] words = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
					if (words.Length == 1)
					{
						// ignore bad input
						schedule.ScheduleTime = TextFormatHelper.ConvertTimeStrToNum(words[0]);
						if (schedule.ScheduleTime < 0)
							continue;
					}
					else if (words.Length > 1)
					{
						// ignore bad input
						schedule.ScheduleTime = TextFormatHelper.ConvertTimeStrToNum(words[words.Length - 1]);
						if (schedule.ScheduleTime < 0)
							continue;
						else
						{
							schedule.ScheduleName = string.Join(" ", words, 0, words.Length - 1);
						}
					}
					else
						break;

					requests.Add(schedule);
				}
			}

			return requests;
		}

		private void Output(IEnumerable<DaySchedule> scheduleList)
		{
			int dayCount = 1;
			foreach (DaySchedule daySchedule in scheduleList)
			{
				DateTime startTime = ConfigurationSetting.MorningStartTime;
				Console.WriteLine(string.Format("Track {0}:", dayCount));
				daySchedule.Output();
				dayCount++;
			}
		}
		#endregion
	}
}
