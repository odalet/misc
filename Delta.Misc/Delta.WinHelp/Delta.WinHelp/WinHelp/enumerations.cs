using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.WinHelp
{
    public enum WinHelpDocumentKind
    {
        Help,
        Multimedia
    }

    public enum WinHelpVersion
    {
        Unknown,
        Version15 = 15,
        Version21 = 21,
        Version27 = 27,
        Version33 = 33,
    }

    public enum WinHelpCompression
    {
        Unknown,
        None,
        LZ77
    }

    public enum WinHelpPhraseCompression
    {
        Unknown,
        None,
        OldStyle,
        Hall
    }

    public static class WinHelpEnumerationsExtensions
    {
        public static string GetDetailedLabel(this WinHelpVersion version)
        {
            switch (version)
            {
                case WinHelpVersion.Version15:
                    return "HC30 Windows 3.0 help file (v15)";
                case WinHelpVersion.Version21:
                    return "HC31 Windows 3.1 help file (v21)";
                case WinHelpVersion.Version27:
                    return "WMVC/MMVC media view file (v27)";
                case WinHelpVersion.Version33:
                    return "MVC or HCW 4.00 Windows 95 (v33)";
            }

            return "Unknown Version";
        }
    }
}
