using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication5
{
	public class ViewModel
	{
		public ViewModel()
		{
			Colors = "Blue";
			Temp = "Blue";
			Lists = new List<string>()
			{
				"Book",
				"Jim",
				"Tom"
			};
		}

		public string Colors { get; set; }

		public string Temp { get; set; }

		public List<string> Lists { get; set; }
	}
}
