using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Collections.Generic;

namespace UnitTestProject1
{
	[TestClass]
	public class UnitTest1
	{
		Mock<ITradeManager> tradeManagerMock = new Mock<ITradeManager>();
		TestManager target = new TestManager();
		TaskFactory taskFactory = new TaskFactory();

		[TestMethod]
		public void TestMethod1()
		{
			//
			Func<string> func = Test;
			Task<string> task = taskFactory.StartNew<string>(func);
			tradeManagerMock.Setup(x => x.test()).Returns(task);

			target.TradeManager = tradeManagerMock.Object;

			//
			Task<string> result = target.Go();

			//
			result.Wait();
			Assert.AreEqual(result.Result, "bad");
		}

		private string Test()
		{
			return "bad";
		}
	}

	public class TestManager
	{
		public ITradeManager TradeManager { get; set; }

		public string Result { get; set; }

		public async Task<string> Go()
		{
			string Result = await TradeManager.test();

			return Result;
		}
	}

	public class TradeManager : ITradeManager
	{
		public async Task<string> test()
		{
			Task<string> task = new Task<string>(() =>
			{
				Task.Delay(4000);
				return "good";
			});

			string result = await task;

			return result;
		}
	}

	public interface ITradeManager
	{
		Task<string> test();
	}
}
