using System.ServiceModel;
using WCF.ServicePool.Common;

namespace WCF.ServicePool.Contract
{
	[ServiceContract]
	[PooledInstanceBehavior]
	public interface IService : IPooledObject
	{
		[OperationContract(IsOneWay = true)]
		void DoSomething();
	}
}
