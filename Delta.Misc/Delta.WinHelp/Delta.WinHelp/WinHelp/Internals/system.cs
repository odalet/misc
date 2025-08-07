using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delta.WinHelp.Internals
{
    internal enum SystemRecordType
    {
        Title = 1,
        Copyright = 2,
        Contents = 3,
        Config = 4,
        Icon = 5,
        HlpWindow = 6,
        MvpWindow = 6,  // Yes it has the same Id
        Citation = 8,
        Lcid = 9,
        Cnt = 10,
        Charset = 11,
        DefaultFont = 12,
        FTIndex = 12,  // Yes it has the same Id
        Group = 13,
        Index = 14,
        KeyIndex = 14,  // Yes it has the same Id
        Language = 18,
        DllMap = 19
    }

    internal class SystemFile : InternalFile
    {
        public SystemFile()
        {
            Records = new List<SystemRec>();
        }

        public SystemHeader SystemHeader { get; set; }
        public string HelpFileTitle { get; set; }
        public IList<SystemRec> Records { get; private set; }
    }

    internal class SystemHeader
    {
        public short Magic { get; set; }
        public short Minor { get; set; }
        public short Major { get; set; }
        public DateTime? GenDate { get; set; }
        public ushort Flags { get; set; }
    }

    internal class SystemRec
    {
        public ushort RecordType { get; set; }
        public ushort DataSize { get; set; }
        public byte[] Data { get; set; }
    }
}
