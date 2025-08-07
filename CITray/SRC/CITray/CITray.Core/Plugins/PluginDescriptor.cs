using System;

namespace CITray.Plugins
{
    /// <summary>
    /// A plugin descriptor is the internal representation of a plugin.
    /// </summary>
    internal class PluginDescriptor
    {
        private string pluginDisplayName = string.Empty;

        public string FileName { get; set; }

        public string AssemblyName { get; set; }

        /// <summary>
        /// Gets or sets the plugin display name.
        /// </summary>
        /// <value>The display name of the plugin.</value>
        public string PluginDisplayName 
        {
            get
            {
                if (string.IsNullOrEmpty(pluginDisplayName))
                    pluginDisplayName = DefaultPluginName;
                return pluginDisplayName;
            }
            set { pluginDisplayName = value; }
        }

        public string PluginDescription { get; set; }
 
        public Type PluginType { get; set;  }

        private string DefaultPluginName
        {
            get
            {
                Assert.IsNotNull(PluginType);
                return PluginType.Name;
            }
        }
    }
}
