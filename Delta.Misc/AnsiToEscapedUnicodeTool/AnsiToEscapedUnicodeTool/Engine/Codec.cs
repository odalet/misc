using System.Text;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Delta.AnsiToEscapedUnicodeTool.Engine
{

    // Grabbed from http://stackoverflow.com/questions/1615559/converting-unicode-strings-to-escaped-ascii-string
    internal static class Codec
    {
        public static string EncodeNonAsciiCharacters(string value, CodecOptions codecOptions = null)
        {
            var options = codecOptions ?? CodecOptions.Defaults;

            var builder = new StringBuilder();
            foreach (char character in value)
            {
                if (character == '\'')
                {
                    switch (options.SingleQuoteEscapeMode)
                    {
                        case SingleQuoteEscapeMode.Normal:
                            builder.Append("\\u0027");
                            break;
                        case SingleQuoteEscapeMode.DoubleQuote:
                            builder.Append("\\u0027\\u027");
                            break;
                        case SingleQuoteEscapeMode.SpecialQuote:
                            builder.Append("\\u2019");
                            break;
                    }
                }
                else if (character > 127)
                {
                    // This character is too big for ASCII 7b
                    var encodedValue = "\\u" + ((int)character).ToString("x4");
                    builder.Append(encodedValue);
                }
                else builder.Append(character);
            }

            return builder.ToString();
        }

        public static string DecodeEncodedNonAsciiCharacters(string value, CodecOptions codecOptions = null)
        {
            var result = Regex.Replace(value, @"\\u(?<Value>[a-zA-Z0-9]{4})",
                m => ((char)int.Parse(m.Groups["Value"].Value, NumberStyles.HexNumber)).ToString());

            var options = codecOptions ?? CodecOptions.Defaults;
            if (options.ReinterpretQuotes)
            {
                result = result.Replace("\'\'", "\'");
                result = result.Replace('’', '\'');
            }

            return result;
        }
    }
}
