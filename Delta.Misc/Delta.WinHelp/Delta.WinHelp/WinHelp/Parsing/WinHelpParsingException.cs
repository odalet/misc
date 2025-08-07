using System;

namespace Delta.WinHelp.Parsing
{
    public class WinHelpParsingException : ApplicationException
    {
        public WinHelpParsingException() : base() { }
        public WinHelpParsingException(string message) : base(message) { }
    }
}
