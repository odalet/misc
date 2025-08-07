namespace CITray.UI.Options
{
    partial class OptionsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsDialog));
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.placeHolderPanel = new System.Windows.Forms.Panel();
            this.bottomLineControl = new CITray.UI.LineControl();
            this.optionsTreeControl = new CITray.UI.Options.OptionsTreeControl();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point(416, 327);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "&OK";
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(497, 327);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 3;
            this.cancelButton.Text = "&Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.Location = new System.Drawing.Point(12, 12);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.optionsTreeControl);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.placeHolderPanel);
            this.splitContainer.Size = new System.Drawing.Size(560, 303);
            this.splitContainer.SplitterDistance = 186;
            this.splitContainer.TabIndex = 0;
            // 
            // placeHolderPanel
            // 
            this.placeHolderPanel.BackColor = System.Drawing.Color.White;
            this.placeHolderPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.placeHolderPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.placeHolderPanel.Location = new System.Drawing.Point(0, 0);
            this.placeHolderPanel.Name = "placeHolderPanel";
            this.placeHolderPanel.Size = new System.Drawing.Size(370, 303);
            this.placeHolderPanel.TabIndex = 0;
            // 
            // bottomLineControl
            // 
            this.bottomLineControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.bottomLineControl.Location = new System.Drawing.Point(12, 318);
            this.bottomLineControl.Margin = new System.Windows.Forms.Padding(0);
            this.bottomLineControl.Name = "bottomLineControl";
            this.bottomLineControl.Size = new System.Drawing.Size(560, 6);
            this.bottomLineControl.TabIndex = 1;
            this.bottomLineControl.TabStop = false;
            this.bottomLineControl.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(128)))), ((int)(((byte)(186)))));
            // 
            // optionsTreeControl
            // 
            this.optionsTreeControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsTreeControl.Location = new System.Drawing.Point(0, 0);
            this.optionsTreeControl.Name = "optionsTreeControl";
            this.optionsTreeControl.Size = new System.Drawing.Size(186, 303);
            this.optionsTreeControl.TabIndex = 0;
            // 
            // OptionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.bottomLineControl);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OptionsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Options";
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.SplitContainer splitContainer;
        private CITray.UI.Options.OptionsTreeControl optionsTreeControl;
        private CITray.UI.LineControl bottomLineControl;
        private System.Windows.Forms.Panel placeHolderPanel;
    }
}