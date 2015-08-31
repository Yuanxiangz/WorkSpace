using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraCharts;
using DevExpress.XtraBars;
using DevExpress.XtraCharts.Design;

namespace DevExpChat
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			ChartInit();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private List<TestItem> GetBrokerItem()
		{
			SqlConnection connection = new SqlConnection(@"Data Source=yuanxz7;Initial Catalog=tc;User ID=sa;Password=sa");
			string sqlstr = @"select VirtualConnectionid, Count(VirtualConnectionid) as Count from ProcessedIOI group by VirtualConnectionid";
			DataSet ds = new DataSet();

			try
			{
				using (SqlDataAdapter adapter = new SqlDataAdapter(sqlstr, connection))
				{
					adapter.Fill(ds);
				}
			}
			catch (SqlException ex)
			{
				string a = "";
			}
			finally
			{
				if ((connection != null) && (connection.State != (ConnectionState.Closed | ConnectionState.Broken)))
				{
					connection.Close();
				}
			}

			List<TestItem> testItem = new List<TestItem>();
			foreach (DataRow row in ds.Tables[0].Rows)
			{
				testItem.Add(new TestItem() { ColumnName = row["VirtualConnectionid"].ToString(), Count = (int)row["Count"] });
			}

			testItem.Add(new TestItem() { ColumnName = "BUY", Count = 15 });
			testItem.Add(new TestItem() { ColumnName = "FlEX", Count = 5 });

			return testItem;
		}

		private List<TestItem> GetIOIsTradedItem()
		{
			SqlConnection connection = new SqlConnection(@"Data Source=yuanxz7;Initial Catalog=tc;User ID=sa;Password=sa");
			string sqlstr = @"select (select COUNT(*) from ProcessedIOI) as TotalTraded,"
				+ "(select COUNT(*) from ProcessedIOI where Symbol in (select distinct T_Sec from tc_trade) and ActionableIOI='Y') as ActionableTraded,"
				+ "(select COUNT(*) from ProcessedIOI where Symbol in (select distinct T_Sec from tc_trade) and IOINaturalFlag='Y') as NaturalTraded,"
				+ "(select COUNT(*) from ProcessedIOI where Symbol in (select distinct T_Sec from tc_trade) and IOICustom1 is not null) as CustomTraded";
			DataSet ds = new DataSet();

			try
			{
				using (SqlDataAdapter adapter = new SqlDataAdapter(sqlstr, connection))
				{
					adapter.Fill(ds);
				}
			}
			catch (SqlException ex)
			{
				string a = "";
			}
			finally
			{
				if ((connection != null) && (connection.State != (ConnectionState.Closed | ConnectionState.Broken)))
				{
					connection.Close();
				}
			}

			List<TestItem> testItem = new List<TestItem>();
			testItem.Add(new TestItem() { ColumnName = "Total", Count = 4500 });
			testItem.Add(new TestItem() { ColumnName = "ActionableIOI", Count = 1500 });
			testItem.Add(new TestItem() { ColumnName = "IOINaturalFlag", Count = 500 });
			testItem.Add(new TestItem() { ColumnName = "IOICustom", Count = 3000 });

			return testItem;
		}

		private List<TestItem> GetIOIsReceivedItem()
		{
			List<TestItem> testItem = new List<TestItem>();
			testItem.Add(new TestItem() { ColumnName = "IOICustom", Count = 4000 });
			testItem.Add(new TestItem() { ColumnName = "IOINaturalFlag", Count = 1500 });
			testItem.Add(new TestItem() { ColumnName = "ActionableIOI", Count = 2500 });
			testItem.Add(new TestItem() { ColumnName = "Total", Count = 8500 });

			return testItem;
		}

		private void ChartInit()
		{
			this.chartControl1.Series["Series 1"].DataSource = GetBrokerItem();
			this.chartControl1.Series["Series 1"].ArgumentDataMember = "ColumnName";
			this.chartControl1.Series["Series 1"].ValueDataMembers.AddRange(new string[] { "Count" });

			this.chartControl2.Series["IOIs Traded"].DataSource = GetIOIsTradedItem();
			this.chartControl2.Series["IOIs Traded"].ArgumentDataMember = "ColumnName";
			this.chartControl2.Series["IOIs Traded"].ValueDataMembers.AddRange(new string[] { "Count" });

			this.chartControl2.Series["IOIs Received"].DataSource = GetIOIsReceivedItem();
			this.chartControl2.Series["IOIs Received"].ArgumentDataMember = "ColumnName";
			this.chartControl2.Series["IOIs Received"].ValueDataMembers.AddRange(new string[] { "Count" });
			//SeriesPoint t1 = new SeriesPoint("test1", new object[] { 12 });
			//SeriesPoint t2 = new SeriesPoint("test2", new object[] { 10 });
			//chartControl1.Series[0].Points.Clear();
			//chartControl1.Series[0].Points.AddRange(new SeriesPoint[] { t1, t2 });
		}

		private void chartControl1_MouseClick(object sender, MouseEventArgs e)
		{
			if(e.Button==MouseButtons.Right)
			{
				//popupMenu.ShowPopup(e.Location);
				//BarItem item = new BarButtonItem();
				//item.Caption = "PaletteEditor";
				//item.Name = "PaletteEditor";
				//popupMenu.AddItem(item);
				using (DevExpress.XtraCharts.Design.PaletteEditorForm paletteEditor = new DevExpress.XtraCharts.Design.PaletteEditorForm(chartControl1.PaletteRepository))
				{
					paletteEditor.CurrentPalette = chartControl1.PaletteRepository[chartControl1.PaletteName];
					paletteEditor.ShowDialog();
					chartControl1.PaletteName = paletteEditor.CurrentPalette.Name;
				}
			}
		}
	}

	public class TestItem
	{
		public string ColumnName { get; set; }
		public int Count { get; set; }
	}
}
