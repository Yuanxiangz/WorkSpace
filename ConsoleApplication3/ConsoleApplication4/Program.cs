using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApplication4
{
	class Program
	{
		static void Main(string[] args)
		{
			test2();

			Console.ReadKey();
		}

		static void test()
		{
			List<string> list = new List<string>();
			string a = default(string);
			string temp = list.Where(x => x.Equals("a")).FirstOrDefault();

			Console.WriteLine(temp);
		}

		static void test1()
		{
			decimal d1 = 10.1111111111111000m;
			FormatPrividerTest test = new FormatPrividerTest();

			Console.WriteLine(string.Format(test, "{0}", d1));
			Console.WriteLine(d1.ToString("0.00#####"));
			//Console.WriteLine(d1.ToString("", test));
		}

		static void test2()
		{
			Timer t1 = new Timer(ThreadCallback);
			//t1.Change(0, -1);
		}

		static void ThreadCallback(object obj)
		{
			Console.WriteLine("aaaa");
		}
	}
}
