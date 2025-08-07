using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace XmlDefinitionsTester
{
    internal class Program
    {
        private class CountryDefinition
        {
            public string Name;
            public string NameAlt;
            public string Alpha2;
            public string Alpha3;
            public int Numeric;
            public string Domain;

            public string MemberName
            {
                get { return ToCamelCase(Name); }
            }

            private static string ToCamelCase(string name)
            {
                var normalized = NormalizeName(name);
                var parts = normalized.Split(' ');
                
                return string.Join("", parts
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Select(p => Camelize(p)).ToArray());
            }

            private static string NormalizeName(string name)
            {
                name = name.Replace(",", " ");
                name = name.Replace("'", " ");
                name = name.Replace("(", " ");
                name = name.Replace(")", " ");
                name = name.Replace("-", " ");
                name = name.Replace(".", " ");

                return RemoveDiacritics(name);
            }

            private static string Camelize(string part)
            {
                if (string.IsNullOrWhiteSpace(part)) return string.Empty;
                if (part.Length == 1) return part.ToUpperInvariant();

                return part[0].ToString().ToUpperInvariant() +
                   part.Substring(1).ToLowerInvariant();
            }

            private static string RemoveDiacritics(string input)
            {
                var builder = new StringBuilder();
                var characters = input
                    .Normalize(NormalizationForm.FormD)
                    .Where(c =>
                    {
                        var category = CharUnicodeInfo.GetUnicodeCategory(c);
                        return category != UnicodeCategory.NonSpacingMark;
                    });

                builder.Append(characters.ToArray());
                return (builder.ToString().Normalize(NormalizationForm.FormC));
            }
        }

        static void Main(string[] args)
        {
            var thisPath = ".";

            var countries = new Dictionary<string, CountryDefinition>();

            var xdocAlpha2 = new XmlDocument();
            xdocAlpha2.Load(Path.Combine(thisPath, @"data\countries-alpha2-domain.xml"));
            var rootAlpha2 = xdocAlpha2.ChildNodes.Cast<XmlNode>().SingleOrDefault(n => n.Name == "countries");
            if (rootAlpha2 != null && rootAlpha2.ChildNodes.Count > 0)
            {
                countries = rootAlpha2.ChildNodes.Cast<XmlNode>()
                    .Where(xn => 
                        !(xn is XmlComment) &&     
                        xn.Attributes != null && 
                        xn.Attributes.Count > 0 &&
                        xn.Attributes["name"] != null &&
                        !string.IsNullOrEmpty(xn.Attributes["name"].Value))
                    .Select(xn => new CountryDefinition()
                {
                    Name = xn.Attributes["name"].Value,
                    NameAlt = xn.Attributes["name-alt"].Value,
                    Alpha2 = xn.Attributes["alpha2"].Value.ToUpperInvariant(),
                    Domain = xn.Attributes["domain"].Value.ToLowerInvariant()
                }).ToDictionary(x => x.Name);
            }

            var c1 = countries.Count;

            var xdocAlpha3 = new XmlDocument();
            xdocAlpha3.Load(Path.Combine(thisPath, @"data\countries-alpha3.xml"));
            var rootAlpha3 = xdocAlpha3.ChildNodes.Cast<XmlNode>().SingleOrDefault(n => n.Name == "countries");
            if (rootAlpha3 != null && rootAlpha3.ChildNodes.Count > 0)
            {
                var nodes = rootAlpha3.ChildNodes.Cast<XmlNode>().Where(xn =>
                    !(xn is XmlComment) &&
                    xn.Attributes != null &&
                    xn.Attributes.Count > 0 &&
                    xn.Attributes["name"] != null &&
                    !string.IsNullOrEmpty(xn.Attributes["name"].Value));

                foreach (var node in nodes)
                {
                    var name = node.Attributes["name"].Value;
                    var alpha3 = node.Attributes["alpha3"].Value.ToUpperInvariant();
                    if (countries.ContainsKey(name))
                        countries[name].Alpha3 = alpha3;
                    else
                        countries.Add(name, new CountryDefinition() { Name = name, Alpha3 = alpha3 });
                }
            }

            var c2 = countries.Count;

            var xdocNumeric = new XmlDocument();
            xdocNumeric.Load(Path.Combine(thisPath, @"data\countries-numeric.xml"));
            var rootNumeric = xdocNumeric.ChildNodes.Cast<XmlNode>().SingleOrDefault(n => n.Name == "countries");
            if (rootNumeric != null && rootAlpha2.ChildNodes.Count > 0)
            {
                var nodes = rootNumeric.ChildNodes.Cast<XmlNode>().Where(xn =>
                    !(xn is XmlComment) &&
                    xn.Attributes != null &&
                    xn.Attributes.Count > 0 &&
                    xn.Attributes["name"] != null &&
                    !string.IsNullOrEmpty(xn.Attributes["name"].Value));

                foreach (var node in nodes)
                {
                    var name = node.Attributes["name"].Value;
                    var numeric = int.Parse(node.Attributes["numeric"].Value);
                    if (countries.ContainsKey(name))
                        countries[name].Numeric = numeric;
                    else
                        countries.Add(name, new CountryDefinition() { Name = name, Numeric = numeric });
                }
            }

            var c3 = countries.Count;
        }
    }
}
