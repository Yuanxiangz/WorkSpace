namespace GBKToUnicode
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
			this.GBKTextBox = new System.Windows.Forms.TextBox();
			this.UnicodeTextBox = new System.Windows.Forms.TextBox();
			this.Convertbt = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// GBKTextBox
			// 
			this.GBKTextBox.Location = new System.Drawing.Point(63, 90);
			this.GBKTextBox.Multiline = true;
			this.GBKTextBox.Name = "GBKTextBox";
			this.GBKTextBox.Size = new System.Drawing.Size(315, 206);
			this.GBKTextBox.TabIndex = 0;
			// 
			// UnicodeTextBox
			// 
			this.UnicodeTextBox.Location = new System.Drawing.Point(523, 90);
			this.UnicodeTextBox.Multiline = true;
			this.UnicodeTextBox.Name = "UnicodeTextBox";
			this.UnicodeTextBox.Size = new System.Drawing.Size(315, 206);
			this.UnicodeTextBox.TabIndex = 1;
			// 
			// Convertbt
			// 
			this.Convertbt.Location = new System.Drawing.Point(416, 176);
			this.Convertbt.Name = "Convertbt";
			this.Convertbt.Size = new System.Drawing.Size(75, 23);
			this.Convertbt.TabIndex = 2;
			this.Convertbt.Text = "Convert";
			this.Convertbt.UseVisualStyleBackColor = true;
			this.Convertbt.Click += new System.EventHandler(this.Convertbt_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(60, 62);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(45, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Chinese";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(523, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(47, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Unicode";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(916, 413);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.Convertbt);
			this.Controls.Add(this.UnicodeTextBox);
			this.Controls.Add(this.GBKTextBox);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox GBKTextBox;
		private System.Windows.Forms.TextBox UnicodeTextBox;
		private System.Windows.Forms.Button Convertbt;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
	}
}

