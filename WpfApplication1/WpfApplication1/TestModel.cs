using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApplication1
{
	public class TestModel
	{
		public TestModel()
		{
			BlotterCommandList = new List<BlotterCommand>();
			BlotterCommandList.Add(new BlotterCommand() { CommandType = BlotterCommandType.Create, CommandHandler = () => MessageBox.Show("Create button!"), LayOut = BlotterCommandLayOut.Botton });
			BlotterCommandList.Add(new BlotterCommand() { CommandType = BlotterCommandType.Delete, CommandHandler = () => MessageBox.Show("Delete button!"), LayOut = BlotterCommandLayOut.Botton });

			Elements = new List<object>();
			Elements.Add(new Car() { Name = "Benz" });
			Elements.Add(new Desk() { Name = "HighDesk" });
		}

		public List<BlotterCommand> BlotterCommandList { get; set; }

		public List<object> Elements { get; set; }
	}

	public class TestCommand : ICommand
	{
		//A method prototype without return value.
		private Action<object> ExecuteCommand = null;
		//A method prototype return a bool type.
		public Func<object, bool> CanExecuteCommand = null;
		public event EventHandler CanExecuteChanged;

		public TestCommand(Action<object> action)
		{
			ExecuteCommand = action;
		}

		public bool CanExecute(object parameter)
		{
			if (CanExecuteCommand != null)
			{
				return this.CanExecuteCommand(parameter);
			}
			else
			{
				return true;
			}
		}

		public void Execute(object parameter)
		{
			if (this.ExecuteCommand != null) this.ExecuteCommand(parameter);
		}

		public void RaiseCanExecuteChanged()
		{
			if (CanExecuteChanged != null)
			{
				CanExecuteChanged(this, EventArgs.Empty);
			}
		}
	}
}
