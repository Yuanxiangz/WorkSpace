using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication5
{
	class Program
	{
		static void Main(string[] args)
		{
			IEnumerable<Item> list = new List<Item>() { 
				new Item(){Name="Tom", Age=12},
				new Item(){Name="Jim", Age=14}
			};

			//list.ForEach((n) => { 
			//	n.Name = "test";
			//});

			//list.ForEach((n) =>
			//{
			//	Console.WriteLine(n.Name);
			//});

			list = list.Select((n) => { n.Name = "test"; return n; });

			foreach (Item item in list)
			{
				Console.WriteLine(item.Name);
			}

			Console.ReadKey();
		}
	}

	public class Item
	{
		public string Name { get; set; }

		public int Age { get; set; }
	}
}
