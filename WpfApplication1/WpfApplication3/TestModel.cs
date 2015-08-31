using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eze.InfragisticsEx;

namespace WpfApplication3
{
	public class TestModel
	{
		private EzeLayoutCollection selectedLayout;

		public EzeLayoutCollection SelectedLayout
		{
			get { return this.selectedLayout; }
			set { selectedLayout = value; }
		} 
	}

	public class TestData
	{
		public string Name { get; set; }

		public int ID { get; set; }
	}
}
