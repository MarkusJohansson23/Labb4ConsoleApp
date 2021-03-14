using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Word
    {
        //Auto-implemented properties
        public string[] Translations { get; }
        public int FromLanguage { get; }
        public int ToLanguage { get; }

        //Constructors
        public Word(params string[] translations)
        {
            FromLanguage = 0;
            ToLanguage = FromLanguage + 1;        
            Translations = new string[translations.Length];
            for (int i = 0; i < translations.Length; i++)
            {
                Translations[i] = translations[i];
            }
        }
        public Word(int fromLanguage, int toLanguage, params string[] translations)
        {
            FromLanguage = fromLanguage;
            ToLanguage = toLanguage;
            Translations = new string[translations.Length];
            for (int i = 0; i < translations.Length; i++)
            {
                Translations[i] = translations[i];
            }
        }
    }
}
