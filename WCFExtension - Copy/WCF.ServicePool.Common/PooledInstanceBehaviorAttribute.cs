using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace WCF.ServicePool.Common
{
	public class PooledInstanceBehaviorAttribute : Attribute, IContractBehavior
	{
		public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) { }
		
		public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime) { }
		
		public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
		{
			dispatchRuntime.InstanceProvider = new PooledInstanceProvider();
		}
		
		public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint) { }
	}

}
