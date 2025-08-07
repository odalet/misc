using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.WinHelp.Internals
{
    internal class PhrasesFile : InternalFile
    {
        public PhrasesFile() { }
        
        public ushort NumPhrases { get; set; }
        public ushort MustBeOneHundred { get; set; }
        public ushort[] PhraseOffsets { get; set; }
        public string[] Phrases { get; set; }
    }

    internal class LZ77PhrasesFile : PhrasesFile
    {
        public int DecompressedSize { get; set; }
    }
}
