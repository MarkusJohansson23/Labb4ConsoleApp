using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassLibrary
{
    public class WordList
    {
        //Auto-implemented properties
        private static readonly string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb4WorkShopApp"); //TODO: change Labb4WorkShopApp
        public string Name { get; }             //Namnet på listan
        public string[] Languages { get; }      //Namnen på språken
        private List<Word> Words { get; set; }
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
                string name = Path.GetFileNameWithoutExtension(files[i]);
                nameArray[i] = name;
            }

            return nameArray;    //Returnerar array med namn på alla listor som finns lagrade (utan filändelsen). 
        }
        public static WordList LoadList(string name)// Kamil
        {
            return new WordList("Test", "Test");                //Laddar in ordlistan (name anges utan filändelse) och returnerar som WordList.
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
            /*if(File.Exist(folder))
             * Print "File with this name alreary exists. Do you want to overwrite it?"
             * if("yes") --> overwrite
             * if("no") --> break
             */
            //Sparar listan till en fil med samma namn som listan och filändelse .dat
        }
        public void Add(params string[] translations)//Kamil
        {
            //Lägger till ord i listan. Kasta ArgumentException om det är fel antal translations.
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
