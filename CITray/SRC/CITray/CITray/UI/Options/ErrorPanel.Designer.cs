namespace CITray.UI.Options
{
    partial class ErrorPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtb = new System.Windows.Forms.RichTextBox();
            this.lineControl1 = new CITray.UI.LineControl();
            this.SuspendLayout();
            // 
            // rtb
            // 
            this.rtb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb.Location = new System.Drawing.Point(0, 12);
            this.rtb.Name = "rtb";
            this.rtb.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.rtb.ShowSelectionMargin = true;
            this.rtb.Size = new System.Drawing.Size(475, 342);
            this.rtb.TabIndex = 1;
            this.rtb.Text = "";
            this.rtb.WordWrap = false;
            // 
            // lineControl1
            // 
            this.lineControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lineControl1.Location = new System.Drawing.Point(0, 0);
            this.lineControl1.Name = "lineControl1";
            this.lineControl1.Size = new System.Drawing.Size(475, 12);
            this.lineControl1.TabIndex = 2;
            this.lineControl1.TabStop = false;
            this.lineControl1.Text = "Error";
            this.lineControl1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(128)))), ((int)(((byte)(186)))));
            // 
            // ErrorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rtb);
            this.Controls.Add(this.lineControl1);
            this.Name = "ErrorPanel";
            this.Size = new System.Drawing.Size(475, 354);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtb;
        private LineControl lineControl1;
    }
}
