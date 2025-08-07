using System;
using System.IO;
using Delta.WinHelp.Internals;

namespace Delta.WinHelp.Parsing
{
    internal interface IWinHelpParser<TResult>
    {
        TResult Parse();

        TResult Result { get; }
    }

    internal abstract class BaseParser<TResult> : IWinHelpParser<TResult>
    {
        private readonly BinaryReader reader;

        public BaseParser(BinaryReader inputReader)
        {
            if (inputReader == null)
                throw new ArgumentNullException("inputReader");
            reader = inputReader;
        }

        protected BinaryReader Reader { get { return reader; } }

        #region IWinHelpParser<TResult> Members

        public TResult Result { get; protected set; }

        public virtual TResult Parse()
        {
            var result = ParseCore();
            Check(result);
            Result = result;
            return result;
        }

        #endregion

        protected abstract TResult ParseCore();

        protected virtual void Check(TResult result) { }
    }

    internal abstract class BaseInternalFileParser<T> : BaseParser<T> where T : InternalFile, new()
    {
        public BaseInternalFileParser(BinaryReader reader) : base(reader) { }

        protected override T ParseCore()
        {
            var header = ParseFileHeader();
            var result = ParseFileContent(header);
            result.Header = header;
            return result;
        }

        private InternalFileHeader ParseFileHeader()
        {
            var header = new InternalFileHeader();
            header.ReservedSpace = base.Reader.ReadUInt32();
            header.UsedSpace = base.Reader.ReadUInt32();
            header.FileFlags = base.Reader.ReadByte();
            return header;
        }

        protected abstract T ParseFileContent(InternalFileHeader internalFileHeader);
    }
}
