using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
	public partial class Form1 : Form
	{
		static Form1 f1;
		System.Threading.Timer timer;
		bool go;
		public Form1()
		{
			InitializeComponent();
			System.Threading.TimerCallback timerCallback = delegate
			{
				timer.Change(-1, 0);
				System.Threading.Thread.Sleep(5000);
				if (this.button1.InvokeRequired)
				{
					this.Invoke(new timetick(change));
				}
				else
					change();

				timer.Change(0, 1000);
			};
			//timer = new System.Threading.Timer(timerCallback, null, 0, 1000);
		}

		private void button1_Click(object sender, EventArgs e)
		{
			this.button1.Text = "A";

			System.Threading.Timer t = new System.Threading.Timer((o) => { }, null, 0, 1000);
			t.Dispose();
			string a = "";
		}

		private delegate void timetick();
		private void timer_Tick(object sender, EventArgs e)
		{
			System.Threading.Thread.Sleep(30000);
			if (this.button1.InvokeRequired)
			{
				this.Invoke(new timetick(change));
			}
			else
				change();
		}

		private void change()
		{
			this.button1.Text = this.button1.Text.Equals("A") ? "B" : "A";
		}

		private void button2_Click(object sender, EventArgs e)
		{
			go = go ? false : true;
			timer.Change(0, 0);
		}
	}
}
