using System;
using System.Windows.Forms;
using CITray.Controllers;

namespace CITray.UI.Options
{
    /// <summary>
    /// Options Dialog.
    /// </summary>
    internal partial class OptionsDialog : Form
    {
        private IServiceProvider services = null;
        private IOptionsController controller = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsDialog"/> class.
        /// </summary>
        public OptionsDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initializes this instance with the specified service provider.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public void Initialize(IServiceProvider serviceProvider)
        {
            services = serviceProvider ?? This.Services; 
            controller = services.GetService<IOptionsController>(true);

            optionsTreeControl.SectionSelected += (s, e) =>
            {
                var panel = e.Panel;
                placeHolderPanel.Controls.Clear();
                placeHolderPanel.Controls.Add(panel);
            };

            optionsTreeControl.Initialize(services);
        }
    }
}
