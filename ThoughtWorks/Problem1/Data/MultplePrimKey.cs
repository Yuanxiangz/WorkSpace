using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem1.Data
{
	public class MultplePrimKey
	{
		public string Key1 { get; set; }
		public string Key2 { get; set; }

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (ReferenceEquals(this, obj))
				return true;

			MultplePrimKey otherKey = (obj as MultplePrimKey);

			if ((otherKey != null) && (otherKey.Key1 == this.Key1) && (otherKey.Key2 == Key2))
				return true;

			return false;
		}

		public override int GetHashCode()
		{
			return (Key1 + Key2).GetHashCode();
		}
	}
}
