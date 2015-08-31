using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;

namespace WpfApplication1
{
	public class Desk
	{
		public string Name { get; set; }

		public bool IsVIP { get; set; }
	}
	public class Car
	{
		public string Name { get; set; }
	}
	public class UserControl1 : UserControl
	{
		public static readonly DependencyProperty ViewContentProperty =
	DependencyProperty.Register("ViewContent", typeof(object), typeof(UserControl1), null);

		public static readonly DependencyProperty CustomCommandProperty =
	DependencyProperty.Register("CustomCommand", typeof(object), typeof(UserControl1), new PropertyMetadata(new List<BlotterCommand>(), new PropertyChangedCallback(OnRegisterCommandsChanged)));

		public static readonly DependencyProperty ItemsProperty =
DependencyProperty.Register("Items", typeof(List<object>), typeof(UserControl1), new PropertyMetadata(new List<object>(), null));

		public UserControl1()
		{
			////Grid grid = new Grid();
			////Button bt = new Button() { Height = 20, Width = 20 };
			////grid.Children.Add(bt);
			////this.Background = new SolidColorBrush(Colors.Red);
			////grid.Background = new SolidColorBrush(Colors.Yellow);
			////this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
			////this.VerticalContentAlignment = System.Windows.VerticalAlignment.Stretch;

			////this.Content = grid;

			////grid.Width = 100;
			////grid.Height = 100;
			//StackPanel panel = new StackPanel();
			//panel.Orientation = Orientation.Horizontal;
			//panel.Children.Add(new Button() { Content = "ABC" });
			//panel.Children.Add(new Button() { Content = "BCD" });

			//ViewContent = panel;
			////this.Content = ViewContent;
			//Items = new List<string>() { "Apple","Pear" };
			string a = "";
		}

		public List<BlotterCommand> CustomCommand
		{
			get { return GetValue(CustomCommandProperty) as List<BlotterCommand>; }
			set
			{
				SetValue(CustomCommandProperty, value);
			}
		}

		public object ViewContent
		{
			get { return GetValue(ViewContentProperty); }
			set { SetValue(ViewContentProperty, value); }
		}

		public List<object> Items 
		{
			get { return GetValue(ItemsProperty) as List<object>; }
			set { SetValue(ItemsProperty, value); }
		}

		//protected override void OnInitialized(System.EventArgs e)
		//{
		//	base.OnInitialized(e);

		//	//Grid grid = new Grid();
		//	//Button bt = new Button() { Height = 20, Width = 20 };
		//	//grid.Children.Add(bt);
		//	//this.Background = new SolidColorBrush(Colors.Red);
		//	//grid.Background = new SolidColorBrush(Colors.Yellow);
		//	//this.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Stretch;
		//	//this.VerticalContentAlignment = System.Windows.VerticalAlignment.Stretch;

		//	//this.Content = grid;

		//	//grid.Width = 100;
		//	//grid.Height = 100;
		//	StackPanel panel = new StackPanel();
		//	panel.Orientation = Orientation.Horizontal;
		//	panel.Children.Add(new Button() { Content = "ABC" });
		//	panel.Children.Add(new Button() { Content = "BCD" });

		//	ViewContent = panel;
		//	//this.Content = ViewContent;
		//}

		private static void OnRegisterCommandsChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			try
			{
				UserControl1 control = (UserControl1)sender;

				//StackPanel panel = new StackPanel();
				//panel.Orientation = Orientation.Horizontal;
				//panel.Children.Add(new Button() { Content = "ABC" });
				//panel.Children.Add(new Button() { Content = "BCD" });
				//StackPanel panel1 = control.Template.FindName("horizontalCommandPanel",control) as StackPanel;
				//UIElementCollection collection = new UIElementCollection(control, control);
				//collection.Add(new Button() { Content = "ABC" });
				//collection.Add(new Button() { Content = "BCD" });
				////panel = collection;


				//control.ViewContent = collection;
			}
			catch (System.Exception ex)
			{
				System.Diagnostics.Trace.WriteLine("[Test]" + ex);
			}
		}
	}
}
