using System;

namespace CITray.Controllers
{
    /// <summary>
    /// Defines the application-wide actions.
    /// </summary>
    internal interface IApplicationController
    {
        /// <summary>
        /// Gets the application's main window instance.
        /// </summary>
        MainForm MainWindow { get; }

        /// <summary>
        /// Exits the application.
        /// </summary>
        void ExitApplication();

        /// <summary>
        /// Shows the application About box.
        /// </summary>
        void AboutApplication();

        /// <summary>
        /// Shows the options dialog.
        /// </summary>
        void ShowOptions();

        /// <summary>
        /// Shows the application's main window.
        /// </summary>
        void ShowMainWindow();

        /// <summary>
        /// Hides the application's main window.
        /// </summary>
        void HideMainWindow();
    }
}
