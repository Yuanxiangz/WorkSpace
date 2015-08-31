using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace WCF.ServicePool.Common
{
	public class PooledInstanceProvider : IInstanceProvider
	{
		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			return PooledInstanceLocator.GetInstanceFromPool(instanceContext.Host.Description.ServiceType);
		}

		public object GetInstance(InstanceContext instanceContext)
		{
			return this.GetInstance(instanceContext, null);
		}

		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
			PooledInstanceLocator.ReleaseInstanceToPool(instance as IPooledObject);
		}
	}

}
