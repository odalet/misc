using System;

namespace UriTest
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Dump("http://www.google.com/foo/bar/baz?q=1&r=2");
            Dump("/foo/bar/baz?q=1&r=2"); // throws: relative uri
            Dump("foo/bar/baz?q=1&r=2"); // throws: relative uri
            Dump("addup://www.google.com/foo/bar/baz?q=1&r=2");
            Dump("addup:///foo/bar/baz?q=1&r=2");
            Dump("urn:addup:foo:bar:baz");

            Dump("addup://machine-11@formup-350/components/head[1]/laser/power:min/");
            Dump("addup://machine-11@formup-350/components/head[1]/laser/power:min/");
            Dump("http://addup-central/machine-api/v1/type:formup-350/machine:machine-11/components/head[1]/laser/power:min/");

            Console.ReadKey();
        }

        private static void Dump(string uriAsString)
        {
            try
            {
                var uri = new Uri(Uri.EscapeUriString(uriAsString));
                Console.WriteLine("OK <= " + DumpUri(uri));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message} <= {uriAsString}");
            }
        }

        private static string DumpUri(Uri uri)
        {
            var details =
                $"\tScheme   : {uri.Scheme}\r\n" +
                $"\tUserInfo : {uri.UserInfo}\r\n" +
                $"\tHost     : {uri.Host} ({uri.HostNameType}, {uri.IdnHost})\r\n" +
                $"\tPort     : {uri.Port}\r\n" +
                $"\tAuthority: {uri.Authority}\r\n" +
                $"\tSegments : {string.Join(", ", uri.Segments)}\r\n" +
                $"\tQuery    : {uri.Query}\r\n";
            return uri.GetComponents((UriComponents)511, UriFormat.UriEscaped) + "\r\n" + details;
        }
    }
}
