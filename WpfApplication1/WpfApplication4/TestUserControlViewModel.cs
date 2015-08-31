using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication4
{
	public class TestUserControlViewModel
	{
		List<string> allocationMethods = new List<string>();
		TestClass tClass = new TestClass();
		List<Item> items = new List<Item>();
		TextItem textItem = new TextItem();

		public TestUserControlViewModel()
		{
			allocationMethods.Add("Test1");
			allocationMethods.Add("Test2");
			allocationMethods.Add("Test3");
			allocationMethods.Add("Test4");
			allocationMethods.Add("Test5");

			AllocationMethod = "Test4";
			tClass.Name = "Test";

			items.Add(new Item() { ID = "1", Name = "Tom" });
			items.Add(new Item() { ID = "2", Name = "Jim" });
			textItem.Content = "Text";
		}

		public List<string> AllocationMethods
		{
			get { return allocationMethods; }
			set { allocationMethods = value; }
		}

		public List<Item> Items
		{
			get { return items; }
			set { items = value; }
		}

		public string AllocationMethod { get; set; }

		public ICommand CMD
		{
			get;
			set;
		}

		public TextItem TextItem
		{
			get { return textItem; }
			set { textItem = value; }
		}

		public TestClass TClass
		{
			get { return tClass; }
			set { tClass = value; }
		}
	}

	public class Item
	{
		public string ID { get; set; }
		public string Name { get; set; }
	}

	public class TextItem
	{
		public string Content { get; set; }
	}
}
