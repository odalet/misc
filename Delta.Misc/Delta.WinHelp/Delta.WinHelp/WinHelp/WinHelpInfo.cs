using System;
using System.Linq;
using Delta.WinHelp.Internals;

namespace Delta.WinHelp
{
    /// <summary>
    /// Contains various information about a given .HLP file
    /// </summary>
    public class WinHelpInfo
    {
        internal  WinHelpInfo(SystemFile sysfile)
        {
            if (sysfile == null) throw new ArgumentNullException("sysfile");
                        
            Date = sysfile.SystemHeader.GenDate;
            DetermineVersion(sysfile.SystemHeader);
            DetermineCompressionAndTopicBlockSize(sysfile.SystemHeader);            
        }

        public WinHelpVersion Version { get; private set; }

        public DateTime? Date { get; private set; }

        public WinHelpCompression Compression { get; private set; }

        public int TopicBlockSize { get; private set; }

        private void DetermineVersion(SystemHeader header)
        {
            var minor = (int)header.Minor;
            if (Enum.GetValues(typeof(WinHelpVersion)).Cast<int>().Contains((int)header.Minor))
                Version = (WinHelpVersion)minor;
            else Version = WinHelpVersion.Unknown;
        }

        private void DetermineCompressionAndTopicBlockSize(SystemHeader header)
        {
            if (header.Minor <= 16)
            {
                Compression = WinHelpCompression.None;
                TopicBlockSize = 2048;
            }
            else if (header.Flags == 0)
            {
                Compression = WinHelpCompression.None;
                TopicBlockSize = 4096;
            }
            else if (header.Flags == 4)
            {
                Compression = WinHelpCompression.LZ77;
                TopicBlockSize = 4096;
            }
            else if (header.Flags == 8)
            {
                Compression = WinHelpCompression.LZ77;
                TopicBlockSize = 2048;
            }
            else // No clue...
            {
                Compression = WinHelpCompression.Unknown;
                TopicBlockSize = -1;
            }
        }
    }
}
