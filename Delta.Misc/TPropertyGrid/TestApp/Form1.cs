using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace TestApp
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private TPropertyGrid.TPropertyGrid tpg;
		private System.Windows.Forms.CheckBox cbEn;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tpg = new TPropertyGrid.TPropertyGrid();
			this.cbEn = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// tpg
			// 
			this.tpg.CommandsVisibleIfAvailable = true;
			this.tpg.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tpg.LargeButtons = false;
			this.tpg.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.tpg.Location = new System.Drawing.Point(0, 0);
			this.tpg.Name = "tpg";
			this.tpg.Size = new System.Drawing.Size(504, 366);
			this.tpg.TabIndex = 0;
			this.tpg.Text = "PropertyGrid";
			this.tpg.ViewBackColor = System.Drawing.SystemColors.Window;
			this.tpg.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// cbEn
			// 
			this.cbEn.Checked = true;
			this.cbEn.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cbEn.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.cbEn.Location = new System.Drawing.Point(0, 366);
			this.cbEn.Name = "cbEn";
			this.cbEn.Size = new System.Drawing.Size(504, 24);
			this.cbEn.TabIndex = 1;
			this.cbEn.Text = "English?";
			this.cbEn.CheckedChanged += new System.EventHandler(this.cbEn_CheckedChanged);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(504, 390);
			this.Controls.Add(this.tpg);
			this.Controls.Add(this.cbEn);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.EnableVisualStyles();
			Application.DoEvents();
			Application.Run(new Form1());
		}


		private TestObject test = new TestObject();

		private void Form1_Load(object sender, System.EventArgs e)
		{
			tpg.SelectedObject = test;
		}

		private void cbEn_CheckedChanged(object sender, System.EventArgs e)
		{
			TPropertyGrid.T.UseEnglish = cbEn.Checked;
			tpg.Refresh();
		}
	}
}
