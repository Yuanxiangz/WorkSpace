using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GBKToUnicode
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void Convertbt_Click(object sender, EventArgs e)
		{
			//string hz = GBKTextBox.Text.ToString();
			//byte[] b = Encoding.Unicode.GetBytes(hz);
			//string o = "";
			//foreach (var x in b)
			//{
			//	o += string.Format("{0:X2}", x) + " ";
			//}
			//UnicodeTextBox.Text = o;

			string cd = GBKTextBox.Text.ToString();
			string cd2 = cd.Replace(" ", "");
			cd2 = cd2.Replace("\r", "");
			cd2 = cd2.Replace("\n", "");
			cd2 = cd2.Replace("\r\n", "");
			cd2 = cd2.Replace("\t", "");
			if (cd2.Length % 4 != 0)
			{
				MessageBox.Show("Unicode编码为双字节，请删多或补少！确保是二的倍数。");
			}
			else
			{
				int len = cd2.Length / 2;
				byte[] b = new byte[len];
				for (int i = 0; i < cd2.Length; i += 2)
				{
					string bi = cd2.Substring(i, 2);
					b[i / 2] = (byte)Convert.ToInt32(bi, 16);
				}
				string o = Encoding.Unicode.GetString(b);
				UnicodeTextBox.Text = o;
			}
		}
	}
}
