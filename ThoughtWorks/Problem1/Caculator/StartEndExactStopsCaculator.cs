using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Problem1.Data;
using Problem1.Interface;

namespace Problem1.Caculator
{
	/// <summary>
	/// Caculate following routes
	/// 7.	The number of trips starting at A and ending at C with exactly 4 stops.  In the sample data below, there are three such trips: A to C (via B,C,D); A to C (via D,C,D); and A to C (via D,E,B).
	/// </summary>
	public class StartEndExactStopsCaculator : ICaculator
	{
		private const string STARTPOINTREG = @"starting\sat\s(?<stop>[A-Za-z]+)\s";
		private const string ENDPOINTREG = @"ending\sat\s(?<stop>[A-Za-z]+)\s";
		private const string EXACTSTOPSNUM = @"exactly\s(?<exactnum>\d+)\sstops";

		private string startPoint = string.Empty;
		private string endPoint = string.Empty;
		private int exactStopsNum = 0;

		// Return how many routes there are
		private int routesCount = 0;

		private IDictionary<MultplePrimKey, int> input;

		public int Caculate()
		{
			if (exactStopsNum == 0)
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

			match = Regex.Match(question, EXACTSTOPSNUM);
			if (match != null)
			{
				int.TryParse(match.Groups["exactnum"].Value, out exactStopsNum);
			}
		}

		private void CaculateRouteCount(string start, int num)
		{
			if (num == exactStopsNum)
				return;
			else
			{
				num++;
				foreach (MultplePrimKey road in input.Keys)
				{
					if ((num + 1) == exactStopsNum && start.Equals(road.Key1) && endPoint.Equals(road.Key2))
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
