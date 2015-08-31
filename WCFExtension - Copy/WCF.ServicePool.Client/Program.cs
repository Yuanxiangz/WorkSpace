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
			ChannelFactory<IService> channelFactory = null;
			try
			{
				channelFactory = new ChannelFactory<IService>("Endpoint1");
				ThreadPool.QueueUserWorkItem((o) =>
					{
						for (int i = 1; i <= 1000; i++)
						{
							Console.WriteLine("{0}: invocate service! thread 1", i);
							Console.WriteLine(channelFactory.CreateChannel().DoReturn());
							//Thread.Sleep(1000);
						}
					});

				ThreadPool.QueueUserWorkItem((o) =>
				{
					for (int i = 1; i <= 1000; i++)
					{
						Console.WriteLine("{0}: invocate service! thread 2", i);
						Console.WriteLine(channelFactory.CreateChannel().DoReturn());
						//Thread.Sleep(1000);
					}
				});
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				channelFactory.Close();
			}

			Console.ReadKey();
		}
	}
}
