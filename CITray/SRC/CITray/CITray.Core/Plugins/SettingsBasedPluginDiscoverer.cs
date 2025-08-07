using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Generic;

using CITray.Api;

namespace CITray.Plugins
{
    internal class SettingsBasedPluginDiscoverer : IPluginDiscoverer
    {
        private ISettingsService settings = null;
        private List<PluginDescriptor> discoveredPlugins = new List<PluginDescriptor>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsBasedPluginDiscoverer"/> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public SettingsBasedPluginDiscoverer(ISettingsService settingsService)
        {
            if (settingsService == null) throw new ArgumentNullException("settingsService");
            settings = settingsService;
        }

        #region IPluginDiscoverer Members

        /// <summary>
        /// Gets a collection of descriptors representing the discovered plugins.
        /// </summary>
        /// <value>The discovered plugins' descriptors.</value>
        public ICollection<PluginDescriptor> DiscoveredPlugins
        {
            get { return discoveredPlugins; }
        }

        /// <summary>
        /// Discovers CITray plugins.
        /// </summary>
        public void Discover()
        {
            var files = GetAssemblyFiles();
            if (files == null || files.Length == 0)
            {
                //TODO: log info: no plugin found.
                return;
            }

            foreach (var file in files)
                DiscoverFromFile(file);
        }

        #endregion

        private string[] GetAssemblyFiles()
        {
            var baseDirectory = Path.Combine(This.RootApplicationDirectory,
                settings.Global.PluginsFolder);
            if (!Directory.Exists(baseDirectory))
            {
                //TODO: log warning: plugin dir is invalid;
                return new string[0];
            }

            return Directory.GetFiles(baseDirectory, "*.dll");
        }

        private void DiscoverFromFile(string file)
        {
            Assembly assy = null;
            try
            {
                assy = Assembly.LoadFrom(file);
            }
            catch (Exception ex)
            {
                //TODO: log the error
                var debugEx = ex;
                return;
            }

            Assert.IsNotNull(assy, string.Format("Assembly loaded from file {0} should not be null.", file));

            DiscoverFromAssembly(assy, file);
        }

        /// <summary>
        /// Discovers plugins from the specified assembly.
        /// </summary>
        /// <param name="assy">The assembly.</param>
        /// <param name="file">The file containing the assembly (for information purpose).</param>
        private void DiscoverFromAssembly(Assembly assy, string file)
        {
            // loop over the assy's type and search for IPlugin implementors
            var types = assy.GetTypes().Where(t =>
                t.IsA<IPlugin>() && !t.IsAbstract).ToArray();

            if (types.Length == 0)
            {
                // Nothing to discover in this assembly.
                //TODO: log verbose info about this.
                return;
            }

            // Now create the descriptors
            discoveredPlugins.AddRange(types.Select(t =>
            {
                var displayName = Get<DisplayNameAttribute>(t, a => a.DisplayName);
                var description = Get<DescriptionAttribute>(t, a => a.Description);

                return new PluginDescriptor()
                {
                    FileName = file,
                    AssemblyName = assy.FullName,
                    PluginDescription = description,
                    PluginDisplayName = displayName,
                    PluginType = t
                };
            }));
        }

        private string Get<T>(Type type, Func<T, string> extractInfo)
            where T : Attribute
        {
            var attributes = type.GetCustomAttributes(typeof(T), false);
            var attribute = attributes.Cast<T>().SingleOrDefault();

            if (attribute != null) return extractInfo(attribute);
            else return string.Empty;
        }
    }
}
