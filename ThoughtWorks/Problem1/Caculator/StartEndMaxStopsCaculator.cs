using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Problem1.Interface;
using System.Text.RegularExpressions;
using Problem1.Data;

namespace Problem1.Caculator
{
	/// <summary>
	/// Caculate follow route
	/// 6.	The number of trips starting at C and ending at C with a maximum of 3 stops.  In the sample data below, there are two such trips: C-D-C (2 stops). and C-E-B-C (3 stops).
	/// </summary>
	public class StartEndMaxStopsCaculator : ICaculator
	{
		private const string STARTPOINTREG = @"starting\sat\s(?<stop>[A-Za-z]+)\s";
		private const string ENDPOINTREG = @"ending\sat\s(?<stop>[A-Za-z]+)\s";
		private const string MAXSTOPSNUM = @"maximum\sof\s(?<maxnum>\d+)\sstops";

		private string startPoint = string.Empty;
		private string endPoint = string.Empty;
		private int maxStopsNum = 0;

		// Return how many routes there are
		private int routesCount = 0;

		private IDictionary<MultplePrimKey, int> input;

		public int Caculate()
		{
			if (maxStopsNum == 0)
				return -1;

			CaculateRouteCount(startPoint, 0);

			return routesCount == 0 ? -1 : routesCount;
		}

		public void LoadQuestionAndInput(string question, IDictionary<Data.MultplePrimKey, int> input)
		{
			ParseInput(question);
			this.input = input;
		}

		private void ParseInput(string question)
		{
			Match match = Regex.Match(question, STARTPOINTREG);
			if (match != null)
			{
				startPoint = match.Groups["stop"].Value;
			}

			match = Regex.Match(question, ENDPOINTREG);
			if (match != null)
			{
				endPoint = match.Groups["stop"].Value;
			}

			match = Regex.Match(question, MAXSTOPSNUM);
			if (match != null)
			{
				int.TryParse(match.Groups["maxnum"].Value, out maxStopsNum);
			}
		}

		private void CaculateRouteCount(string start, int num)
		{
			if (num == maxStopsNum)
				return;
			else
			{
				num++;
				foreach (MultplePrimKey road in input.Keys)
				{
					if (start.Equals(road.Key1) && endPoint.Equals(road.Key2))
					{
						routesCount++;
						return;
					}

					if (road.Key1.Equals(start))
						CaculateRouteCount(road.Key2, num);
				}
			}
		}
	}
}
