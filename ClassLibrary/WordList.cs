using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassLibrary
{
    public class WordList
    {
        //Auto-implemented properties
        public string Name { get; }             //Namnet på listan
        public string[] Languages { get; }      //Namnen på språken

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
            var fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Labb4WorkShopApp"); //TODO: Change Labb4WorkShopApp
            string[] files = Directory.GetFiles(fileName, "*.dat", SearchOption.AllDirectories);
            string[] nameArray = new string[files.Length];

            foreach (var item in files)
            {
                string name = Path.GetFileNameWithoutExtension(item);
                Console.WriteLine(name);
            }

            return nameArray;    //Returnerar array med namn på alla listor som finns lagrade (utan filändelsen). 
        }
        public static WordList LoadList(string name)// Kamil
        {
            return new WordList("Test", "Test");                //Laddar in ordlistan (name anges utan filändelse) och returnerar som WordList.
        }
        public void Save()//Markus
        {
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
