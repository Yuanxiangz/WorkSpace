using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
	public class Utils
	{
		public static object RunMethod(System.Type t,
			string methodName, object objInstance, object[] objParams, BindingFlags eFlags)
		{
			MethodInfo m = t.GetMethod(methodName, eFlags);
			if (m == null)
			{
				throw new ArgumentException("There is no method '" +
				 methodName + "' for type '" + t.ToString() + "'.");
			}

			object objRet = m.Invoke(objInstance, objParams);
			return objRet;
		}
	}
}
