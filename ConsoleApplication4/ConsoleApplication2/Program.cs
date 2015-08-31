using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
	class Program
	{
		static void Main(string[] args)
		{
			//Class1 c1 = new Class1();
			//for (int i = 0; i < 100;i++ )
			//{
			//	c1.Run();
			//}

			Child2 c = new Child2();
			c.Output();

			BindingFlags flags = BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
			PropertyInfo hl = typeof(Child1).GetProperty("Name", flags);
			if (hl != null)
			{
				object itemInfo = hl.GetValue(c, null);
				itemInfo = null;
			}

			Console.ReadKey();
		}
	}

	public class Base
	{
		public virtual void Output()
		{
			Console.WriteLine("this is Base!");
		}
	}

	public class Child1:Base
	{
		public Child1()
		{
			Name = "Jim";
		}

		public override void Output()
		{
			base.Output();
			Console.WriteLine("this is Child1!");
		}

		private string Name { get; set; }
	}

	public class Child2 : Child1
	{
		public override void Output()
		{
			base.Output();
			Console.WriteLine("this is Child2!");
		}
	}
}
