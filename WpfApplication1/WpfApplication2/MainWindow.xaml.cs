using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApplication2
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		ScrollBar sBar = null;
		ScrollViewer _scvComboBox = null;
		public MainWindow()
		{
			InitializeComponent();
			this.DataContext = new TestModel(3000);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			//Grid grid = (Grid)((Button)sender).Parent;
			//grid.Children.Add(new Button() { Content = "New", Height = 30, Width = 30 });
		}

		private void ComboBox_DropDownOpened(object sender, EventArgs e)
		{
			object o = sender;
			string a = "";

			//double m = sBar.Value;
			//sBar.Value = 1;
			if (combox1.IsDropDownOpen)
			{
				//ScrollViewer sViewer = FindScrollViewer(combox1.Parent);
				//sViewer.ScrollToTop();
				//sViewer.ScrollToBottom();
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			//combox1.ItemsSource = null;
			//combox1.ItemsSource = (new TestModel(500)).Names;
			//ScrollBar sBar=combox1.FindName("PART_VerticalScrollBar") as ScrollBar;
			//sBar = FindControl(combox1);
			//sBar.ValueChanged += combox1_DragLeave;
			//sBar.TouchLeave += sBar_TouchLeave;
			//sBar.Scroll += sBar_Scroll;
			//sBar.DragLeave += sBar_DragLeave;



			//ComboBoxItem item = combox1.ItemContainerGenerator.ContainerFromIndex(0) as ComboBoxItem;
			//if (item != null)
			//{
			//	item.BringIntoView();
			//}

			//if (this.combox1.SelectedIndex == -1)
			//{
				if (this.combox1.Items.Count > 0 && this.combox1.SelectedItem == null)
				{
					var sv = FindChildren<VirtualizingStackPanel>(this.combox1);
					if (sv != null && sv.Count > 0)
					{
						sv[0].BringIndexIntoViewPublic(0);
					}
				}
			//}
			
			if (this.combox1.Items.Count > 0 && this.combox1.SelectedItem == null)
			{
				var sv = FindChildren<ScrollViewer>(this.combox1);
				if (sv != null && sv.Count > 0)
				{
					sv[0].ScrollToEnd();
				}
			}

			string a = "";
		}

		public IList<T> FindChildren<T>(DependencyObject parent) where T : DependencyObject
		{
			var result = new List<T>();

			int count = VisualTreeHelper.GetChildrenCount(parent);
			for (int i = 0; i < count; i++)
			{
				DependencyObject child = VisualTreeHelper.GetChild(parent, i);
				if (child is T)
				{
					result.Add((T)child);
				}

				result.AddRange(FindChildren<T>(child));
			}

			return result;
		}

		void sBar_DragLeave(object sender, DragEventArgs e)
		{
			double m = sBar.Value;
		}

		void sBar_Scroll(object sender, ScrollEventArgs e)
		{
			double m = sBar.Value;
		}

		void sBar_TouchLeave(object sender, TouchEventArgs e)
		{
			double m = sBar.Value;
		}

		private void combox1_DragLeave(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			double m = sBar.Value;
		}

		private ScrollBar FindControl(DependencyObject obj)
		{
			ScrollBar sBar = null;
			tBox.AppendText(obj.GetType().ToString() + Environment.NewLine);
			if (obj is ScrollBar)
			{
				sBar = obj as ScrollBar;
				if (sBar.Orientation == Orientation.Vertical)
				{
					return sBar;
				}
			}

			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				sBar = FindControl(VisualTreeHelper.GetChild(obj, i));
				if (sBar != null)
				{
					return sBar;
				}
			}

			return null;
		}

		private ScrollViewer FindScrollViewer(DependencyObject obj)
		{
			ScrollViewer sViewer = null;
			//tBox.AppendText(obj.GetType().ToString() + Environment.NewLine);
			if (obj is ScrollViewer)
			{
				sViewer = obj as ScrollViewer;
				//if (!sViewer.Name.Equals("PART_ContentHost"))
				//{
				//return sViewer;
				//}
			}

			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				sViewer = FindScrollViewer(VisualTreeHelper.GetChild(obj, i));
				if (sViewer != null)
				{
					return sViewer;
				}
			}

			return null;
		}

		private void combox1_ScrollChanged(object sender, ScrollChangedEventArgs e)
		{
			if (_scvComboBox == null)
			{
				_scvComboBox = e.OriginalSource as ScrollViewer;
			}
			_scvComboBox.ScrollToBottom();
		}

		private ComboBoxItem GetComboxItem(ComboBox container)
		{
			if (container != null)
			{
				// Try to generate the ItemsPresenter and the ItemsPanel.
				// by calling ApplyTemplate.  Note that in the 
				// virtualizing case even if the item is marked 
				// expanded we still need to do this step in order to 
				// regenerate the visuals because they may have been virtualized away.

				container.ApplyTemplate();
				ItemsPresenter itemsPresenter = FindVisualChild<ItemsPresenter>(container);
				if (itemsPresenter == null)
				{
					container.UpdateLayout();

					itemsPresenter = FindVisualChild<ItemsPresenter>(container);
				}

				Panel itemsHostPanel = (Panel)VisualTreeHelper.GetChild(itemsPresenter, 0);

				// Ensure that the generator for this panel has been created.
				UIElementCollection children = itemsHostPanel.Children;

				VirtualizingStackPanel virtualizingPanel =
					itemsHostPanel as VirtualizingStackPanel;

				for (int i = 0, count = container.Items.Count; i < count; i++)
				{
					ComboBoxItem subContainer;
					if (virtualizingPanel != null)
					{
						// Bring the item into view so 
						// that the container will be generated.
						//virtualizingPanel.BringIntoView(i);

						subContainer =
							(ComboBoxItem)container.ItemContainerGenerator.
							ContainerFromIndex(i);
					}
					else
					{
						subContainer =
							(ComboBoxItem)container.ItemContainerGenerator.
							ContainerFromIndex(i);

						// Bring the item into view to maintain the 
						// same behavior as with a virtualizing panel.
						subContainer.BringIntoView();
					}
				}
			}

			return null;
		}

		private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj)
	   where T : DependencyObject
		{
			if (depObj != null)
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
					if (child != null && child is T)
					{
						yield return (T)child;
					}

					foreach (T childOfChild in FindVisualChildren<T>(child))
					{
						yield return childOfChild;
					}
				}
			}
		}

		private T FindVisualChild<T>(DependencyObject obj)
			where T : DependencyObject
		{
			foreach (T child in FindVisualChildren<T>(obj))
			{
				return child;
			}

			return null;
		}
	}

	public class TestModel
	{
		private List<string> names = new List<string>();

		public TestModel(int count)
		{
			for (int i = 0; i < count; i++)
			{
				names.Add(i.ToString());
			}
			DisplayName = "test";
		}

		public String DisplayName { get; set; }

		public ICommand ScrollChangedCommand { get; set; }

		public List<string> Names
		{
			get { return names; }
			set { names = value; }
		}
	}
}
