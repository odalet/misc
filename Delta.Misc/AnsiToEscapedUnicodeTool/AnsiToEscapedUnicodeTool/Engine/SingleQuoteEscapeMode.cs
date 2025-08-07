namespace Delta.AnsiToEscapedUnicodeTool.Engine
{
    internal enum SingleQuoteEscapeMode
    {
        /// <summary>Replaces single quotes with \u0027.</summary>
        Normal = 0,
        /// <summary>Replaces single quotes with \u0027\u0027 (needed in Java .properties files).</summary>
        DoubleQuote = 1,
        /// <summary>Replaces single quotes with \u2019 (alternative for Java .properties files: this is the right single curly quote).</summary>
        SpecialQuote = 2
    }
}
