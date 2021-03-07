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
                string[] parameters = Console.ReadLine().Split(' ');
                Console.WriteLine(new string('-', 70));
                switch (parameters[0].ToLower())
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
                            Console.WriteLine("Input list name without .dat");
                        }
                        Console.WriteLine();
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
