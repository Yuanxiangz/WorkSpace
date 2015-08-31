using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCF.ServicePool.Common
{
	public interface IPooledObject
	{
		bool IsBusy
		{ get; set; }
	}
}
