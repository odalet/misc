namespace CITray.Hudson.UI
{
    partial class SelectProjectsControl
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
            this.lineControl1 = new CITray.UI.LineControl();
            this.lineControl2 = new CITray.UI.LineControl();
            this.projectsListControl1 = new CITray.Hudson.UI.ProjectsListControl();
            this.addServerControl1 = new CITray.Hudson.UI.AddServerControl();
            this.SuspendLayout();
            // 
            // lineControl1
            // 
            this.lineControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lineControl1.Location = new System.Drawing.Point(0, 0);
            this.lineControl1.Name = "lineControl1";
            this.lineControl1.Size = new System.Drawing.Size(843, 12);
            this.lineControl1.TabIndex = 0;
            this.lineControl1.TabStop = false;
            this.lineControl1.Text = "Choose a Hudson server";
            this.lineControl1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(128)))), ((int)(((byte)(186)))));
            // 
            // lineControl2
            // 
            this.lineControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.lineControl2.Location = new System.Drawing.Point(0, 39);
            this.lineControl2.Name = "lineControl2";
            this.lineControl2.Size = new System.Drawing.Size(843, 12);
            this.lineControl2.TabIndex = 0;
            this.lineControl2.TabStop = false;
            this.lineControl2.Text = "Choose projects to monitor";
            this.lineControl2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(128)))), ((int)(((byte)(186)))));
            // 
            // projectsListControl1
            // 
            this.projectsListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.projectsListControl1.Location = new System.Drawing.Point(0, 51);
            this.projectsListControl1.Name = "projectsListControl1";
            this.projectsListControl1.Size = new System.Drawing.Size(843, 323);
            this.projectsListControl1.TabIndex = 2;
            // 
            // addServerControl1
            // 
            this.addServerControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.addServerControl1.Location = new System.Drawing.Point(0, 12);
            this.addServerControl1.MinimumSize = new System.Drawing.Size(250, 27);
            this.addServerControl1.Name = "addServerControl1";
            this.addServerControl1.Size = new System.Drawing.Size(843, 27);
            this.addServerControl1.TabIndex = 1;
            // 
            // SelectProjectsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.projectsListControl1);
            this.Controls.Add(this.lineControl2);
            this.Controls.Add(this.addServerControl1);
            this.Controls.Add(this.lineControl1);
            this.Name = "SelectProjectsControl";
            this.Size = new System.Drawing.Size(843, 374);
            this.ResumeLayout(false);

        }

        #endregion

        private CITray.UI.LineControl lineControl1;
        private AddServerControl addServerControl1;
        private CITray.UI.LineControl lineControl2;
        private ProjectsListControl projectsListControl1;
    }
}
