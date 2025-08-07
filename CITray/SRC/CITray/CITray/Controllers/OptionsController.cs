using System;
using System.Windows.Forms;
using System.Collections.Generic;

using CITray.Api.UI;

using CITray.UI;
using CITray.UI.Options;

namespace CITray.Controllers
{
    internal class OptionsController : BaseController, IOptionsController
    {
        private bool initialized = false;
        private List<OptionsSection> topLevelSections = null;
        private Dictionary<string, Func<BaseOptionsPanel>> panels = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsController"/> class.
        /// </summary>
        /// <param name="serviceProvider">The service provider.</param>
        public OptionsController(IServiceProvider serviceProvider) : base(serviceProvider) { }
        
        #region IOptionsController Members

        /// <summary>
        /// Gets an instance of the options dialog.
        /// </summary>
        /// <returns>A <see cref="CITray.UI.OptionsDialog"/> object.</returns>
        public OptionsDialog GetOptionsDialog()
        {
            if (!initialized) Initialize();

            var dialog = new OptionsDialog();
            dialog.Initialize(this);
            return dialog;
        }

        public IEnumerable<OptionsSection> TopLevelSections
        {
            get { return topLevelSections; }
        }

        #endregion

        private void Initialize()
        {
            if (initialized) return;

            // Sections & Panels
            var environnmentSection = new OptionsSection("Environment");
            var pluginsSection = new OptionsSection("Plugins");

            topLevelSections = new List<OptionsSection>() 
            {
                environnmentSection,
                pluginsSection
            };

            var generalSection = new OptionsSection("General");
            generalSection.PanelBuilder = () => CreatePanel<GeneralPanel>();
            environnmentSection.AddChildSection(generalSection);

            var projectsSection = new OptionsSection("Projects");
            projectsSection.PanelBuilder = () => CreatePanel<ProjectsPanel>();
            environnmentSection.AddChildSection(projectsSection);

            foreach (var descriptor in This.PluginManager.DiscoveredPlugins)
            {
                var pluginSection = new OptionsSection(descriptor.PluginDisplayName);
                pluginsSection.AddChildSection(pluginSection);
            }

            initialized = true;
        }

        private T CreatePanel<T>() where T : BaseOptionsPanel, new()
        {
            var panel = new T();
            panel.Initialize(this);
            return panel;
        }
    }
}
