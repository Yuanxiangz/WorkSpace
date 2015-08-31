namespace DevExpChat
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel1 = new DevExpress.XtraCharts.PieSeriesLabel();
			DevExpress.XtraCharts.PiePointOptions piePointOptions1 = new DevExpress.XtraCharts.PiePointOptions();
			DevExpress.XtraCharts.PieSeriesView pieSeriesView1 = new DevExpress.XtraCharts.PieSeriesView();
			DevExpress.XtraCharts.PieSeriesLabel pieSeriesLabel2 = new DevExpress.XtraCharts.PieSeriesLabel();
			DevExpress.XtraCharts.PieSeriesView pieSeriesView2 = new DevExpress.XtraCharts.PieSeriesView();
			DevExpress.XtraCharts.ChartTitle chartTitle1 = new DevExpress.XtraCharts.ChartTitle();
			DevExpress.XtraCharts.XYDiagram xyDiagram1 = new DevExpress.XtraCharts.XYDiagram();
			DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel1 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.Series series3 = new DevExpress.XtraCharts.Series();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel2 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.SideBySideBarSeriesLabel sideBySideBarSeriesLabel3 = new DevExpress.XtraCharts.SideBySideBarSeriesLabel();
			DevExpress.XtraCharts.ChartTitle chartTitle2 = new DevExpress.XtraCharts.ChartTitle();
			this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
			this.chartControl2 = new DevExpress.XtraCharts.ChartControl();
			((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(pieSeriesLabel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.chartControl2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(xyDiagram1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(series3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).BeginInit();
			this.SuspendLayout();
			// 
			// chartControl1
			// 
			this.chartControl1.Dock = System.Windows.Forms.DockStyle.Right;
			this.chartControl1.Location = new System.Drawing.Point(496, 0);
			this.chartControl1.Name = "chartControl1";
			this.chartControl1.PaletteName = "In A Fog";
			pieSeriesLabel1.LineVisible = true;
			series1.Label = pieSeriesLabel1;
			series1.Name = "Series 1";
			piePointOptions1.Pattern = "{A} = {V}";
			piePointOptions1.PointView = DevExpress.XtraCharts.PointView.ArgumentAndValues;
			piePointOptions1.ValueNumericOptions.Format = DevExpress.XtraCharts.NumericFormat.Percent;
			piePointOptions1.ValueNumericOptions.Precision = 0;
			series1.PointOptions = piePointOptions1;
			pieSeriesView1.RuntimeExploding = false;
			series1.View = pieSeriesView1;
			this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1};
			pieSeriesLabel2.LineVisible = true;
			this.chartControl1.SeriesTemplate.Label = pieSeriesLabel2;
			pieSeriesView2.RuntimeExploding = false;
			this.chartControl1.SeriesTemplate.View = pieSeriesView2;
			this.chartControl1.Size = new System.Drawing.Size(434, 415);
			this.chartControl1.TabIndex = 0;
			chartTitle1.Text = "IOI Coverage %";
			this.chartControl1.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle1});
			this.chartControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.chartControl1_MouseClick);
			// 
			// chartControl2
			// 
			xyDiagram1.AxisX.Range.ScrollingRange.SideMarginsEnabled = true;
			xyDiagram1.AxisX.Range.SideMarginsEnabled = true;
			xyDiagram1.AxisX.VisibleInPanesSerializable = "-1";
			xyDiagram1.AxisY.NumericOptions.Precision = 0;
			xyDiagram1.AxisY.Range.ScrollingRange.SideMarginsEnabled = true;
			xyDiagram1.AxisY.Range.SideMarginsEnabled = true;
			xyDiagram1.AxisY.Visible = false;
			xyDiagram1.AxisY.VisibleInPanesSerializable = "-1";
			xyDiagram1.Rotated = true;
			this.chartControl2.Diagram = xyDiagram1;
			this.chartControl2.Dock = System.Windows.Forms.DockStyle.Left;
			this.chartControl2.Location = new System.Drawing.Point(0, 0);
			this.chartControl2.Name = "chartControl2";
			this.chartControl2.PaletteName = "In A Fog";
			sideBySideBarSeriesLabel1.LineVisible = true;
			series2.Label = sideBySideBarSeriesLabel1;
			series2.Name = "IOIs Received";
			sideBySideBarSeriesLabel2.LineVisible = true;
			series3.Label = sideBySideBarSeriesLabel2;
			series3.Name = "IOIs Traded";
			this.chartControl2.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series2,
        series3};
			sideBySideBarSeriesLabel3.LineVisible = true;
			this.chartControl2.SeriesTemplate.Label = sideBySideBarSeriesLabel3;
			this.chartControl2.SideBySideBarDistanceFixed = 0;
			this.chartControl2.Size = new System.Drawing.Size(455, 415);
			this.chartControl2.TabIndex = 1;
			chartTitle2.Text = "IOIs Received/Traded";
			this.chartControl2.Titles.AddRange(new DevExpress.XtraCharts.ChartTitle[] {
            chartTitle2});
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(930, 415);
			this.Controls.Add(this.chartControl2);
			this.Controls.Add(this.chartControl1);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(pieSeriesLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(pieSeriesView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(pieSeriesLabel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(pieSeriesView2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(xyDiagram1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(series3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(sideBySideBarSeriesLabel3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.chartControl2)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraCharts.ChartControl chartControl1;
		private DevExpress.XtraCharts.ChartControl chartControl2;
		//private DevExpress.XtraBars.PopupMenu popupMenu;
	}
}

