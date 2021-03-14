using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary
{
    public class Word
    {
        //Auto-implemented properties
        public string[] Translations { get; }   //Translations lagrar översättningarna, en för varje språk
        public int FromLanguage { get; }        //Språk som ska översättas till respektive från
        public int ToLanguage { get; }          //Språk som ska översättas till respektive från

        //Constructors
        public Word(params string[] translations)
        {
            FromLanguage = 0;                               //Temp
            ToLanguage = FromLanguage + 1;                  //Temp (out of range exception?)
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
