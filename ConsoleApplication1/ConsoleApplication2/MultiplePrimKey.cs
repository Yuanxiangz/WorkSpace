using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
	public class MultplePrimKey
	{
		private string key1;
		private string key2;

		public string Key1
		{
			get { return key1; }
			set { key1 = value; }
		}
		public string Key2
		{
			get { return key2; }
			set { key2 = value; }
		}

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
			return (key1 + key2).GetHashCode();
		}
	}
}
