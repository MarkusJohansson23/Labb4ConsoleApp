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
            //Console Application
            if (!Directory.Exists(WordList.folder))
                Directory.CreateDirectory(WordList.folder);

            string[] parameters = args;
            if (args.Length == 0)
            {
                Console.WriteLine(new string('-', 100));
                ShowOptions();
            }

            int counter = 0;
            bool flag = true;
            while (flag)
            {
                counter++;
                try
                {
                    if (counter > 1 || args.Length == 0)
                    {
                        parameters = Console.ReadLine()
                            .Split(new char[] { ' ', '.', ',', ';', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
                    }

                    string fileName = string.Empty;
                    if (parameters.Length > 1)
                    {
                        fileName = parameters[1];
                    }
                    parameters = Array.ConvertAll(parameters, x => x.ToLower());
                    Console.WriteLine(new string('-', 100));

                    switch (parameters[0])
                    {
                        case "-lists":
                            Console.WriteLine(string.Join(Environment.NewLine, WordList.GetLists()));
                            Console.WriteLine(new string('-', 100));
                            break;
                        case "-new":
                            if (parameters.Length > 2)
                            {
                                if (parameters.Length == 3)
                                {
                                    Console.WriteLine("We need more languages");
                                    Console.Write("Input a few more languages on this line (separated by a space): ");
                                    string[] moreLanguages = Console.ReadLine()
                                        .Split(new char[] { ' ', '.', ',', ';', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);

                                    Array.Resize(ref parameters, moreLanguages.Length + parameters.Length);
                                    for (int i = 3; i < parameters.Length; i++)
                                    {
                                        parameters[i] = moreLanguages[i - 3];
                                    }
                                }
                                WordList wordList = new WordList(fileName, parameters[2..(parameters.Length)]);
                                AddWordsPrompt(wordList);
                                SavePrompt(wordList);
                                Console.WriteLine(new string('-', 100));
                                ShowOptions();
                            }
                            else
                            {
                                string name = string.Empty;
                                if (parameters.Length != 2)
                                {
                                    Console.Write("Input list name without the .dat extension: ");
                                    name = Console.ReadLine();
                                }

                                Console.Write("Input all the languages (separated by a space) on this line: ");
                                string[] languages = Console.ReadLine()
                                    .Split(new char[] { ' ', '.', ',', ';', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);
                                if (languages.Length == 1)
                                {
                                    Console.WriteLine("\nWe need more languages");
                                    Console.Write("Input a few more languages on this line (separated by a space): ");
                                    string[] moreLanguages = Console.ReadLine()
                                        .Split(new char[] { ' ', '.', ',', ';', '<', '>' }, StringSplitOptions.RemoveEmptyEntries);

                                    Array.Resize(ref languages, moreLanguages.Length + 1);
                                    for (int i = 1; i < languages.Length; i++)
                                    {
                                        languages[i] = moreLanguages[i - 1];
                                    }
                                }

                                languages = Array.ConvertAll(languages, x => x.ToLower());
                                WordList wordList = new WordList(parameters.Length == 2 ? fileName : name, languages);
                                AddWordsPrompt(wordList);
                                SavePrompt(wordList);
                                Console.WriteLine(new string('-', 100));
                                ShowOptions();
                            }
                            break;
                        case "-add":
                            if (parameters.Length > 1)
                            {
                                var wordList = WordList.LoadList(fileName);
                                AddWordsPrompt(wordList);
                                SavePrompt(wordList);
                                Console.WriteLine(new string('-', 100));
                                ShowOptions();
                            }
                            else
                            {
                                Console.Write("Input list name without the .dat extension: ");
                                string name = Console.ReadLine();
                                var wordList = WordList.LoadList(name);
                                AddWordsPrompt(wordList);
                                SavePrompt(wordList);
                                Console.WriteLine(new string('-', 100));
                                ShowOptions();
                            }
                            break;
                        case "-remove":
                            if(parameters.Length > 1)
                            {

                            }
                            else
                            {
                                Console.Write("Input list name without the .dat extension: ");
                                string name = Console.ReadLine();
                                var wordList = WordList.LoadList(name);
                                RemoveWordsPrompt(wordList);
                                SavePrompt(wordList);
                                Console.WriteLine(new string('-', 100));
                                ShowOptions();
                            }
                            break;
                        case "-words":
                            break;
                        case "-count":
                            if (parameters.Length > 1)
                            {
                                var wordList = WordList.LoadList(fileName);
                                Console.WriteLine("There are {0} Word objects in the {1} list", wordList.Count(), wordList.Name);
                                Console.WriteLine(new string('-', 100));
                            }
                            else
                            {
                                Console.Write("Input list name without the .dat extension: ");
                                string name = Console.ReadLine();
                                var wordList = WordList.LoadList(name);
                                Console.WriteLine("\nThere are {0} Word objects in the {1} list", wordList.Count(), wordList.Name);
                                Console.WriteLine(new string('-', 100));
                            }
                            break;
                        case "-practice":
                            if (parameters.Length > 1)
                            {
                                var wordList = WordList.LoadList(fileName);
                                PracticeWordsPrompt(wordList);
                                Console.WriteLine(new string('-', 100));
                                ShowOptions();
                            }
                            else
                            {
                                Console.Write("Input list name without the .dat extension: ");
                                string name = Console.ReadLine();
                                var wordList = WordList.LoadList(name);
                                PracticeWordsPrompt(wordList);
                                Console.WriteLine(new string('-', 100));
                                ShowOptions();
                            }
                            break;
                        case "-options":
                            ShowOptions();
                            break;
                        case "-exit":
                            flag = false;
                            break;
                        default:
                            Console.WriteLine("Invalid input");
                            if (counter == 1)
                            {
                                Console.WriteLine(new string('-', 100));
                                ShowOptions();
                            }
                            break;
                    }
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("\n" + ane.Message);
                    Console.WriteLine(new string('-', 100));
                    ShowOptions();
                }
                catch (ArgumentException ae)
                {
                    Console.WriteLine("\n" + ae.Message);
                    Console.WriteLine(new string('-', 100));
                    ShowOptions();
                }
                catch (IndexOutOfRangeException ioore)
                {
                    Console.WriteLine("\n" + ioore.Message);
                    Console.WriteLine(new string('-', 100));
                    ShowOptions();
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n" + e.Message);
                    Console.WriteLine(new string('-', 100));
                    ShowOptions();
                } 
            }
        }
        private static void ShowOptions()
        {
            Console.WriteLine("Use any of the following parameters:");
            Console.WriteLine(new string('-', 100));
            Console.WriteLine("-lists");
            Console.WriteLine("-new <list name> <language 1> <language 2> .. <language n>");
            Console.WriteLine("-add <list name>");
            Console.WriteLine("-remove <list name> <language> <word 1> <word 2> .. <word n>");
            Console.WriteLine("-words <listname> <sortByLanguage>");
            Console.WriteLine("-count <listname>");
            Console.WriteLine("-practice <listname>");
            Console.WriteLine("-options");
            Console.WriteLine("-exit");
            Console.WriteLine(new string('-', 100));
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
            Console.WriteLine("Press \"enter\" to cancel the prompt below\n");
            while (condition)
            {
                string[] translations = new string[wordList.Languages.Length];
                for (int i = 0; i < translations.Length; i++)
                {
                    Console.Write("Input a word in {0}: ", wordList.Languages[i]);
                    translations[i] = Console.ReadLine();
                    if (translations[i] == "")
                    {
                        condition = false;
                        break;
                    }
                }
                if (translations[0] != "")
                {
                    wordList.Add(translations);
                    counter++;
                }
            }
            if (counter == 1)
            {
                Console.WriteLine("\n{0} Word object added to the list", counter);
            }
            else
            {
                Console.WriteLine("\n{0} Word objects added to the list", counter);
            }
        }
        private static void PracticeWordsPrompt(WordList wordList)
        {
            bool condition = true;
            int numerator = 0, denominator = 0;
            Console.WriteLine("Press \"enter\" to cancel the prompt below");
            while (condition)
            {
                var word = wordList.GetWordToPractice();
                string toLanguage = wordList.Languages[word.ToLanguage];
                string fromLanguage = word.Translations[word.FromLanguage];
                Console.Write("\nTranslate \"{0}\" to {1}: ", fromLanguage, toLanguage);
                string translation = Console.ReadLine().ToLower();
                if (translation == "")
                {
                    condition = false;
                    break;
                }
                if (translation.Equals(word.Translations[word.ToLanguage]))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Correct");
                    Console.ResetColor();
                    numerator++;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("Incorrect. ");
                    Console.ResetColor();
                    Console.Write("The correct word was: ");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(word.Translations[word.ToLanguage]);
                    Console.ResetColor();
                }
                denominator++;
            }
            if (numerator != 0 && numerator == denominator)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nCongratulations! ");
                Console.ResetColor();
                Console.WriteLine("You got {0}/{1} correct", numerator, denominator);
            }
            else if (denominator != 0)
            {
                Console.WriteLine("\nYou got {0}/{1} correct", numerator, denominator);
            }
            else
            {
                Console.WriteLine("\nNo words were guessed");
            }
        }
        private static void RemoveWordsPrompt(WordList wordList)
        {
            int counter = 0;
            bool condition = true;
            Console.Write("Input language: ");
            string language = Console.ReadLine().ToLower();
            string wordToRemove = string.Empty;
            int languageNumber = 0;
            bool removedOrNot = false;

            for (int i = 0; i < wordList.Languages.Length; i++)
            {
                if (language == wordList.Languages[i])
                {
                    Console.Write("What word do you want to remove? ");
                    wordToRemove = Console.ReadLine().ToLower();
                    languageNumber = i;
                    removedOrNot = wordList.Remove(languageNumber, wordToRemove);
                    break;
                }
            }
            
            if (removedOrNot == true)
            {
                Console.WriteLine($"The word \"{wordToRemove}\" and associated translations successfully removed.");
            }
            else
            {
                Console.WriteLine($"Could not find \"{wordToRemove}\". Word was not removed.");
            }


        }
    }
}
