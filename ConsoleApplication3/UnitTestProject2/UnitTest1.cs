using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestProject2
{
	[TestClass]
	public class UnitTest1
	{
		Mock<ITest> testMock = new Mock<ITest>();
		CompositionContainer container = null;

		[TestInitialize]
		public void Setup()
		{
			testMock.Setup(x => x.ATest(It.IsAny<string>())).Returns("Hello");

			var catalog = new AggregateCatalog();
			catalog.Catalogs.Add(new AssemblyCatalog(typeof(Test).Assembly));
			catalog.Catalogs.Add(new AssemblyCatalog(typeof(MainPro).Assembly));
			container = new CompositionContainer(catalog);
			container.ComposeParts(this);
			//container.ComposeExportedValue<ITest>(testMock.Object);
			//ITest test=container.GetExportedValue<ITest>();
			//string a = test.ATest("");
		}

		[TestMethod]
		public void TestMethod1()
		{
			MainPro pro = new MainPro();

			Assert.AreEqual(pro.GetName(), pro.GetConst());
		}
	}

	public interface ITest
	{
		string ATest(string name);
	}

	[Export(typeof(ITest))]
	public class Test : ITest
	{
		public string Name { get; private set; }

		public string ATest(string name)
		{
			return name;
		}
	}

	public class MainPro
	{
		[Import]
		public ITest Test { get; set; }

		public string GetName()
		{
			return Test.ATest("Hello"); ;
		}

		public string GetConst()
		{
			return "Hello";
		}
	}
}
