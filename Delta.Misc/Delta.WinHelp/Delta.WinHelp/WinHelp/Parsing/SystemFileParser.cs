using System;
using System.Collections.Generic;
using System.IO;
using Delta.WinHelp.Internals;

namespace Delta.WinHelp.Parsing
{
    internal class SystemFileParser : BaseInternalFileParser<SystemFile>
    {
        private const short systemFileHeaderMagic = 0x036C;
        public SystemFileParser(BinaryReader reader) : base(reader) { }

        private int alreadyDecodedCount = 0;

        protected override SystemFile ParseFileContent(InternalFileHeader internalFileHeader)
        {
            var file = new SystemFile() { Header = internalFileHeader };

            var header = new SystemHeader();
            header.Magic = base.Reader.ReadInt16(); 
            alreadyDecodedCount += 2;
            header.Minor = base.Reader.ReadInt16(); 
            alreadyDecodedCount += 2;
            header.Major = base.Reader.ReadInt16(); 
            alreadyDecodedCount += 2;
            header.GenDate = Helper.DecodeDate(base.Reader.ReadInt32()); 
            alreadyDecodedCount += 4;
            header.Flags = base.Reader.ReadUInt16(); 
            alreadyDecodedCount += 2;

            file.SystemHeader = header;

            if (file.SystemHeader.Minor <= 16) // We only have the title next
                file.HelpFileTitle = Helper.DecodeStringz(base.Reader);
            else
            {
                ParseRecords(file);
                DecodeRecords(file);
            }

            return file;
        }

        private void ParseRecords(SystemFile file)
        {
            var yetToDecodeCount = file.Header.UsedSpace - alreadyDecodedCount;
            while (yetToDecodeCount > 0)
            {
                var record = new SystemRec();
                record.RecordType = base.Reader.ReadUInt16();
                yetToDecodeCount -= 2;
                record.DataSize = base.Reader.ReadUInt16();
                yetToDecodeCount -= 2;
                record.Data = base.Reader.ReadBytes(record.DataSize);
                yetToDecodeCount -= record.DataSize;

                file.Records.Add(record);
            }
        }

        private void DecodeRecords(SystemFile file)
        {
            var configs = new List<string>();

            foreach (var record in file.Records)
            {
                var rectype = (SystemRecordType)record.RecordType;
                Log(string.Format("Found a '{0}' System Record: {1}", rectype, BitConverter.ToString(record.Data)));
                
                switch (rectype)
                {
                    case SystemRecordType.Title:                        
                        file.HelpFileTitle = Helper.DecodeStringz(record.Data);
                        break;
                    case SystemRecordType.Copyright: break;
                    case SystemRecordType.Contents: break;
                    case SystemRecordType.Config:
                        configs.Add(Helper.DecodeStringz(record.Data));
                        break;
                    case SystemRecordType.Icon: break;
                    case SystemRecordType.HlpWindow: break;
                    case SystemRecordType.Citation: break;
                    case SystemRecordType.Lcid:
                        ////var lcid = string.Empty;
                        //////foreach (var record.Data
                        ////Log("LCID = " + lcid);
                        break;
                    case SystemRecordType.Cnt: 
                        var cntFileName = Helper.DecodeStringz(record.Data);
                        break;
                    case SystemRecordType.Charset:
                        var charset = BitConverter.ToUInt16(record.Data, 0);
                        break;
                    case SystemRecordType.DefaultFont: break;
                    case SystemRecordType.Group: break;
                    case SystemRecordType.Index: break;
                    case SystemRecordType.DllMap: break;

                }
            }
        }
        
        protected override void Check(SystemFile result)
        {
            if (result.SystemHeader.Magic != systemFileHeaderMagic)
                throw new WinHelpParsingException("Invalid |SYSTEM Header: Invalid Magic number");

        }

        private static void Log(string text)
        {
            System.Console.WriteLine("LOG: " + text);
        }
    }
}
