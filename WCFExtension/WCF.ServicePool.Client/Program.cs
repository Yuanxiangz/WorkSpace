using System;
using System.ServiceModel;
using System.Threading;
using WCF.ServicePool.Contract;

namespace WCF.ServicePool.Client
{
	class Program
	{
		static void Main(string[] args)
		{
			using (ChannelFactory<IService> channelFactory = new ChannelFactory<IService>("Endpoint1"))
			{
				for (int i = 1; i <= 10; i++)
				{
					Console.WriteLine("{0}: invocate service!", i);
					channelFactory.CreateChannel().DoSomething();
					Thread.Sleep(1000);
				}
			}

			Console.ReadKey();
		}
	}
}
