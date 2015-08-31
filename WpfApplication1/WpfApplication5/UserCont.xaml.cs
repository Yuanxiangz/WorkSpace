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
	/// Interaction logic for UserCont.xaml
	/// </summary>
	public partial class UserCont : UserControl
	{
		public static readonly DependencyProperty ColorProperty =
			DependencyProperty.Register("Color", typeof(string), typeof(UserCont),
			new PropertyMetadata(null, ControlPropertyChanged));

		public static readonly DependencyProperty EventHandlerProperty =
			DependencyProperty.Register("Command", typeof(EventHandler), typeof(UserCont),
			new PropertyMetadata(null, null));

		public UserCont()
		{
			InitializeComponent();
		}

		public string Color
		{
			get { return (string)GetValue(ColorProperty); }
			set { SetValue(ColorProperty, value); }
		}

		public EventHandler Command
		{
			get { return (EventHandler)GetValue(EventHandlerProperty); }
			set { SetValue(EventHandlerProperty, value); }
		}

		private static void ControlPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
		{
			string a = string.Empty;
		}
	}
}
