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

            Console.WriteLine(new string('-', 70));
            ShowOptions();

            bool flag = true;
            while (flag)
            {
                string[] parameters = Console.ReadLine()
                    .Split(new char[] { ' ', '.', ',', ';', '_' }, StringSplitOptions.RemoveEmptyEntries);
                parameters = Array.ConvertAll(parameters, x => x.ToLower());

                Console.WriteLine(new string('-', 70));
                switch (parameters[0].ToLower())
                {
                    case "-lists":
                        Console.WriteLine(string.Join(Environment.NewLine, WordList.GetLists()));
                        Console.WriteLine(new string('-', 70));
                        break;
                    case "-new":
                        if (parameters.Length > 1)      //Need to check what happens when null in constructor or out of range. Should still work I think.
                        {
                            WordList wordList = new WordList(parameters[1], parameters[2..(parameters.Length)]);
                            //Is there a way to to make parameters[1] case sensitive?
                            AddWordsPrompt(wordList);
                            SavePrompt(wordList);
                            Console.WriteLine(new string('-', 70));
                            ShowOptions();
                        }
                        else
                        {
                            if (parameters.Length > 2) // Fix later
                            {
                                //Console.Write("Input list name without the .dat extension: "); for later
                            }
                            Console.Write("Input list name without the .dat extension: ");
                            string name = Console.ReadLine();
                            Console.Write("Input languages on this line: ");
                            string[] languages = Console.ReadLine()
                                .Split(new char[] { ' ', '.', ',', ';', '_' }, StringSplitOptions.RemoveEmptyEntries);
                            languages = Array.ConvertAll(languages, x => x.ToLower());
                            WordList wordList = new WordList(name, languages);
                            AddWordsPrompt(wordList);
                            SavePrompt(wordList);
                            Console.WriteLine(new string('-', 70));
                            ShowOptions();
                        }
                        break;
                    case "-add":
                        if (parameters.Length > 1)
                        {
                            var wordList = WordList.LoadList(parameters[1]);
                            AddWordsPrompt(wordList);
                            SavePrompt(wordList);
                            Console.WriteLine(new string('-', 70));
                            ShowOptions();
                        }
                        else
                        {
                            Console.Write("Input list name without the .dat extension: ");
                            string name = Console.ReadLine().ToLower();
                            var wordList = WordList.LoadList(name);
                            AddWordsPrompt(wordList);
                            SavePrompt(wordList);
                            Console.WriteLine(new string('-', 70));
                            ShowOptions();
                        }
                        break;
                    case "-remove":
                        break;
                    case "-words":
                        break;
                    case "-count":
                        if (parameters.Length > 1)
                        {
                            var wordList = WordList.LoadList(parameters[1]);
                            Console.WriteLine("There are {0} Word objects in the {1} list", wordList.Count(), wordList.Name);
                            Console.WriteLine(new string('-', 70));
                        }
                        else
                        {
                            Console.Write("Input list name without the .dat extension: ");
                            string name = Console.ReadLine().ToLower();
                            var wordList = WordList.LoadList(name);
                            Console.WriteLine("\nThere are {0} Word objects in the {1} list", wordList.Count(), wordList.Name);
                            Console.WriteLine(new string('-', 70));
                        }
                        break;
                    case "-practice":
                        break;
                    case "-commands":
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
            Console.WriteLine("-commands");
            Console.WriteLine("-exit");
            Console.WriteLine(new string('-', 70));
        }
        private static void SavePrompt(WordList wordList)
        {
            bool flag = true;
            Console.Write("Do you wish to save (y/n): ");
            while (flag)
            {
                string save = Console.ReadLine().ToLower();
                if (save == "y" || save == "yes")
                {
                    flag = false;
                    wordList.Save();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0}.dat was save sucessfully", wordList.Name);
                    Console.ResetColor();
                }
                else if (save == "n" || save == "no")
                {
                    flag = false;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0}.dat was not saved", wordList.Name);
                    Console.ResetColor();
                }
                else
                {
                    Console.Write("Invalid input. Please type \"yes\" or \"no\": ");
                }
            }
        }
        private static void AddWordsPrompt(WordList wordList)
        {
            int counter = 0;
            bool condition = true;
            Console.WriteLine("Press \"space\" then \"enter\" to cancel the prompt below\n");
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
            Console.WriteLine("\n{0} Word object(s) added to the list.", counter);
        }
    }
}
