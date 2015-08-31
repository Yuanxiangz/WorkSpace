using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Moq;

namespace UnitTestProject1
{
	[TestClass]
	public class UnitTest1
	{
		Mock<Interface1> test1 = new Mock<Interface1>();
		List<string> list = new List<string>();
		[TestInitialize]
		public void Initialize()
		{
			
			test1.Setup(x => x.aaa(It.IsAny<string>())).Callback((string value) => list.Add(value));
		}

		[TestMethod]
		public void TestMethod1()
		{
			test1.Object.aaa("aaa");
			Assert.AreEqual("aaa", list.First());
		}
	}
}
