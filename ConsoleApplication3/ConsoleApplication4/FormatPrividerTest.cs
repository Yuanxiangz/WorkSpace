using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
	public class FormatPrividerTest : IFormatProvider, ICustomFormatter
	{
		public object GetFormat(Type formatType)
		{
			//if (formatType == typeof(ICustomFormatter))
				return this;
			//else
			//	return null;
		}

		public string Format(string format, object arg, IFormatProvider formatProvider)
		{
			try
			{
				if (arg != null)
				{
					string convertValue = arg.ToString().Trim();
					if (string.IsNullOrEmpty(convertValue))
						return string.Empty;

					if (convertValue.Contains("."))
					{
						convertValue = convertValue.TrimEnd(new char[] { '0' });
						if (convertValue.EndsWith("."))
						{
							convertValue = convertValue.Remove(convertValue.Length - 1, 1);
						}
						else
							return convertValue;
					}

					return string.Format("{0}.00", convertValue);
				}
			}
			catch (Exception ex)
			{ 
				
			}

			return string.Empty;
		}
	}
}
