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
	public class StartEndShortestCaculator : ICaculator
	{
		private const string FROMTOREG = @"from\s(?<start>[A-Za-z]+)\sto\s(?<end>[A-Za-z]+)\.";

		private string startPoint = string.Empty;
		private string endPoint = string.Empty;
		private int shortestRouteLength = 0;
		private Dictionary<string, int> remainingCollection = new Dictionary<string, int>();
		private Dictionary<string, int> shortestCollection = new Dictionary<string, int>();

		private IDictionary<MultplePrimKey, int> input;

		public int Caculate()
		{
			Dijkstra(startPoint);
			if (startPoint.Equals(endPoint))
			{
				int shortest = -1;

				foreach (MultplePrimKey key in input.Keys)
				{
					if (key.Key2.Equals(endPoint))
					{
						if ((shortest == -1 || shortest > (shortestCollection[key.Key1] + input[key])) &&
							shortestCollection.Keys.Contains(key.Key1))
						{
							shortest = shortestCollection[key.Key1] + input[key];
						}
					}
				}

				return shortest;
			}

			return shortestCollection[endPoint] == 0 ? -1 : shortestCollection[endPoint];
		}

		public void LoadQuestionAndInput(string question, IDictionary<MultplePrimKey, int> input)
		{
			ParseInput(question);
			this.input = input;

			shortestCollection.Add(startPoint, 0);
			GetRemainingCollection(startPoint);
		}

		#region Private Method
		private void ParseInput(string question)
		{
			Match match = Regex.Match(question, FROMTOREG);
			if (match != null)
			{
				startPoint = match.Groups["start"].Value;
				endPoint = match.Groups["end"].Value;
			}
		}

		private void GetRemainingCollection(string start)
		{
			foreach (MultplePrimKey key in input.Keys)
			{
				if (key.Key1.Equals(start) && !remainingCollection.Keys.Contains(key.Key2))
				{
					remainingCollection.Add(key.Key2, -1);
					GetRemainingCollection(key.Key2);
				}
			}
		}

		private void Dijkstra(string start)
		{
			foreach (MultplePrimKey key in input.Keys)
			{
				if (start.Equals(key.Key1) &&
					remainingCollection.Keys.Contains(key.Key2) &&
					!shortestCollection.Keys.Contains(key.Key2))
				{
					if (remainingCollection[key.Key2] == -1 || remainingCollection[key.Key2] > (shortestCollection[key.Key1] + input[key]))
					{
						remainingCollection[key.Key2] = shortestCollection[key.Key1] + input[key];
					}
				}
			}

			PointItem nextPoint = GetMinimunLengthPoint();
			if (nextPoint == null) return;

			shortestCollection.Add(nextPoint.Point, nextPoint.Distance);
			remainingCollection.Remove(nextPoint.Point);

			Dijkstra(nextPoint.Point);
		}

		private PointItem GetMinimunLengthPoint()
		{
			int min = -1;
			string point = string.Empty;
			foreach (string s in shortestCollection.Keys)
			{
				foreach (MultplePrimKey key in input.Keys)
				{
					if (s.Equals(key.Key1) &&
						remainingCollection.Keys.Contains(key.Key2) &&
						!shortestCollection.Keys.Contains(key.Key2) &&
						remainingCollection[key.Key2] != -1)
					{
						if (min == -1 || min > remainingCollection[key.Key2])
						{
							min = remainingCollection[key.Key2];
							point = key.Key2;
						}
					}
				}
			}

			PointItem item = new PointItem() { Distance = min, Point = point };
			return (point == string.Empty ? null : item);
		}
		#endregion
	}

	public class PointItem
	{
		public int Distance;
		public string Point;
	}
}
