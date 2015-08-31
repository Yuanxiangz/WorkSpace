using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Reflection;

namespace ConsoleApplication2
{
	class Program
	{
		private static Dictionary<string, string> htFixList = new Dictionary<string, string>();

		static void Main(string[] args)
		{

			test13();

			Console.ReadKey();
		}

		static void test13()
		{
			Dictionary<MultplePrimKey, int> dic = new Dictionary<MultplePrimKey, int>();
			MultplePrimKey key = new MultplePrimKey();
			key.Key1 = "A";
			key.Key2 = "B";
			dic.Add(key, 2);
			key = new MultplePrimKey();
			key.Key1 = "A";
			key.Key2 = "C";
			dic.Add(key, 12);
			key = new MultplePrimKey();
			key.Key1 = "A";
			key.Key2 = "D";
			dic.Add(key, 3);
			key = new MultplePrimKey();
			key.Key1 = "C";
			key.Key2 = "B";
			dic.Add(key, 5);
			key = new MultplePrimKey();
			key.Key1 = "D";
			key.Key2 = "B";
			dic.Add(key, 2);
			key = new MultplePrimKey();
			key.Key1 = "C";
			key.Key2 = "D";
			dic.Add(key, 32);


			MultplePrimKey testkey = new MultplePrimKey();
			testkey.Key1 = "C";
			testkey.Key2 = "D";

			Console.WriteLine(dic[testkey].ToString());

		}

		static void test11()
		{
			Class3.Test("n");
			Class3.Test("m");
			Class3.Test("d");
			Class3.Test("aaa");
		}

		static void test10()
		{
			LinkType linkType = LinkType.Security;
			linkType = linkType | LinkType.Security;
			string a = "";
		}

		static OfferSide ConvertAction(OfferSide ioiAction)
		{
			switch (ioiAction)
			{
				case OfferSide.Buy:
					return OfferSide.Buy;
				case OfferSide.Cover:
					return OfferSide.Buy;
				case OfferSide.Sell:
					return OfferSide.Sell;
				case OfferSide.Short:
					return OfferSide.Sell;
				default:
					return OfferSide.NotSet;
			}
		}
		static bool IsSideMatch(OfferSide ioiOfferSide, OfferSide linkedAction)
		{
			if (ioiOfferSide.Equals(linkedAction))
				return true;
			else
				return ioiOfferSide.Equals(ConvertAction(linkedAction));
		}

		static bool IsSideMatch2(OfferSide ioiOfferSide, OfferSide linkedAction)
		{
			return ioiOfferSide.Equals(ConvertAction(linkedAction));
		}

		static void test9()
		{
			DateTime startTime = DateTime.Now;
			for (int i = 0; i < 100000; i++)
			{
				IsSideMatch(OfferSide.Buy, OfferSide.Buy);
			}

			Console.WriteLine("Time snap1: " + (DateTime.Now - startTime).ToString());

			startTime = DateTime.Now;
			for (int i = 0; i < 100000; i++)
			{
				IsSideMatch2(OfferSide.Short, OfferSide.Short);
			}

			Console.WriteLine("Time snap2: " + (DateTime.Now - startTime).ToString());
		}

		static void test8()
		{
			Class2 c2 = new Class2();
			c2.bbb();
		}

		static void test7()
		{
			DateTime now = DateTime.Now;
			string htFix = @"IOIQualifier=C|IOIQualifier=A|";
			for (int i = 0; i < 10000; i++)
			{
				ParseAttributeGroup(htFix);
			}

			TimeSpan span = DateTime.Now - now;

			Console.WriteLine("Time span: " + span);
		}

		static void test6()
		{
			DateTime now = DateTime.Now;
			string htFix = @"199=IOIQualifier=C|IOIQualifier=A||218=12|219=aaa|220=aaa|221=aaa|222=aaa|223=aaa|";
			for (int i = 0; i < 10000; i++)
			{
				ParseHTFix(htFix);
				htFixList.Clear();
			}

			TimeSpan span = DateTime.Now - now;

			Console.WriteLine("Time span: " + span);
		}

		static void test5()
		{
			HashSet<string> aa = new HashSet<string>();
			aa.Add("C");
			aa.Add("D");
			aa.Add("A");

			Dictionary<string, string> ttt = new Dictionary<string, string>();
			ttt.Add("C", "C");
			ttt.Add("D", "D");
			ttt.Add("A", "A");

			string b = "";
		}

		static void test4()
		{
			LinkType type = LinkType.Trade | LinkType.Security;
			switch (type)
			{
				case LinkType.Trade:
					Console.WriteLine("Trade");
					break;
				case LinkType.Security:
					Console.WriteLine("Security");
					break;
				default:
					Console.WriteLine("default");
					break;
			}

			object obj = null;
			string a = "";
			LinkType typeaa;

			if (Enum.TryParse<LinkType>(a, out typeaa))
			{
				string s = "";
			}

			typeaa = LinkType.Undefined;
			string sss = GetDescription(typeaa);

			Console.WriteLine(type.ToString());
		}

