using System;
using System.ServiceModel;
using System.Threading;
using WCF.ServicePool.Contract;

namespace WCF.ServicePool.Service
{
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
	public class Service : IService
	{
		static int count;
		public Service()
		{
			Interlocked.Increment(ref count);
			Console.WriteLine("{0}: Service instance is constructed!", count);
		}

		public void DoSomething() 
		{ 
			Thread.Sleep(2000);
		}
		
		public bool IsBusy
		{ get; set; }
	}
}
