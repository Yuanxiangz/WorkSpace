using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
	public class ClassBase<T>
	{
		private T item;

		public T Item
		{
			get { return item; }
			set { item = value; }
		}

		public int ID
		{
			get { return GetID(item); }
		}

		protected abstract int GetID(T item);
	}
}
