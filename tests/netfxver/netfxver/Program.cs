using Microsoft.Win32;
using System;
using System.Linq;

namespace netfxver
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            GetVersionFromRegistry(false);
            Console.WriteLine("--------------------------");
            GetVersionFromRegistry(true);
            Console.ReadKey();
        }

        private static void GetVersionFromRegistry(bool x86 = false)
        {
            var wow = x86 ? @"WOW6432Node\" : "";

            // Opens the registry key for the .NET Framework entry.
            using (var ndpKey = RegistryKey
                .OpenRemoteBaseKey(RegistryHive.LocalMachine, "")
                .OpenSubKey($@"SOFTWARE\{wow}Microsoft\NET Framework Setup\NDP\"))
            {
                // As an alternative, if you know the computers you will query are running .NET Framework 4.5 
                // or later, you can use:
                // using (RegistryKey ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, 
                // RegistryView.Registry32).OpenSubKey(@"SOFTWARE\Microsoft\NET Framework Setup\NDP\"))

                var keys = ndpKey.GetSubKeyNames().Where(v => v.StartsWith("v"));

                // We want v4.0 to appear between v3.5 and v4
                var ordered = keys
                    .Where(v => string.Compare(v, "v4") < 0)
                    .Union(keys.Where(v => v == "v4.0"))
                    .Union(keys.Where(v => v != "4.0" && string.Compare(v, "v4") >= 0));

                foreach (var versionKeyName in ordered)
                {
                    var versionKey = ndpKey.OpenSubKey(versionKeyName);
                    var fullVersion = (string)versionKey.GetValue("Version", string.Empty);
                    var sp = versionKey.GetValue("SP", string.Empty).ToString();
                    var install = versionKey.GetValue("Install", string.Empty).ToString();

                    Console.WriteLine($"{versionKeyName}:");

                    if (!string.IsNullOrEmpty(fullVersion)) // < v4
                    {
                        var hasSP = !string.IsNullOrEmpty(sp) && install == "1";
                        Console.WriteLine($"\t{fullVersion + (hasSP ? $" SP{sp}" : "")}");
                        continue; // We're done here
                    }

                    // >= v4

                    var sub = versionKey.GetSubKeyNames();
                    //var ordered = sub.Where(v => v == "v4.0"); //.(sub.Where(v => v != "v4.0"));

                    foreach (var subKeyName in versionKey.GetSubKeyNames())
                    {
                        var subKey = versionKey.OpenSubKey(subKeyName);
                        fullVersion = (string)subKey.GetValue("Version", string.Empty);
                        sp = subKey.GetValue("SP", string.Empty).ToString();
                        install = subKey.GetValue("Install", "").ToString();

                        Console.WriteLine($"\t{subKeyName}");

                        if (!string.IsNullOrEmpty(fullVersion)) // < v4
                        {
                            var hasSP = !string.IsNullOrEmpty(sp) && install == "1";
                            Console.WriteLine($"\t\t{fullVersion + (hasSP ? $" SP{sp}" : "")}");
                            continue; // We're done here
                        }
                    }
                }
            }
        }
    }
}
