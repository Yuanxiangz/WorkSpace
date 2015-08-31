using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication4
{
	/// <summary>
	/// Interaction logic for NestedUserControl.xaml
	/// </summary>
	public partial class NestedUserControl : UserControl
	{
		public static readonly DependencyProperty AllocationMethodProperty =
			DependencyProperty.Register("AllocationMethod", typeof(string), typeof(NestedUserControl));

		public static readonly DependencyProperty AllocationMethodSourceProperty =
			DependencyProperty.Register("AllocationMethodSource", typeof(object), typeof(NestedUserControl));

		public NestedUserControl()
		{
			InitializeComponent();
		}

		public string AllocationMethod
		{
			get { return (string)GetValue(AllocationMethodProperty); }
			set { SetValue(AllocationMethodProperty, value); }
		}

		public object AllocationMethodSource
		{
			get { return GetValue(AllocationMethodSourceProperty); }
			set { SetValue(AllocationMethodSourceProperty, value); }
		}
	}
}
