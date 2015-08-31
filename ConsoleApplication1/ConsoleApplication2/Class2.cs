using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ConsoleApplication2
{
	public class Class2
	{
		public void bbb()
		{
			Class1 class1 = new Class1();
			TestItem item = new TestItem() { Name = "item1", Desc = "test" };
			object obj = Utils.RunMethod(typeof(Class1), "aaa", class1, new object[] { item }, BindingFlags.NonPublic | BindingFlags.Instance);

			Console.WriteLine(obj.ToString());

			Utils.RunMethod(typeof(Class1), "ttt", class1, null, BindingFlags.NonPublic | BindingFlags.Instance);
			Console.WriteLine(class1.Pro);

			PrivateObject pobj = new PrivateObject(class1);
			TestItem item1 = new TestItem() { Name = "item2", Desc = "test" };
			object obj1 = pobj.Invoke("aaa", new object[] { item1 });
			Console.WriteLine(obj1.ToString());
		}
	}
}