		static void test3()
		{
			LinkType type;
			Enum.TryParse<LinkType>("Tradess", out type);
			string a = "";
			HashSet<LinkType> aaa = new HashSet<LinkType>();
			aaa.Add(LinkType.Trade);
			aaa.Add(LinkType.Security);
			aaa.Add(LinkType.Undefined);
			Console.WriteLine(string.Join<LinkType>(",", aaa));
		}

		static void test2()
		{
			System.Windows.Forms.Control test = new System.Windows.Forms.Control();
			test.Click += test_Click;
			test.Click -= test_Click;
			test.Click -= test_Click;
			test.Click -= test_Click;
		}

		private static void test_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		static void test1()
		{
			string str = @"<{[(+?/-=|,! ^$: At.x*";
			string msgValue = @"<{[(+?/-=|,! ^$: at.x_test";
			//string str = @"*At*x*";
			//string msgValue = @"weataax_test";
			Console.WriteLine("str: " + str);
			Console.WriteLine("msgValue: " + msgValue);

			string regstr = GenerateRegex(str.Trim().ToLower());
			Console.WriteLine("regstr: " + regstr);
			Regex reg = new Regex(regstr);
			if (reg.IsMatch(msgValue.ToLower()))
			{
				Console.WriteLine("Y");
			}
			else
				Console.WriteLine("N");

			int g;
			Console.WriteLine(Int32.TryParse("12sa", out g));
		}

		static string GenerateRegex(string str)
		{
			str = str.Replace(@"\", @"\\");
			str = str.Replace(@".", @"\.");
			str = str.Replace(@"^", @"\^");
			str = str.Replace(@"$", @"\$");
			str = str.Replace(@"+", @"\+");
			str = str.Replace(@"?", @"\?");
			str = str.Replace(@"{", @"\{");
			str = str.Replace(@"}", @"\}");
			str = str.Replace(@"[", @"\[");
			str = str.Replace(@"]", @"\]");
			str = str.Replace(@"(", @"\(");
			str = str.Replace(@")", @"\)");
			str = str.Replace(@"<", @"\<");
			str = str.Replace(@">", @"\>");
			str = str.Replace(@"-", @"\-");
			str = str.Replace(@"=", @"\=");
			str = str.Replace(@"|", @"\|");
			str = str.Replace(@",", @"\,");
			str = str.Replace(@"!", @"\!");
			str = str.Replace(@":", @"\:");
			str = str.Replace(@" ", @"\s");

			str = str.Replace(@"*", @".*");
			str = "^" + str + "$";

			return str;
		}

		public static string GetDescription(Enum value)
		{
			FieldInfo fi = value.GetType().GetField(value.ToString());
			DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
			return attributes.Length > 0 ? attributes[0].Description : value.ToString();
		}

		static void ParseHTFix(string htFix)
		{
			Regex regex = new Regex(@"\d+=");
			MatchCollection mc = regex.Matches(htFix);
			foreach (Match m in mc)
			{
				int valueLength;
				int valueStartIndex = m.Index + m.Value.Length;
				if (m.NextMatch().Index != 0)
					valueLength = m.NextMatch().Index - valueStartIndex - 1;// remove "|"
				else
					valueLength = htFix.Length - 1 - valueStartIndex;

				if (valueLength > 0)
				{
					htFixList.Add(m.Value.TrimEnd(new char[] { '=' }), htFix.Substring(valueStartIndex, valueLength));
				}
			}
		}

		static string ParseAttributeGroup(string attrGroup)
		{
			if (!string.IsNullOrEmpty(attrGroup))
			{
				string[] items = attrGroup.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);

				List<string> elementList = new List<string>();
				for (int j = 0; j < items.Length; j++)
				{
					string[] units = items[j].Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
					if (units.Length == 2)
					{
						elementList.Add(units[1].Trim());
					}
				}

				return string.Join(",", elementList);
			}

			return string.Empty;
		}

		static void test12()
		{
			OfferSide s;
			if (Enum.TryParse("buy", true, out s))
			{
				Console.WriteLine(s.ToString());
			}
		}

		[Flags]
		enum LinkType
		{
			[Description("")]
			Undefined = 0x1,
			[Description("Trade")]
			Trade = 0x2,
			[Description("Security")]
			Security = 0x4,
			[Description("Underlying")]
			Underlying = 0x8
		}

		enum OfferSide
		{
			Buy,
			Sell,
			Short,
			Cover,
			NotSet
		}
	}
}
