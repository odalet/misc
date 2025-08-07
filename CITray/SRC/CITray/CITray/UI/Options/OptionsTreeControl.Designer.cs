namespace CITray.UI.Options
{
    partial class OptionsTreeControl
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
            this.optionsTree = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // optionsTree
            // 
            this.optionsTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.optionsTree.Location = new System.Drawing.Point(0, 0);
            this.optionsTree.Name = "optionsTree";
            this.optionsTree.ShowRootLines = false;
            this.optionsTree.Size = new System.Drawing.Size(246, 391);
            this.optionsTree.TabIndex = 0;
            // 
            // OptionsTreeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.optionsTree);
            this.Name = "OptionsTreeControl";
            this.Size = new System.Drawing.Size(246, 391);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView optionsTree;
    }
}
