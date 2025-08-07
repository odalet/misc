using System;
using System.Windows.Forms;
using System.ComponentModel;

using CITray.Controllers;

namespace CITray.UI
{
    /// <summary>
    /// Wraps the application's <see cref="System.Windows.Forms.NotifyIcon"/>.
    /// </summary>
    internal sealed class TrayIcon : Component //IDisposable
    {
        private ContextMenuStrip cstrip;
        private NotifyIcon notifyIcon;
        private ToolStripMenuItem showMenuItem;
        private ToolStripMenuItem aboutMenuItem;
        private ToolStripMenuItem exitMenuItem;
        private ToolStripMenuItem optionsMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private IContainer components;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrayIcon"/> class.
        /// </summary>
        public TrayIcon()
        {
            InitializeComponent();  
        }

        /// <summary>
        /// Gets or sets a value indicating whether the icon is visible in the system tray.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        public bool Visible
        {
            get { return notifyIcon.Visible; }
            set { notifyIcon.Visible = true; }
        }

        /// <summary>
        /// Initializes this instance of <see cref="TrayIcon"/> with the specified service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public void Initialize(IServiceProvider serviceProvider)
        {
            var services = serviceProvider ?? This.Services;

            // Get a reference to the application controller
            var controller = services.GetService<IApplicationController>(true);

            // Wire events
            exitMenuItem.Click += (s, e) => controller.ExitApplication();
            aboutMenuItem.Click += (s, e) => controller.AboutApplication();
            optionsMenuItem.Click += (s, e) => controller.ShowOptions();
            showMenuItem.Click += (s, e) => controller.ShowMainWindow();
            notifyIcon.MouseDoubleClick += (s, e) => controller.ShowMainWindow();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TrayIcon));
            this.cstrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.optionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.cstrip.SuspendLayout();
            // 
            // cstrip
            // 
            this.cstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showMenuItem,
            this.optionsMenuItem,
            this.aboutMenuItem,
            this.toolStripSeparator,
            this.exitMenuItem});
            this.cstrip.Name = "cstrip";
            this.cstrip.Size = new System.Drawing.Size(153, 98);
            // 
            // showMenuItem
            // 
            this.showMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showMenuItem.Name = "showMenuItem";
            this.showMenuItem.Size = new System.Drawing.Size(152, 22);
            this.showMenuItem.Text = "&Show CITray...";
            // 
            // aboutMenuItem
            // 
            this.aboutMenuItem.Name = "aboutMenuItem";
            this.aboutMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutMenuItem.Text = "&About...";
            // 
            // exitMenuItem
            // 
            this.exitMenuItem.Name = "exitMenuItem";
            this.exitMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitMenuItem.Text = "E&xit";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "CITray";
            this.notifyIcon.ContextMenuStrip = this.cstrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "CITray";
            this.notifyIcon.Visible = true;
            // 
            // optionsMenuItem
            // 
            this.optionsMenuItem.Name = "optionsMenuItem";
            this.optionsMenuItem.Size = new System.Drawing.Size(152, 22);
            this.optionsMenuItem.Text = "&Options...";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(149, 6);
            this.cstrip.ResumeLayout(false);

        }
    }
}
