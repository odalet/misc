namespace Delta.AnsiToEscapedUnicodeTool.Engine
{
    internal class CodecOptions
    {
        private static readonly CodecOptions defaults = new CodecOptions() 
        {
            SingleQuoteEscapeMode = SingleQuoteEscapeMode.Normal 
        };

        public static CodecOptions Defaults
        {
            get { return defaults; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CodecOptions"/> class.
        /// </summary>
        public CodecOptions()
        {
            SingleQuoteEscapeMode = SingleQuoteEscapeMode.Normal;
        }

        // Ansi --> Unicode
        public SingleQuoteEscapeMode SingleQuoteEscapeMode { get; set; }

        // Unicode --> Ansi
        public bool ReinterpretQuotes { get; set; }
    }
}
