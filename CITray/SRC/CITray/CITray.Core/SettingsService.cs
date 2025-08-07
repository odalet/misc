using System;
using System.IO;
using System.Xml.Linq;

using CITray.Settings;

namespace CITray
{
    internal class SettingsService : ISettingsService, ISettings
    {
        private string fileName = string.Empty;
        private GlobalSettings globalSettings = new GlobalSettings();
        private Action<Exception> onCorrupted = null;
        private bool isRunningInsideLambda = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsService"/> class.
        /// </summary>
        /// <param name="settingsFileName">Name of the settings file.</param>
        /// <param name="onCorruptedAction">The action to execute if the settings file is corrupted when loaded.</param>
        /// <remarks>
        /// The default for <paramref name="onCorruptedAction"/> is to save the current settings to disk and to load them back.
        /// </remarks>
        public SettingsService(string settingsFileName, Action<Exception, ISettingsService> onCorruptedAction)
        {
            if (string.IsNullOrEmpty(settingsFileName))
                throw new ArgumentNullException("settingsFileName");
            fileName = settingsFileName;

            var local = this;
            if (onCorruptedAction == null) onCorrupted = ex =>
            {
                // this is reentrant and may loop indefinitely. We tell the current instance it must not try 
                // to call onCorrupted lambda again.
                local.isRunningInsideLambda = true;
                try
                {
                    //TODO: log the exception and log the fact we will regenerate an empty settings file                    
                    local.Save();
                    local.Load();
                }
                finally { local.isRunningInsideLambda = false; }
            };
            else onCorrupted = ex =>
            {
                // here again, we wrap the call to prevent reentrance.
                local.isRunningInsideLambda = true;
                try
                {
                    onCorruptedAction(ex, local);
                }
                finally { local.isRunningInsideLambda = false; }
            };

            Clear();
        }

        #region ISettings Members

        /// <summary>
        /// Gets the global settings section.
        /// </summary>
        /// <value>The global settings.</value>
        public GlobalSettings Global
        {
            get { return globalSettings; }
        }

        #endregion

        #region ISettingsService Members

        /// <summary>
        /// Loads the settings service contents from the settings file.
        /// </summary>
        /// <remarks>
        /// If settings exist that were never saved, they'll be overwritten by this operation.
        /// </remarks>
        public void Load()
        {
            EnsureFileName();
            if (!File.Exists(fileName))
            {
                // Create the default settings file (from the one 
                // embedded in the assembly's resource) 
                Clear();
                var xdoc = XDocument.Parse(
                    CoreProperties.Resources.defaultSettings, 
                    LoadOptions.PreserveWhitespace);
                FileHelper.CreateDirectoryIfNeeded(
                    Path.GetDirectoryName(fileName));
                xdoc.Save(fileName);
            }

            try
            {
                var xdoc = XDocument.Load(fileName);
                globalSettings = new GlobalSettings();
                globalSettings.Load(xdoc.Element("settings").Element("global"));
            }
            catch (Exception ex)
            {
                if (isRunningInsideLambda) throw;
                else onCorrupted(ex); // this tries to recover from the error.
            }
        }

        /// <summary>
        /// Saves the settings service contents to the settings file.
        /// </summary>
        public void Save()
        {
            EnsureFileName();

            var xdoc = new XDocument();
            xdoc.Add(
                new XElement("settings",
                    new XAttribute("version", This.ApplicationVersion),
                    new XElement("global", globalSettings.Save()
                    )
                )
            );

            xdoc.Save(fileName);
        }

        #endregion

        /// <summary>
        /// Clears the settings service contents.
        /// </summary>
        private void Clear()
        {
            //TODO: implement Clear
            globalSettings = new GlobalSettings();
            globalSettings.Clear();
        }

        /// <summary>
        /// Ensures the <see cref="fileName"/> is not null nor empty.
        /// </summary>
        /// <exception cref="NullReferenceException">We consider here that an empty string is equivalent to a null string.</exception>
        private void EnsureFileName()
        {
            if (string.IsNullOrEmpty(fileName)) throw new NullReferenceException("fileName");
        }
    }
}
