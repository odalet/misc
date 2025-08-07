using System;
using System.Collections.Generic;

namespace CITray.Plugins
{
    /// <summary>
    /// This interface is implemented by classes that allaow the discovery
    /// of CITray plugins.
    /// </summary>
    internal interface IPluginDiscoverer
    {
        /// <summary>
        /// Gets a collection of descriptors representing the discovered plugins.
        /// </summary>
        /// <value>The discovered plugins' descriptors.</value>
        ICollection<PluginDescriptor> DiscoveredPlugins { get; }

        /// <summary>
        /// Discovers CITray plugins.
        /// </summary>
        void Discover();
    }
}
