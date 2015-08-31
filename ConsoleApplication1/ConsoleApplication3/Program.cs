using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApplication3
{
	public class Program
	{
		static void Main(string[] args)
		{
			Dictionary<string, WeakReferenceCollection> list = new System.Collections.Generic.Dictionary<string, WeakReferenceCollection>();
			int count = 0;
			while (true)
			{

				WeakReferenceCollection wc = new WeakReferenceCollection();
				list.Add(count.ToString(), wc);
				count++;
				wc.Add(new WeakReference(new A() { Name = "Jim" }));
				wc.Add(new WeakReference(new A() { Name = "Tom" }));
				wc.Add(new WeakReference(new A() { Name = "Will" }));

				object obj = new object();
				wc.Add(new WeakReference(obj));
				obj = null;
				GC.Collect();

				string name = (wc.First().Target as A).Name;


				string str = "[amount] = [100.1m] and [committed] >= [20.0m]";
				string pattern1 = @"\[\d+\.\d+m\]";
				MatchEvaluator evaluator = new MatchEvaluator(test);
				string result = Regex.Replace(str, pattern1, evaluator, RegexOptions.IgnoreCase);

				string tt = ((OfferSide)3).ToString();
				OfferSide aa;
				bool bb=Enum.TryParse<OfferSide>("3", out aa);
				bool cc = Enum.IsDefined(typeof(OfferSide), aa);

			}

			Console.ReadKey();
		}

		static string test(Match m)
		{
			return m.Value.Replace("m", "");
		}
	}

	public class WeakReferenceCollection : List<WeakReference>
	{ }

	public class A
	{
		public string Name { get; set; }
	}

	public enum OfferSide
	{
		Buyer = 1,
		Seller = 2,
		Unknown = 7
	}
}
