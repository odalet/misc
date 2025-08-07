using System;
using System.IO;
using System.Reflection;
using System.ComponentModel.Design;

using CITray.Plugins;

using ThisAssembly = ThisAssemblyCore;

namespace CITray
{
    /// <summary>
    /// This "This" class represents a central point containing services and
    /// global objects/methods needed throughout the entire application.
    /// </summary>
    internal class This
    {
        private static readonly object locker = new object();
        private static readonly This instance = new This();
        private static bool initialized = false;
        private static string rootdir = string.Empty;

        private ServiceContainer serviceContainer = null;

        /// <summary>
        /// Bootstraps the application.
        /// </summary>
        public static void Bootstrap()
        {
            if (initialized) throw new ApplicationException(string.Format(
                SR.AlreadyInitializedError, typeof(This)));
            
            lock(locker)
            {
                instance.Initialize();
                initialized = true;
            }            
        }

        public static string ApplicationName
        {
            get { return ThisAssembly.Product; }
        }

        public static string ApplicationVersion
        {
            get { return ThisAssembly.ProductVersion; }
        }

        public static string SettingsFileName
        {
            get
            {
                var userDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                return Path.Combine(Path.Combine(Path.Combine(
                    userDataPath, ApplicationName), ApplicationVersion), "settings.xml");
            }
        }

        /// <summary>
        /// Gets the global services for the application.
        /// </summary>
        /// <value>The services.</value>
        public static IServiceContainer Services
        {
            get { return instance.serviceContainer; }
        }

        public static ISettings Settings
        {
            get { return Services.GetService<ISettings>(true); }
        }

        public static PluginManager PluginManager
        {
            get; private set;
        }

        #region Helper properties

        /// <summary>
        /// Gets the root directory for this application.
        /// </summary>
        /// <value>The root directory.</value>
        public static string RootApplicationDirectory
        {
            get
            {
                if (string.IsNullOrEmpty(rootdir))
                {
                    var location = Assembly.GetEntryAssembly().Location;
                    rootdir = Path.GetDirectoryName(location);
                }

                return rootdir;
            }
        }

        #endregion

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            serviceContainer = new ServiceContainer();

            // add global services
            AddSettingsService();

            LoadPlugins();
        }

        private void LoadPlugins()
        {
            PluginManager = new PluginManager(Services);
            PluginManager.Discover();
        }

        /// <summary>
        /// Adds the global settings service and read settings information.
        /// </summary>
        private void AddSettingsService()
        {
            try
            {
                // Get user settings file.
                var settingsFileName = SettingsFileName;
                var settingsService = new SettingsService(settingsFileName, null);
                settingsService.Load();

                serviceContainer.AddService<ISettingsService>(settingsService);
                serviceContainer.AddService<ISettings>(settingsService);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(string.Format(
                    SR.SettingsInitializationError, ex.Message), ex);
            }
        }
        
//#if DEBUG
//        public static void TestSaveSettings()
//        {
//            var settings = new SettingsService(@"c:\temp\settings.xml", null);
//            settings.Global.EnabledPlugins = new string[]
//            {
//                "a.dll", "b.dll", "c.dll"
//            };

//            settings.Global.PluginsFolder = @".\plugins";

//            settings.Save();
//        }

//        public static void TestLoadSettings()
//        {
//            var settings = new SettingsService(@"c:\temp\settings.xml", null);
//            settings.Load();
//            var g = settings.Global;
//        }
//#endif
    }
}
