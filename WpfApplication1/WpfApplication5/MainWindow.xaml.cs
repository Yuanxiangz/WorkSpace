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

namespace WpfApplication5
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		ViewModel vm=null;
		public MainWindow()
		{
			InitializeComponent();
			vm = new ViewModel();
			this.DataContext = vm;
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			vm.Colors = vm.Colors == "Red" ? "Blue" : "Red";
		}

		private void OnClick(object sender, EventArgs e)
		{
			vm.Colors = vm.Colors == "Red" ? "Blue" : "Red";
		}
	}
}
