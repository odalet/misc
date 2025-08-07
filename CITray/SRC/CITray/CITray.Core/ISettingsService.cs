using System;

namespace CITray
{
    internal interface ISettingsService : ISettings
    {
        /// <summary>
        /// Loads the settings service contents from its underlying store.
        /// </summary>
        void Load();

        /// <summary>
        /// Saves the settings service contents to its underlying store.
        /// </summary>
        void Save();
    }
}
