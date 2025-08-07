using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Delta.WinHelp.Internals;
using Delta.WinHelp.Parsing;

namespace Delta.WinHelp
{
    public class WinHelpDocument
    {
        private const uint winHelpMagic = 0x00035F3F;
        private readonly List<WinHelpContentFile> files;

        public static WinHelpDocument Load(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentException("An absolute path to the document file must be provided", "filename");

            // Similarly to what is done in HELPDECO.C, we guess the kind of document based on its extension
            return Load(filename, Path.GetExtension(filename).ToLowerInvariant().StartsWith(".m") ?
                WinHelpDocumentKind.Multimedia : WinHelpDocumentKind.Help);
        }

        public static WinHelpDocument Load(string filename, WinHelpDocumentKind kind)
        {
            var fname = filename;
            if (!Path.IsPathRooted(fname))
                fname = Path.Combine(Environment.CurrentDirectory, filename);

            if (!File.Exists(fname))
                throw new FileNotFoundException(string.Format("File {0} could not be found.", fname));

            var document = new WinHelpDocument();
            document.FileName = fname;

            document.Parse();

            return document;
        }

        private WinHelpDocument()
        {
            files = new List<WinHelpContentFile>();
        }

        public string FileName { get; private set; }

        public WinHelpDocumentKind Kind { get; private set; }

        public WinHelpInfo Info { get; private set; }

        internal WinHelpHeader DocumentHeader { get; private set; }

        internal InternalDirectory Directory { get; private set; }

        public IReadOnlyList<WinHelpContentFile> Files
        {
            get { return files; }
        }

        private void Parse()
        {
            var now = DateTime.Now; // PERFS

            using (var stream = File.OpenRead(FileName))
            using (var reader = new BinaryReader(stream))
            {
                DocumentHeader = new DocumentHeaderParser(reader).Parse();

                // Move to @DirectoryStart & parse the internal directory
                stream.Seek((long)DocumentHeader.DirectoryStart, SeekOrigin.Begin);
                Directory = new InternalDirectoryParser(reader).Parse();

                // Now we have all the file names and their locations. Let's fill the Files table.
                files.AddRange(Directory.LeafPages.SelectMany(lp => lp.Entries.Select(e =>
                    new WinHelpContentFile(this, e.FileName, e.FileOffset))));

                // Decode the |SYSTEM file
                var sys = files.SingleOrDefault(f => f.IsSystemFile);
                if (sys != null)
                {
                    stream.Seek((long)sys.Offset, SeekOrigin.Begin);
                    var sysfile = new SystemFileParser(reader).Parse();
                    Info = new WinHelpInfo(sysfile);
                }

                // Decode the |PHRASES file
                var phrases = files.SingleOrDefault(f => f.IsPhrasesFile);
                if (phrases != null)
                {
                    stream.Seek((long)phrases.Offset, SeekOrigin.Begin);
                    var phrasesFile = new PhrasesFileParser(reader, Info.Compression == WinHelpCompression.LZ77).Parse();
                }
            }

            // PERFS
            var now2 = DateTime.Now;
            Console.WriteLine("File was parsed in {0}", now2 - now);

            // TEST: retrieve all the file names & locations
            var pairs = Directory.LeafPages.SelectMany(lp => lp.Entries.Select(e => new { e.FileName, e.FileOffset }));
            foreach (var pair in pairs)
            {
                Console.WriteLine("File {0} is @{1}", pair.FileName, pair.FileOffset);
            }

            var now3 = DateTime.Now;
            Console.WriteLine("All files displayed in {0} ({1})", now3 - now, now3 - now2);
        }
    }
}
