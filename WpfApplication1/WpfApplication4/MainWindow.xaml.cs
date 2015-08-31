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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace WpfApplication4
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			TestUserControlViewModel vm = new TestUserControlViewModel();
			this.DataContext = vm;
		}

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			await Test();
		}

		private async Task Test()
		{
			await DownloadFile();
			this.bt1.Content = "Download";
		}

		private async Task DownloadFile()
		{
			await Task.Delay(5000).ConfigureAwait(false);
			MessageBox.Show("Download finished!");
		}
	}

	public class Test
	{
		public string Name { get; set; }
	}
}
