using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ConsoleApplication2.A;

namespace ConsoleApplication2
{
	class Program
	{
		static void Main(string[] args)
		{
			test3();

			Console.ReadKey();
		}

		static void test()
		{
			Class1.Item a = new Class1.Item();
			Type t = typeof(Class1);
			Type tinner = t.GetNestedType("Item", BindingFlags.NonPublic);
			PrivateObject o = new PrivateObject(tinner);
			o.SetFieldOrProperty("ID", "12345");

			PrivateObject class1 = new PrivateObject(typeof(Class1));
			dynamic list = (List<dynamic>)class1.GetFieldOrProperty("itemList");
			list.Add(o.Target);

			Console.WriteLine(class1.GetFieldOrProperty("GetFromListID"));
		}

		public void test11()
		{
			Class1.Item a = new Class1.Item();
		}

		public static void test2()
		{
			Class2 c2 = new Class2();
			c2.Run();
			c2.Run();
			c2.Run();

			Console.WriteLine(MyAopHandler.Count);
		}

		public static void test3()
		{
			Class1 a = null;
			Debug.Assert(a != null, "asdfasdf");
			a.ToString();
		}
	}

	public class Class1Sub : Class1, IClass1
	{
		public string Name
		{
			get { return "Bill"; }
		}
	}

	public enum TestType
	{
		A,
		B,
		C
	}
}
