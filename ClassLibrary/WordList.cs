using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassLibrary
{
    public class WordList
    {
        //Fields
        private static readonly string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb4WorkShopApp"); //TODO: Change Labb4WorkShopApp

        //Auto-implemented properties
        public string Name { get; }                     //Namnet på listan
        public string[] Languages { get; }              //Namnen på språken
        private List<Word> Words { get; set; }          //Lista av typ Word

        //Constructor
        public WordList(string name, params string[] languages)
        {
            Name = name;
            Languages = new string[languages.Length];
            for (int i = 0; i < languages.Length; i++)
            {
                Languages[i] = languages[i];
            }
        }

        //Methods
        public static string[] GetLists()//Markus
        {
            string[] files = Directory.GetFiles(folder, "*.dat", SearchOption.AllDirectories);
            string[] nameArray = new string[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                nameArray[i] = Path.GetFileNameWithoutExtension(files[i]);
            }

            return nameArray;    //Returnerar array med namn på alla listor som finns lagrade (utan filändelsen). 
        }
        public static WordList LoadList(string name)//Kamil
        {
            if (File.Exists(Path.Combine(folder, name + ".dat")))
            {
                string[] content = File.ReadAllLines(Path.Combine(folder, name + ".dat"));
                string[] languages = content[0].Split(';', StringSplitOptions.RemoveEmptyEntries);
                WordList wordList = new WordList(name, languages);
                wordList.Words = new List<Word>();

                for (int i = 1; i < content.Length; i++)
                {
                    wordList.Words.Add(new Word(content[i].Split(';', StringSplitOptions.RemoveEmptyEntries)));
                }

                return wordList;    //Laddar in ordlistan (name anges utan filändelse) och returnerar som WordList.
            }
            else
            {
                Console.WriteLine("The file does not exist"); //Fix later TODO

                return new WordList("Test", "Test");
            }
        }
        public void Save()//Markus
        {
            //Sparar listan till en fil med samma namn som listan och filändelse .dat
        }
        public void Add(params string[] translations)//Kamil
        {
            //Lägger till ord i listan. Kasta ArgumentException om det är fel antal translations.
            if (Words != null || translations.Length != Languages.Length) //rätt logik?
            {
                string[] words = new string[Languages.Length];
                for (int i = 0; i < Languages.Length; i++)
                {
                    words[i] = translations[i];
                }
                Words.Add(new Word(words));
            }
            else
            {
                throw new ArgumentException("Incorrect number of translations added");
            }
        }
        public bool Remove(int translation, string word)//markus
        {
            return false;                                       //translation motsvarar index i Languages. Sök igenom språket och ta bort ordet.
        }
        public int Count()//Kamil
        {
            return 0;                                           //Räknar och returnerar antal ord i listan.
        }
        public void List(int sortByTranslation, Action<string[]> showTranslations)//markus
        {
            //sortByTranslation = Vilket språk listan ska sorteras på.
            //showTranslations = Callback som anropas för varje ord i listan.
        }
        public Word GetWordToPractice()//Kamil
        {
            return new Word("Test");                            //Returnerar slumpmässigt Word från listan, med slumpmässigt valda FromLanguage och ToLanguage(dock inte samma).

        }
    }
}
