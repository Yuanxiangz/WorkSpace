using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
	public class Class1
	{
		public string Pro { get; set; }

		private string aaa(TestItem item)
		{
			Pro = item.Name;
			return item.Desc;
		}

		private void ttt()
		{
			Pro = "ttt";
		}
	}

	public class TestItem
	{
		public string Name { get; set; }

		public string Desc { get; set; }
	}
}
