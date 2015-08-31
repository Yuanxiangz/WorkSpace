using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication3.Types;

namespace ConsoleApplication3
{
	public class ClassConcerned : ClassBase<Type1>
	{
		protected override int GetID(Type1 item)
		{
			return item.TypeID;
		}
	}
}
