using System;
using System.Windows.Forms;

using CITray.UI;
using CITray.Controllers;

namespace CITray
{
    /// <summary>
    /// The application's main window.
    /// </summary>
    internal partial class MainForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            Text = ThisAssembly.MainWindowTitle;
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
            exitAction.Run += (s, e) => controller.ExitApplication();
            optionsAction.Run += (s, e) => controller.ShowOptions();
            aboutAction.Run += (s, e) => controller.AboutApplication();
#if !DEBUG
            FormClosing += (s, e) =>
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    controller.HideMainWindow();
                    e.Cancel = true; // don't close
                }
            };
#endif
        }       
    }
}
