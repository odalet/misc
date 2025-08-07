using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace CITray.Settings
{
    public class GlobalSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GlobalSettings"/> class.
        /// </summary>
        public GlobalSettings()
        {
            Clear();
        }

        /// <summary>
        /// Gets or sets the plugins folder.
        /// </summary>
        /// <value>The plugins folder.</value>
        public string PluginsFolder { get; internal set; }

        /// <summary>
        /// Gets or sets the enabled plugins list.
        /// </summary>
        /// <value>The enabled plugins.</value>
        public string[] EnabledPlugins { get; internal set; }

        /// <summary>
        /// Loads the settings from the specified Xml element.
        /// </summary>
        /// <param name="root">The root Xml element containing settings.</param>
        internal void Load(XElement root)
        {
            PluginsFolder = root.Element("pluginsFolder").Value;
            EnabledPlugins = root.Element("enabledPlugins").Elements("assembly").Select(x => x.Value).ToArray();
        }

        /// <summary>
        /// Saves these settings to XML.
        /// </summary>
        /// <returns>A collection of XML elements.</returns>
        internal IEnumerable<XElement> Save()
        {
            return new XElement[]
            {
                new XElement("pluginsFolder", PluginsFolder),
                new XElement("enabledPlugins",
                    EnabledPlugins.Select(s =>
                        new XElement("assembly", s)
                    )
                )
            };
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        internal void Clear()
        {
            PluginsFolder = string.Empty;
            EnabledPlugins = new string[0];
        }
    }
}
