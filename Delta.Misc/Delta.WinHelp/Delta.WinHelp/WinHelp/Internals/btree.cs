using System.Collections.Generic;

namespace Delta.WinHelp.Internals
{
    internal class BTreeHeader
    {
        public ushort Magic { get; internal set; }
        public ushort Flags { get; internal set; }
        public ushort PageSize { get; internal set; }
        public string Structure { get; internal set; }
        public short MustBeZero { get; internal set; }
        public short PageSplits { get; internal set; }
        public short RootPage { get; internal set; }
        public short MustBeNegOne { get; internal set; }
        public short TotalPages { get; internal set; }
        public short NLevels { get; internal set; }
        public int TotalBtreeEntries { get; internal set; }
    }

    internal class BTreeIndexPage<T> where T : BTreeIndexEntry
    {
        public BTreeIndexPage()
        {
            Entries = new List<T>();
        }

        public BTreeIndexHeader Header { get; set; }
        public IList<T> Entries { get; private set; }
    }

    internal class BTreeIndexHeader
    {
        public ushort Unused { get; set; }
        public short NEntries { get; set; }
        public short PreviousPage { get; set; }
    }

    internal class BTreeLeafPage<T> where T : BTreeLeafEntry
    {
        public BTreeLeafPage()
        {
            Entries = new List<T>();
        }

        public BTreeNodeHeader Header { get; set; }
        public IList<T> Entries { get; private set; }
    }

    internal class BTreeNodeHeader
    {
        public ushort Unused { get; set; }
        public short NEntries { get; set; }
        public short PreviousPage { get; set; }
        public short NextPage { get; set; }
    }

    internal class BTreeIndexEntry { }

    internal class BTreeLeafEntry { }
}
