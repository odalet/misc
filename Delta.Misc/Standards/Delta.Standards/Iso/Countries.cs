using System.Collections.Generic;

namespace Delta.Standards.Iso
{
    /// <summary>
    /// List of countries along with their ISO 3166-1 alpha-2, alpha-3 and numeric codes, its top-level domain
    /// </summary>
    /// <remarks>
    /// See:
    /// * http://en.wikipedia.org/wiki/ISO_3166-1_alpha-2
    /// * http://www.iso.org/iso/country_codes.htm
    /// </remarks>
    public partial class Countries
    {                
        public static IEnumerable<Country> All
        {
            get { return countries; }
        }
    }
}
