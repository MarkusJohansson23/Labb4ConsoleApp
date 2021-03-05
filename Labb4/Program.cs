using System;
using System.IO;
using ClassLibrary;

namespace Labb4
{
    class Program
    {
        private static readonly string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private static readonly string folder = Path.Combine(appDataPath, @"Labb4WorkShopApp");      //<-- DON'T Forget to change this after done with draft

        static void Main(string[] args)
        {
            //Console Applcation

            /* Man ska kunna skapa listor, lägga till och ta bort ord, öva, m.m genom att skicka
            in olika argument till programmet. Om man inte skickar några argument (eller
            felaktiga argument) ska följande skrivas ut: */


            if (!Directory.Exists(folder))
                Directory.CreateDirectory(folder);

            Console.WriteLine("Use any of the following parameters:");
            Console.WriteLine("-lists");
            Console.WriteLine("-new <list name> <language 1> <language 2> .. <language n>");
            Console.WriteLine("-add <list name>");
            Console.WriteLine("-remove <list name> <language> <word 1> <word 2> .. <word n>");
            Console.WriteLine("-words <listname> <sortByLanguage>");
            Console.WriteLine("-count <listname>");
            Console.WriteLine("-practice <listname>");
            Console.WriteLine();
            var testTest = new WordList("example1", "swedish", "english");
            testTest.Add("bla bla");
            var test = WordList.LoadList("example - kopia");

            
            
            Console.ReadKey();
        }
    }
}
