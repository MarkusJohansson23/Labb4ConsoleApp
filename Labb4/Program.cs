using System;
using System.IO;
using System.Linq;
using ClassLibrary;

namespace Labb4
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console Applcation

            /* Man ska kunna skapa listor, lägga till och ta bort ord, öva, m.m genom att skicka
            in olika argument till programmet. Om man inte skickar några argument (eller
            felaktiga argument) ska följande skrivas ut: */

            if (!Directory.Exists(WordList.folder))
                Directory.CreateDirectory(WordList.folder);

            ShowOptions();

            bool flag = true;
            while (flag)
            {
                string[] parameters = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine(new string('-', 70));
                switch (parameters[0].ToLower())                                                        //Don't forget that the remaining array is not ToLower();
                {
                    case "-lists":
                        Console.WriteLine(string.Join(Environment.NewLine, WordList.GetLists()));
                        Console.WriteLine(new string('-', 70));
                        break;
                    case "-new":
                        break;
                    case "-add":
                        if (parameters.Length > 1)
                        {

                        }
                        else
                        {
                            Console.Write("Input list name without the .dat extension: ");
                            string name = Console.ReadLine();
                            var wordList = WordList.LoadList(name);                                     //Might encapsulate this for later reuse 
                            int counter = 0;
                            bool condition = true;
                            while (condition)
                            {
                                string[] translations = new string[wordList.Languages.Length];
                                for (int i = 0; i < translations.Length; i++)
                                {
                                    Console.Write("Input a word in {0}: ", wordList.Languages[i]);
                                    translations[i] = Console.ReadLine();
                                    if (translations[i] == " ")
                                    {
                                        condition = false;
                                        break;
                                    }
                                }
                                if (translations[0] != " ")
                                {
                                    wordList.Add(translations);
                                    counter++;
                                }
                            }
                            Console.WriteLine("\n{0} Word object(s) added to the list.", counter);      //Fix later with tryparse
                            Console.Write("Do you wish to save (y/n): ");
                            string save = Console.ReadLine().ToLower();
                            switch (save)
                            {
                                case "y":                                   //add yes and/or variations of y
                                    wordList.Save();
                                    break;
                                case "n":                                   //add no and/or variations of n
                                    break;
                                default:
                                    Console.WriteLine("Invalid input");
                                    break;
                            }
                            Console.WriteLine(new string('-', 70));
                            ShowOptions();
                        }
                        break;
                    case "-remove":
                        break;
                    case "-words":
                        break;
                    case "-count":
                        break;
                    case "-practice":
                        break;
                    case "-help":
                        ShowOptions();
                        break;
                    case "-exit":
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            }
        }
        private static void ShowOptions()
        {
            Console.WriteLine("Use any of the following parameters:");
            Console.WriteLine(new string('-', 70));
            Console.WriteLine("-lists");
            Console.WriteLine("-new <list name> <language 1> <language 2> .. <language n>");
            Console.WriteLine("-add <list name>");
            Console.WriteLine("-remove <list name> <language> <word 1> <word 2> .. <word n>");
            Console.WriteLine("-words <listname> <sortByLanguage>");
            Console.WriteLine("-count <listname>");
            Console.WriteLine("-practice <listname>");
            Console.WriteLine("-help");
            Console.WriteLine("-exit");
            Console.WriteLine(new string('-', 70));
        }
    }
}
