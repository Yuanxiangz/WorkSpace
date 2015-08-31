using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.ServicePool.Common
{
	public static class PooledInstanceLocator
	{
		internal static WeakReferenceDictionary ServiceInstancePool
		{ get; set; }

		static PooledInstanceLocator()
		{
			ServiceInstancePool = new WeakReferenceDictionary();
		}

		public static IPooledObject GetInstanceFromPool(Type serviceType)
		{
			if (!serviceType.GetInterfaces().Contains(typeof(IPooledObject)))
			{
				throw new InvalidCastException("InstanceType must implement Artech.WCFExtensions.IPooledInstance");
			}

			if (!ServiceInstancePool.ContainsKey(serviceType))
			{
				ServiceInstancePool[serviceType] = new WeakReferenceCollection();
			}

			WeakReferenceCollection instanceReferenceList = ServiceInstancePool[serviceType];

			lock (serviceType)
			{
				IPooledObject serviceInstance = null;
				foreach (WeakReference weakReference in instanceReferenceList)
				{
					serviceInstance = weakReference.Target as IPooledObject;
					if (serviceInstance != null && !serviceInstance.IsBusy)
					{
						serviceInstance.IsBusy = true;
						return serviceInstance;
					}
				}

				serviceInstance = Activator.CreateInstance(serviceType) as IPooledObject;
				serviceInstance.IsBusy = true;
				instanceReferenceList.Add(new WeakReference(serviceInstance));
				return serviceInstance;
			}
		}

		public static void Scavenge()
		{
			foreach (Type serviceType in ServiceInstancePool.Keys)
			{
				lock (serviceType)
				{
					WeakReferenceCollection instanceReferenceList = ServiceInstancePool[serviceType];
					for (int i = instanceReferenceList.Count - 1; i > -1; i--)
					{
						if (instanceReferenceList[i].Target == null)
						{
							instanceReferenceList.RemoveAt(i);
						}
					}

				}
			}
		}

		public static void ReleaseInstanceToPool(IPooledObject instance)
		{
			instance.IsBusy = false;
		}
	}

	public class WeakReferenceCollection : List<WeakReference>
	{ }

	public class WeakReferenceDictionary : Dictionary<Type, WeakReferenceCollection>
	{ }

}
