using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
	public class Class1
	{
		public Class1()
		{
			List = new List<string>();
		}

		public Class1(string a)
			: this()
		{
			List.Add(a);
		}

		public List<string> List { get; set; }
	}
}
