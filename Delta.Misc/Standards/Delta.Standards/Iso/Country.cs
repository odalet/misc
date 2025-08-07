using System;

namespace Delta.Standards.Iso
{
    /// <summary>
    /// Represents A country along with its ISO 3166/MA English short name, 
    /// its ISO 3166-1 alpha-2, alpha-3 and numeric codes, and its top level domain. 
    /// </summary>
    public class Country
    {
        public Country(string name, string alpha2, string alpha3, int numeric, string tld, string simpleName = "")
        {
            if (string.IsNullOrEmpty(name)) 
                throw new ArgumentException("The country name must be provided");

            Name = name;
            SimpleName = simpleName ?? name;
            
            TwoLettersCode = alpha2 ?? string.Empty;
            ThreeLettersCode = alpha3 ?? string.Empty;
            NumericCode = numeric;
            
            if (string.IsNullOrEmpty(tld))
                TopLevelDomain = string.Empty;
            else 
            {
                if (!tld.StartsWith("."))
                    TopLevelDomain = "." + tld;
                else TopLevelDomain = tld;
            }
        }

        /// <summary>
        /// Gets the English short country name officially used by the ISO 3166 Maintenance Agency (ISO 3166/MA)
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets an alternative, simpler but non official country name.
        /// </summary>
        /// <remarks>
        /// Most of the time, this has the same value than <see cref="Name"/>.
        /// </remarks>
        public string SimpleName { get; private set; }

        /// <summary>
        /// Gets the country's ISO 3166-1 alpha-2 code
        /// </summary>
        public string TwoLettersCode { get; private set; }
        
        /// <summary>
        /// Gets the country's ISO 3166-1 alpha-3 code
        /// </summary>
        public string ThreeLettersCode { get; private set; }

        /// <summary>
        /// Gets the country's ISO 3166-1 numeric code
        /// </summary>
        public int NumericCode { get; private set; }

        /// <summary>
        /// Gets the Country top level domain assigned by IANA as described by RFC 1591.
        /// </summary>
        public string TopLevelDomain { get; private set; }        
    }
}
