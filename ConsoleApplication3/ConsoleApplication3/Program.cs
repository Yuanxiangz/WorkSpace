using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;
using System.Threading;

namespace ConsoleApplication3
{
	class Program
	{
		static void Main(string[] args)
		{
			//Release("5.7SR9.4.0");
			test15();

			Console.ReadKey();
		}

		static void test()
		{
			SqlTransaction sqlTransaction = null;
			//string userName = string.Empty;
			try
			{
				using (SqlConnection connection = new SqlConnection(@"Data Source=yuanxz7;Initial Catalog=test;User ID=test;Password=test"))
				{
					connection.Open();
					sqlTransaction = connection.BeginTransaction();

					string sqlstr2 = @"update table1 set Address='aaa'";
					SqlCommand cmd2 = new SqlCommand(sqlstr2, connection);
					cmd2.Transaction = sqlTransaction;
					cmd2.ExecuteNonQuery();


					string sqlstr = @"select * from table1 where [ID]=1";
					SqlCommand cmd = new SqlCommand(sqlstr, connection);
					cmd.Transaction = sqlTransaction;
					SqlDataReader sr = cmd.ExecuteReader();
					while (sr.Read())
					{
						for (int i = 0; i < 4; i++)
						{
							Console.WriteLine(sr[i].ToString());
						}
					}

					sr.Close();

					sqlTransaction.Commit();
				}

				Console.ReadKey();
			}
			catch (Exception e)
			{
				sqlTransaction.Rollback();
				Console.WriteLine("Error!!!!!!");
			}
		}

		static void Release(string release)
		{
			bool IsEntitled;
			string regstr = @"(?<prefix>[a-zA-Z]*)?_?(0?(?<group1>\d))\D*(0?(?<group2>\d))\D*(0?(?<group3>\d{1,2}))?\D*(0?(?<group4>\d{1,2}))?[^entENT0123456789]*(0?(?<group5>\d{1,2}|(?i)ent(?-i)))?";
			Match match = new Regex(regstr).Match(release);

			string MajorRelease = match.Groups["group1"].Value;
			string MinorRelease = match.Groups["group2"].Value;
			string SystemRelease = match.Groups["group3"].Value;
			string PatchRelease = match.Groups["group4"].Value;

			string DynamicRelease = !String.IsNullOrEmpty(match.Groups["group5"].Value) ? match.Groups["group5"].Value : "0";

			if (DynamicRelease.Equals("ENT", StringComparison.OrdinalIgnoreCase))
				IsEntitled = true;
		}

		static void test2()
		{
			List<int> list = new List<int>();
			list.Add(12);
			list.Add(2);
			list.Add(112);
			list.Add(120);
			list.Add(12);
			list.Add(14);
			list.Add(1);
			list.Sort(Compare);

			list.ForEach((i) => Console.WriteLine(i));

			//Console.ReadKey();
		}

		static int Compare(int A, int B)
		{
			return A - B;
		}

		static void test3()
		{
			object t1 = new Test1();
			//Test2 t2 = t1 as Test2;
			ITest t2 = t1 as ITest;
			t2.Cry();

			Console.ReadKey();
		}

		static void test4()
		{
			int start = Environment.TickCount;
			for (int i = 0; i < 1000; i++)
			{
				string s = "";
				for (int j = 0; j < 100; j++)
				{
					s += "Outer index = ";
					s += i;
					s += " Inner index = ";
					s += j;
					s += " ";
				}
			}
		}

		static void test5()
		{
			Suit s1 = Suit.Red;
			Suit s2 = Suit.Square;

			Console.WriteLine(s2 > s1);
		}

		static void test6()
		{
			decimal a = Convert.ToDecimal("120.102000");
			decimal b = 2800m;

			Console.WriteLine(a.ToString("G0", CultureInfo.InvariantCulture));
			Console.WriteLine(b.ToString("G0", CultureInfo.InvariantCulture));
			Console.WriteLine(DateTime.Now.ToLongTimeString());
			int aaa = 9;
			double bbb = 0.5;
			Console.WriteLine(Math.Pow(9, 0.5));
		}

		static void test7(int a, int b)
		{
			a = a + b;
			b = a - 2 * b;
			a = (a - b) / 2;
			b = a + b;

			Console.WriteLine("a={0}, b={1}", a, b);
		}

