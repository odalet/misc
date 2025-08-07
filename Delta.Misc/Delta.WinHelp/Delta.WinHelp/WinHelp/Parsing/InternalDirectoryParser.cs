using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Delta.WinHelp.Internals;

namespace Delta.WinHelp.Parsing
{
    internal class InternalDirectoryParser : BaseInternalFileParser<InternalDirectory>
    {
        public InternalDirectoryParser(BinaryReader reader) : base(reader) { }

        protected override InternalDirectory ParseFileContent(InternalFileHeader internalFileHeader)
        {
            var directory = new InternalDirectory() { Header = internalFileHeader };
            directory.BTreeHeader = ParseBTreeHeader();

            // Store the pages Data
            var pageBytes = new List<byte[]>();
            for (var i = 0; i < directory.BTreeHeader.TotalPages; i++)
            {
                var page = base.Reader.ReadBytes(directory.BTreeHeader.PageSize);
                pageBytes.Add(page);
            }

            // We store all the pages in one unique list so that their index is kept consistant.
            directory.Pages = new object[directory.BTreeHeader.TotalPages];

            if (directory.BTreeHeader.NLevels > 1)
            {
                var currentIndexLevel = 1;
                var currentIPageIndex = directory.BTreeHeader.RootPage;
                while (currentIndexLevel < directory.BTreeHeader.NLevels)
                {
                    var indexPageData = pageBytes[currentIPageIndex];

                    using (var mstream = new MemoryStream(indexPageData))
                    using (var mreader = new BinaryReader(mstream))
                    {
                        var indexPage = ProcessDirectoryIndexPage(mreader, indexPageData.Length);
                        directory.Pages[currentIPageIndex] = (indexPage);
                        // The following index page is @PreviousPage (unless we've reached NLevels)
                        currentIPageIndex = indexPage.Header.PreviousPage;
                    }

                    currentIndexLevel++;
                }
            }

            // Process the leaf-pages
            for (int pageIndex = 0; pageIndex < directory.Pages.Length; pageIndex++)
            {
                if (directory.Pages[pageIndex] != null)
                    continue; // Aready filled? This is an index page..
                var pageData = pageBytes[pageIndex];

                using (var mstream = new MemoryStream(pageData))
                using (var mreader = new BinaryReader(mstream))
                {
                    var leafPage = ProcessDirectoryLeafPage(mreader, pageData.Length);
                    directory.Pages[pageIndex] = leafPage;
                }
            }

            return directory;
        }

        protected override void Check(InternalDirectory result)
        {
            if (result.BTreeHeader.MustBeZero != (short)0)
                throw new WinHelpParsingException("Invalid BTree Header (MustBeZero != 0)");
            if (result.BTreeHeader.MustBeNegOne != (short)-1)
                throw new WinHelpParsingException("Invalid BTree Header (MustBeNegOne != -1)");
        }

        private BTreeHeader ParseBTreeHeader()
        {
            var header = new BTreeHeader();
            header.Magic = base.Reader.ReadUInt16();
            header.Flags = base.Reader.ReadUInt16();
            header.PageSize = base.Reader.ReadUInt16();
            header.Structure = Helper.DecodeStringz(base.Reader.ReadBytes(16));
            header.MustBeZero = base.Reader.ReadInt16();
            header.PageSplits = base.Reader.ReadInt16();
            header.RootPage = base.Reader.ReadInt16();
            header.MustBeNegOne = base.Reader.ReadInt16();
            header.TotalPages = base.Reader.ReadInt16();
            header.NLevels = base.Reader.ReadInt16();
            header.TotalBtreeEntries = base.Reader.ReadInt32();

            return header;
        }

        private BTreeIndexPage<DirectoryIndexEntry> ProcessDirectoryIndexPage(BinaryReader reader, int pageSize)
        {
            var indexPage = new BTreeIndexPage<DirectoryIndexEntry>();
            indexPage.Header = new BTreeIndexHeader();
            indexPage.Header.Unused = reader.ReadUInt16();
            indexPage.Header.NEntries = reader.ReadInt16();
            indexPage.Header.PreviousPage = reader.ReadInt16();

            // Let's parse the entries
            for (int i = 0; i < indexPage.Header.NEntries; i++)
            {
                var entry = new DirectoryIndexEntry();
                entry.FileName = Helper.DecodeStringz(reader); // There is room for optimization here
                entry.PageNumber = reader.ReadInt16();
                indexPage.Entries.Add(entry);
            }

            return indexPage;
        }

        private BTreeLeafPage<DirectoryLeafEntry> ProcessDirectoryLeafPage(BinaryReader reader, int pageSize)
        {
            var leafPage = new BTreeLeafPage<DirectoryLeafEntry>();
            leafPage.Header = new BTreeNodeHeader();
            leafPage.Header.Unused = reader.ReadUInt16();
            leafPage.Header.NEntries = reader.ReadInt16();
            leafPage.Header.PreviousPage = reader.ReadInt16();
            leafPage.Header.NextPage = reader.ReadInt16();

            // Let's parse the entries
            for (int i = 0; i < leafPage.Header.NEntries; i++)
            {
                var entry = new DirectoryLeafEntry();
                entry.FileName = Helper.DecodeStringz(reader); // There is room for optimization here
                entry.FileOffset = reader.ReadInt32();
                leafPage.Entries.Add(entry);
            }

            return leafPage;
        }
    }
}
