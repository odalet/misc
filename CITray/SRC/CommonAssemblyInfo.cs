using System;
using System.Reflection;
using System.Globalization;

#if CITRAY_CORE
// So that it doesn't collide with other assemblies (see InternalsVisibleTo)
[assembly: AssemblyVersion(ThisAssemblyCore.Version)]
[assembly: AssemblyProduct(ThisAssemblyCore.Product)]
[assembly: AssemblyCompany(ThisAssemblyCore.Company)]
[assembly: AssemblyCopyright(ThisAssemblyCore.Copyright)]
[assembly: AssemblyTrademark(ThisAssemblyCore.Trademark)]

static partial class ThisAssemblyCore
#else

[assembly: AssemblyVersion(ThisAssembly.Version)]
[assembly: AssemblyProduct(ThisAssembly.Product)]
[assembly: AssemblyCompany(ThisAssembly.Company)]
[assembly: AssemblyCopyright(ThisAssembly.Copyright)]
[assembly: AssemblyTrademark(ThisAssembly.Trademark)]

static partial class ThisAssembly
#endif
{
    public const string ProductVersion = "0.1.0";
    public const string Product = "CITray";
    public const string Trademark = "";
    public const string Company = "Delta Software";
	
#if CCNET
    public const string Copyright = "Delta Software ${BuildYear} (MsPL)";
#else
    public const string Copyright = "Delta Software 2010 (MsPL)";
#endif

    public const string Version = thisVersion;
	public static DateTime BuildDate { get { return buildDate; } }

#if CCNET
    private static readonly DateTime buildDate = DateTime.ParseExact(
        "${BuildDate}", "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
    private const string buildNumberAsString = "${BuildNumber}";    
#else
    private static readonly DateTime buildDate = DateTime.ParseExact(
        "1900/01/01 00:00:00", "yyyy/MM/dd HH:mm:ss", CultureInfo.InvariantCulture);
    private const string buildNumberAsString = "0";
#endif
}