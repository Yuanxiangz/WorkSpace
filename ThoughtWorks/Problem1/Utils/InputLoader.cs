using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Problem1.Data;

namespace Problem1.Utils
{
	public class InputLoader
	{
		public static IList<string> LoadQuestions(string path)
		{
			List<string> questions = new List<string>();
			using(StreamReader sr=new StreamReader(path))
			{
				while(!sr.EndOfStream)
				{
					string line = sr.ReadLine();
					if (!line.Equals(string.Empty))
						questions.Add(line);
				}
			}

			return questions;
		}

		public static IDictionary<MultplePrimKey, int> LoadInputs(string path)
		{
			Dictionary<MultplePrimKey, int> roads = new Dictionary<MultplePrimKey, int>();
			using (StreamReader sr = new StreamReader(path))
			{
				string line = sr.ReadToEnd();
				if (line.Equals(string.Empty)) return roads;
				else
				{
					line = line.Replace("Graph:", "");
					string[] paths = line.Split(new char[]{',', ' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);
					for (int i = 0; i < paths.Length;i++ )
					{
						MultplePrimKey key = new MultplePrimKey();
						char[] items = paths[i].ToCharArray();
						if (items.Length == 3)
						{
							key.Key1 = items[0].ToString();
							key.Key2 = items[1].ToString();
							int value;
							if(int.TryParse(items[2].ToString(), out value))
								roads.Add(key, value);
							else
								throw new Exception(ConstStr.BADINPUTGRAPHERROR);
						}
						else
						{
							throw new Exception(ConstStr.BADINPUTGRAPHERROR);
						}
					}
				}
			}

			return roads;
		}
	}
}