		static void test8()
		{
			ArrayQueue<int> queue = new ArrayQueue<int>(3);
			queue.Enqueue(1);
			queue.Enqueue(2);
			queue.Dequeue();
			queue.Enqueue(3);
			queue.Dequeue();
			queue.Enqueue(4);

			Console.WriteLine(queue.Dequeue());
			Console.WriteLine(queue.Dequeue());
			Console.WriteLine(queue.Dequeue());
			Console.WriteLine(queue.Dequeue());
			Console.WriteLine(queue.Dequeue());
			Console.WriteLine(queue.Dequeue());
		}

		static void test10()
		{
			List<int> list = new List<int>();
			list.Add(1);
			list.Add(2);
			list.Add(3);
			list.Add(4);
			list.Add(5);

			list.Insert(1, 10);
			list.RemoveAt(1);

			string a = "";
		}

		static void test9(int[] array, int m)
		{
			int n = array.Length;
			List<int> results = new List<int>();//ASC order sorted list
			for (int i = 0; i < n; i++)
			{
				if (results.Count < m)
				{
					if (results.Count == 0)
						results.Add(array[i]);
					else
					{
						for (int j = results.Count - 1; j > 0; j--)
						{
							if (results[j] <= array[i])
							{
								results.Add(array[i]);
								break;
							}
							else if (results[j] > array[i] && j == 0)
							{
								results.Insert(0, array[i]);
								break;
							}
							else if (array[i] >= results[j - 1] && array[i] < results[j])
							{
								results.Insert(j, array[i]);
								break;
							}
						}
					}
				}
				else if (results.Count == m)
				{
					if (results[m - 1] <= array[i])
						continue;
					else
					{
						//int removeIndex = BinarySearch(results, array[i]);
						//results.Insert(removeIndex, array[i]);
						//results.RemoveAt(m);
					}
				}
			}
		}

		public static int BinarySearch(int[] ints, int key, int i)
		{
			int index = i / 2;
			if (ints[index] < key)
			{
				return BinarySearch(ints, key, index + ints.Length);
			}
			else if (ints[index] > key)
			{
				return BinarySearch(ints, key, index - 1);
			}
			else if (ints[index] == key)
			{
				return index;
			}

			return -1;
		}

		static void test11()
		{
			Timer timer = new Timer(TimerCallBack, DateTime.Now, 10000, 2000);
			Task<string> task = new Task<string>(TaskString);
			Console.WriteLine(task.Result);
		}

		static void TimerCallBack(object obj)
		{
			Console.WriteLine(((DateTime)obj).ToLongTimeString());
		}

		static string TaskString()
		{
			return "O";
		}

		static void test12()
		{
			Dictionary<string, string> dic = new Dictionary<string, string>();
			for (int i = 0; i < 10000; i++)
			{
				dic.Add(i.ToString(), "1");
			}

			DateTime now = DateTime.Now;
			List<string> list = new List<string>(dic.Keys);
			for (int i = 0; i < 10000; i++)
			{
				foreach (var str in list)
				{
					string a = "";
				}
			}
			Console.WriteLine((DateTime.Now - now).TotalMilliseconds);

			now = DateTime.Now;
			for (int i = 0; i < 10000; i++)
			{
				foreach (var str in new List<string>(dic.Keys))
				{
					string a = "";
				}
			}
			Console.WriteLine((DateTime.Now - now).TotalMilliseconds);
		}

		static void test13()
		{
			object obj = new object();
			List<WeakReference> list = test14(obj);
			//ThreadPool.QueueUserWorkItem(new WaitCallback(o =>
			//{
			//	foreach (WeakReference wr in list)
			//	{
			//		if (wr.Target != null)
			//			Console.WriteLine(wr.Target.ToString());
			//	}
			//}));
			HashSet<string> m = new HashSet<string>();
			GC.Collect();
			foreach (WeakReference wr in list)
			{
				if (wr.Target != null)
					Console.WriteLine(wr.Target.ToString());
			}
		}

		static List<WeakReference> test14(object obj)
		{
			List<WeakReference> list = new List<WeakReference>();
			list.Add(new WeakReference(obj));
			return list;
		}

		static void test15()
		{
			Dictionary<string, string> dics = new Dictionary<string, string>();
			dics["1"] = "aaa";
			dics["1"] = "bbb";
			dics.Remove("2");
		}
	}

	public class Test1
	{
		public void Eat()
		{
			Console.WriteLine("Test1 eat!");
		}

		public void Cry()
		{ }
	}

	public class Test2
	{
		public void Eat()
		{
			Console.WriteLine("Test2 eat!");
		}
	}

	public interface ITest
	{
		void Cry();
	}

	enum Suit
	{
		Red,
		Black,
		Square
	}
}
