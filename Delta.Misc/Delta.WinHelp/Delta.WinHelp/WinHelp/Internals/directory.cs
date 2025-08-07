using System.Collections.Generic;
using System.Linq;

namespace Delta.WinHelp.Internals
{

    internal class InternalDirectory : InternalFile
    {
        private BTreeIndexPage<DirectoryIndexEntry>[] indexPages = null;
        private BTreeLeafPage<DirectoryLeafEntry>[] leafPages = null;

        public InternalDirectory() { }
        
        public BTreeHeader BTreeHeader { get; set; }

        public object[] Pages { get; set; }

        public IEnumerable<BTreeIndexPage<DirectoryIndexEntry>> IndexPages
        {
            get 
            {
                if (indexPages == null) indexPages = Pages
                    .Where(p => p is BTreeIndexPage<DirectoryIndexEntry>)
                    .Cast<BTreeIndexPage<DirectoryIndexEntry>>()
                    .ToArray();

                return indexPages;
            }
        }
        
        public IEnumerable<BTreeLeafPage<DirectoryLeafEntry>> LeafPages
        {
            get 
            {
                if (leafPages == null) leafPages = Pages
                    .Where(p => p is BTreeLeafPage<DirectoryLeafEntry>)
                    .Cast<BTreeLeafPage<DirectoryLeafEntry>>()
                    .ToArray();

                return leafPages;
            }
        }
    }

    internal class DirectoryIndexEntry : BTreeIndexEntry
    {
        public string FileName { get; set; }
        public short PageNumber { get; set; }
    }


    internal class DirectoryLeafEntry : BTreeLeafEntry
    {
        public string FileName { get; set; }
        public int FileOffset { get; set; }
    }
}
