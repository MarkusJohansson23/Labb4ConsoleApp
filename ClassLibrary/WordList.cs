using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    public class WordList
    {
        //Read-only field
        public static readonly string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb4WorkShopApp"); //TODO: Change Labb4WorkShopApp

        //Auto-implemented properties
        public string Name { get; }
        public string[] Languages { get; }
        private List<Word> Words { get; set; }

        //Constructor
        public WordList(string name, params string[] languages)
        {
            Name = name;
            Languages = new string[languages.Length];
            for (int i = 0; i < languages.Length; i++)
            {
                Languages[i] = languages[i].ToLower();
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
                if (content.Length > 0)
                {
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
                    throw new IndexOutOfRangeException("Cannot read file. The file is empty");
                }
            }
            else
            {
                throw new ArgumentException("Could not find path");
            }
        }
        public void Save()//Markus
        {
            if (!File.Exists(Path.Combine(folder, Name + ".dat")))
            {
                File.Create(Path.Combine(folder, Name + ".dat"));
            }

            using (var sw = new StreamWriter(Path.Combine(folder, Name + ".dat")))
            {
                sw.WriteLine(string.Join(";", Languages));
                for (int i = 0; i < Words.Count; i++)
                {
                    sw.WriteLine(string.Join(";", Words[i].Translations));
                }
            }
            //Sparar listan till en fil med samma namn som listan och filändelse .dat
        }
        public void Add(params string[] translations)//Kamil
        {
            //Lägger till ord i listan. Kasta ArgumentException om det är fel antal translations

            if (Words == null)
                Words = new List<Word>();

            translations = translations.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            translations = translations.Select(x => x.Trim()).ToArray();
            translations = translations.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            if (translations.Length == Languages.Length)
            {
                Words.Add(new Word(translations.Select(x => x.ToLower()).ToArray()));
            }
            else
            {
                throw new ArgumentException("Invalid number of translations added");
            }
        }
        public bool Remove(int translation, string word)//Markus
        {
            return false;
            //translation motsvarar index i Languages. Sök igenom språket och ta bort ordet.
        }
        public int Count()//Kamil
        {
            if (Words != null)
            {
                return Words.Count;
            }
            return 0;
        }
        public void List(int sortByTranslation, Action<string[]> showTranslations)//markus
        {
            //sortByTranslation = Vilket språk listan ska sorteras på.
            //showTranslations = Callback som anropas för varje ord i listan.
        }
        public Word GetWordToPractice()//Kamil
        {
            Random rng = new Random();
            if (Words != null)
            {
                int fromLanguages = rng.Next(Languages.Length), toLanguages = rng.Next(Languages.Length);
                while (fromLanguages == toLanguages)
                {
                    fromLanguages = rng.Next(Languages.Length);
                    toLanguages = rng.Next(Languages.Length);
                }
                return new Word(fromLanguages, toLanguages, Words[rng.Next(Words.Count)].Translations);
            }
            //Returnerar slumpmässigt Word från listan, med slumpmässigt valda FromLanguage och ToLanguage(dock inte samma).
            throw new ArgumentNullException("No Word objects found");                            
        }
    }
}
