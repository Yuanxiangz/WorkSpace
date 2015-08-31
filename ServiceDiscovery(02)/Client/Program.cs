using System;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using Artech.ServiceDiscovery.Service.Interface;
namespace Artech.ServiceDiscovery.Client
{
	class Program
	{
		static void Main(string[] args)
		{
			using (ChannelFactory<ICalculator> channelFactory = new ChannelFactory<ICalculator>("calculatorservice"))
			{
				ICalculator calculator = channelFactory.CreateChannel();
				Console.WriteLine("x + y = {2} when x = {0} and y = {1}", 1, 2, calculator.Add(1, 2));
			}
			Console.Read();
		}
	}
}







