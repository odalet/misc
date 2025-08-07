namespace Delta.WinHelp.Internals
{
    internal class WinHelpHeader
    {
        public uint Magic { get; internal set; }
        public uint DirectoryStart { get; internal set; }
        public uint FirstFreeBlock { get; internal set; }
        public uint EntireFileSize { get; internal set; }
    }

    internal abstract class InternalFile
    {
        public InternalFileHeader Header { get; internal set; }
    }

    internal class InternalFileHeader
    {
        public uint ReservedSpace { get; internal set; }
        public uint UsedSpace { get; internal set; }
        public byte FileFlags { get; internal set; }
    }
}
