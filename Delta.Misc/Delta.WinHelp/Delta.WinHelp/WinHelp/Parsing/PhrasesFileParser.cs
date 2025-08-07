using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delta.WinHelp.Internals;

namespace Delta.WinHelp.Parsing
{
    internal class PhrasesFileParser : BaseInternalFileParser<PhrasesFile>
    {
        private readonly bool compressed;
        private int alreadyDecodedCount = 0;

        public PhrasesFileParser(BinaryReader reader, bool isCompressed) : base(reader)
        {
            compressed = isCompressed;
        }

        protected override PhrasesFile ParseFileContent(InternalFileHeader internalFileHeader)
        {
            if (compressed)
                return ParseCompressedFileContent(internalFileHeader);

            // Process not compressed files here
            var file = new PhrasesFile() { Header = internalFileHeader };

            file.NumPhrases = Reader.ReadUInt16();
            alreadyDecodedCount += 2;
            file.MustBeOneHundred = Reader.ReadUInt16();
            alreadyDecodedCount += 2;
            file.PhraseOffsets = new ushort[(int)file.NumPhrases + 1];
            file.Phrases = new string[(int)file.NumPhrases];

            FillOffsetsAndStrings(file);

            return file;
        }

        private PhrasesFile ParseCompressedFileContent(InternalFileHeader internalFileHeader)
        {
            // Process compressed files here
            var file = new LZ77PhrasesFile() { Header = internalFileHeader };
            
            file.NumPhrases = Reader.ReadUInt16();
            alreadyDecodedCount += 2;
            file.MustBeOneHundred = Reader.ReadUInt16();
            alreadyDecodedCount += 2;
            file.DecompressedSize = Reader.ReadInt32();
            alreadyDecodedCount += 4;
            file.PhraseOffsets = new ushort[(int)file.NumPhrases + 1];
            file.Phrases = new string[(int)file.NumPhrases];

            FillOffsetsAndStrings(file);

            return file;
        }

        private void FillOffsetsAndStrings(PhrasesFile file)
        {
            file.PhraseOffsets[0] = Reader.ReadUInt16(); // Should be 2 * (NumPhrases + 1)
            alreadyDecodedCount += 2;

            for (int index = 0; index < file.NumPhrases; index++)
            {
                file.PhraseOffsets[index + 1] = Reader.ReadUInt16();
                alreadyDecodedCount += 2;
            }

            //for (int index = 0; index < file.NumPhrases; index++)
            //    file.PhraseOffsets[index + 1] = Reader.ReadUInt16();
        }
    }
}
