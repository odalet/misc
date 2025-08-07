using System;
using System.Collections;

namespace TPropertyGrid
{
    /// <summary>This class is the translation engine</summary>
    public class T
    {
        public static bool UseEnglish = true;

        private static Hashtable translationsEn = new Hashtable();
        private static Hashtable translationsFr = new Hashtable();

        static T()
        {
            FillTranslations();
        }

        public static string Get(string key)
        {
            return Get(key, string.Empty);
        }

        public static string Get(string key, string defval)
        {
            Hashtable h = (UseEnglish ? translationsEn : translationsFr);

            if (h.ContainsKey(key))
                return (string)h[key];
            else return defval;
        }

        private static void FillTranslations()
        {
            translationsEn.Add("Name", "Complete name");
            translationsEn.Add("Age", "Age");
            translationsEn.Add("Height", "Height (m)");

            translationsEn.Add("NameDesc", "Owner's name");
            translationsEn.Add("AgeDesc", "Owner's age");
            translationsEn.Add("HeightDesc", "Owner's height (in meters)");

            translationsFr.Add("Name", "Nom complet");
            translationsFr.Add("Age", "Age");
            translationsFr.Add("Height", "Taille (m)");

            translationsFr.Add("NameDesc", "Nom du propriétaire");
            translationsFr.Add("AgeDesc", "Age du propriétaire");
            translationsFr.Add("HeightDesc", "Taille (en mètres) du propriétaire");

            translationsEn.Add("Strings", "Character strings");
            translationsEn.Add("Numbers", "Numeric values");

            translationsFr.Add("Strings", "Chaînes de caractères");
            translationsFr.Add("Numbers", "Valeurs numériques");

            translationsEn.Add("Family", "Family");
            translationsEn.Add("Sister1", "Sister #1");
            translationsEn.Add("Sister2", "Sister #2");
            translationsEn.Add("Sisters", "Sisters");
            translationsEn.Add("Sister1Desc", "My first sister");
            translationsEn.Add("Sister2Desc", "My second sister");
            translationsEn.Add("SistersDesc", "My two sisters");

            translationsFr.Add("Family", "Famille");
            translationsFr.Add("Sister1", "Soeur n° 1");
            translationsFr.Add("Sister2", "Soeur n° 2");
            translationsFr.Add("Sisters", "Soeurs");
            translationsFr.Add("Sister1Desc", "Ma premièrefirst soeur");
            translationsFr.Add("Sister2Desc", "Ma seconde soeur");
            translationsFr.Add("SistersDesc", "Mes deux soeurs");
        }
    }
}
