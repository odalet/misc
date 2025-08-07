using System;

namespace Delta.WinHelp
{
    /// <summary>
    /// Represents an internal file in a .HLP document.
    /// </summary>
    public class WinHelpContentFile
    {
        private const string systemFileName = "|SYSTEM";
        private const string phrasesFileName = "|Phrases";

        private readonly WinHelpDocument parent;
        private readonly string name;
        private readonly int offset; // from the beginning of the HLP file.

        public WinHelpContentFile(WinHelpDocument parentDocument, string fileName, int fileOffset)
        {
            if (parentDocument == null) 
                throw new ArgumentNullException("parentDocument");

            name = fileName;
            offset = fileOffset;
        }

        public string Name { get { return name; } }

        internal int Offset { get { return offset; } }

        public bool IsSystemFile
        {
            get { return string.CompareOrdinal(name, systemFileName) == 0; }
        }

        public bool IsPhrasesFile
        {
            get { return string.CompareOrdinal(name, phrasesFileName) == 0; }
        }
    }
}
