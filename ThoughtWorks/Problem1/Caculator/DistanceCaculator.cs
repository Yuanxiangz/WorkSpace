using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Problem1.Data;
using Problem1.Interface;

namespace Problem1.Caculator
{
	/// <summary>
	/// Caculate distance of certain route
	/// eg. 1.	The distance of the route A-B-C.
	/// </summary>
	public class DistanceCaculator : ICaculator
	{
		private IDictionary<MultplePrimKey, int> input;
		private List<MultplePrimKey> road = new List<MultplePrimKey>();

		public void LoadQuestionAndInput(string question, IDictionary<MultplePrimKey, int> input)
		{
			road = ParseQuestion(question);
			this.input = input;
		}

		public int Caculate()
		{
			if (road == null || road.Count == 0)
			{
				return -1;
			}
			else
			{
				int distance = 0;
				foreach(MultplePrimKey key in road)
				{
					if (input.Keys.Contains(key))
						distance += input[key];
					else
					{
						return -1;
					}
				}

				return distance;
			}
		}

		private List<MultplePrimKey> ParseQuestion(string question)
		{
			List<MultplePrimKey> list = new List<MultplePrimKey>();
			string[] tempArray = question.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
			if (tempArray.Length == 0) return list;

			string lastStr = tempArray[tempArray.Length - 1].Trim(new char[] { '.' });
			string[] stops = lastStr.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
			if (stops.Length < 2)
				return list;

			for (int i = 0; i < stops.Length - 1; i++)
			{
				MultplePrimKey key = new MultplePrimKey() { Key1 = stops[i], Key2 = stops[i + 1] };
				list.Add(key);
			}

			return list;
		}
	}
}
