using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCoding.Collections.Interfaces
{
	public interface IVisitableDictionary<TKey, TValue>
	{
		void Accept(IVisitor<KeyValuePair<TKey, TValue>> visitor);

		bool IsFixedSize { get; }

		bool IsEmpty { get; }

		bool IsFull { get; }
	}
}
