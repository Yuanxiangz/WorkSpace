using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Collections.Concurrent;

namespace AsyncTest
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			//timer.Tick += ChangeBtn2;
			//timer.Enabled = false;
		}

		private async void button1_Click(object sender, EventArgs e)
		{
			Output(DateTime.Now.ToLongTimeString());
			string str = await Wait();
			Output(DateTime.Now.ToLongTimeString() + ":" + str);
			button1.Text = "I changed";
			Output(DateTime.Now.ToLongTimeString() + ":" + button1.Text);
			Thread.Sleep(2000);
			Output(DateTime.Now.ToLongTimeString() + ": Thread.Sleep(2000)");
			button1.Text = str;
			Output(DateTime.Now.ToLongTimeString() + ":" + button1.Text);
		}

		private Task<string> GetResult()
		{
			return Task<string>.Run(() =>
			{

				Thread.Sleep(5000);
				//Output(Thread.CurrentThread.GetHashCode().ToString() + DateTime.Now.ToLongTimeString() + ": GetResult");
				return "Good";
			}
			);
		}

		private async Task<string> Wait()
		{

			//Task.Run(() =>
			//{
			//	Thread.Sleep(4000);
			//	//Output(Thread.CurrentThread.GetHashCode().ToString() + DateTime.Now.ToLongTimeString() + ": Wait");
			//}
			//);

			return await GetResult();
			//MessageBox.Show("go");
		}

		private void Output(string str)
		{
			using (StreamWriter sw = new StreamWriter(@"D:\a.txt", true))
			{
				sw.WriteLine(str);
			}
		}

		//System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
		
		static int count = 0;
		private void button2_Click(object sender, EventArgs e)
		{
			System.Threading.Timer timer = new System.Threading.Timer(ChangeBtn2);
			timer.Change(1000, 0);
		}

		delegate void timerDelegate(object state);

		private void ChangeBtn2(object sender)
		{
			if (this.InvokeRequired)
			{
				object[] objs=new object[1];
				this.Invoke(new timerDelegate(ChangeBtn2), objs);
			}
			else
			{
				button2.Text = count.ToString();
				count++;
			}
		}

		BlockingCollection<object> collection = new BlockingCollection<object>();
		private void button3_Click(object sender, EventArgs e)
		{
			
		}
	}
}
