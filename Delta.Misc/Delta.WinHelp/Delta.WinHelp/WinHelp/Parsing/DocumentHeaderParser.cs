using System.IO;
using Delta.WinHelp.Internals;

namespace Delta.WinHelp.Parsing
{
    internal class DocumentHeaderParser : BaseParser<WinHelpHeader>
    {
        private const uint winHelpMagic = 0x00035F3F;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentHeaderParser"/> class.
        /// </summary>
        /// <param name="reader">The reader.</param>
        public DocumentHeaderParser(BinaryReader reader) : base(reader) { }

        protected override WinHelpHeader ParseCore()
        {
            var header = new WinHelpHeader();
            header.Magic = base.Reader.ReadUInt32();
            header.DirectoryStart = base.Reader.ReadUInt32();
            header.FirstFreeBlock = base.Reader.ReadUInt32();
            header.EntireFileSize = base.Reader.ReadUInt32();
            return header;
        }

        protected override void Check(WinHelpHeader header)
        {
            if (header.Magic != winHelpMagic)
                throw new WinHelpParsingException("Invalid Magic number");
        }
    }
}
