using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication2.A;
using System.Collections.Concurrent;

namespace ConsoleApplication1
{
	class Program
	{
		static void Main(string[] args)
		{
			test3();

			Console.ReadKey();
		}

		static void test1()
		{
			ConcurrentDictionary<string, Item> dic = new ConcurrentDictionary<string, Item>();
			dic.TryAdd("1", new Item(){Name="a"});

			test2(dic);

			Console.WriteLine();
		}

		static ConcurrentDictionary<string, Item> test2(ConcurrentDictionary<string, Item> newData)
		{
			ConcurrentDictionary<string, Item> dic = new ConcurrentDictionary<string, Item>();
			dic.TryAdd("1", new Item(){Name="b"});
			foreach (KeyValuePair<string, Item> pair in dic)
			{
				newData.AddOrUpdate(pair.Key, pair.Value, (k, v) =>
					{
						return pair.Value;
					});
			}

			return dic;
		}

		static void test3()
		{
			ConcurrentDictionary<string, string> list = new ConcurrentDictionary<string, string>();
			list["a"]="a";
			list["b"] = "b";
			Console.WriteLine(list.Count);
		}
	}

	public class Item
	{
		public string Name { get; set; }
	}
}
