using System;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace CITray.Plugins
{
    internal class PluginManager
    {
        private IServiceContainer services = null;
        private List<PluginDescriptor> discoveredPlugins = new List<PluginDescriptor>();

        private ISettingsService settingsService = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginManager"/> class.
        /// </summary>
        /// <param name="serviceContainer">The service container.</param>
        public PluginManager(IServiceContainer serviceContainer)
        {
            if (serviceContainer == null) throw new ArgumentNullException("serviceContainer");
            services = serviceContainer;

            settingsService = services.GetService<ISettingsService>(true);
        }

        public ICollection<PluginDescriptor> DiscoveredPlugins
        {
            get { return discoveredPlugins; }
        }

        public void Discover()
        {
            var discoProvider = services.GetService<IPluginDiscoverer>();
            if (discoProvider == null) 
                discoProvider = new SettingsBasedPluginDiscoverer(settingsService);

            discoProvider.Discover();
            discoveredPlugins = new List<PluginDescriptor>(
                discoProvider.DiscoveredPlugins);
        }
    }
}
