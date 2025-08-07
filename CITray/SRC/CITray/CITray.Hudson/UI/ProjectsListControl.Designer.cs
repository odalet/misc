namespace CITray.Hudson.UI
{
    partial class ProjectsListControl
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
            this.projectsListView = new System.Windows.Forms.ListView();
            this.nameHeader = new System.Windows.Forms.ColumnHeader();
            this.selectAllButton = new System.Windows.Forms.Button();
            this.unselectAllButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // projectsListView
            // 
            this.projectsListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.projectsListView.CheckBoxes = true;
            this.projectsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameHeader});
            this.projectsListView.Location = new System.Drawing.Point(3, 3);
            this.projectsListView.Name = "projectsListView";
            this.projectsListView.Size = new System.Drawing.Size(483, 154);
            this.projectsListView.TabIndex = 0;
            this.projectsListView.UseCompatibleStateImageBehavior = false;
            this.projectsListView.View = System.Windows.Forms.View.Details;
            // 
            // nameHeader
            // 
            this.nameHeader.Text = "Name";
            this.nameHeader.Width = 86;
            // 
            // selectAllButton
            // 
            this.selectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.selectAllButton.Location = new System.Drawing.Point(3, 163);
            this.selectAllButton.Name = "selectAllButton";
            this.selectAllButton.Size = new System.Drawing.Size(75, 23);
            this.selectAllButton.TabIndex = 1;
            this.selectAllButton.Text = "Select &All";
            this.selectAllButton.UseVisualStyleBackColor = true;
            // 
            // unselectAllButton
            // 
            this.unselectAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.unselectAllButton.Location = new System.Drawing.Point(84, 163);
            this.unselectAllButton.Name = "unselectAllButton";
            this.unselectAllButton.Size = new System.Drawing.Size(75, 23);
            this.unselectAllButton.TabIndex = 1;
            this.unselectAllButton.Text = "&Unselect All";
            this.unselectAllButton.UseVisualStyleBackColor = true;
            // 
            // ProjectsListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.unselectAllButton);
            this.Controls.Add(this.selectAllButton);
            this.Controls.Add(this.projectsListView);
            this.Name = "ProjectsListControl";
            this.Size = new System.Drawing.Size(489, 189);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView projectsListView;
        private System.Windows.Forms.ColumnHeader nameHeader;
        private System.Windows.Forms.Button selectAllButton;
        private System.Windows.Forms.Button unselectAllButton;
    }
}
