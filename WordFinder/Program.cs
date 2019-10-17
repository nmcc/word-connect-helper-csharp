using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using WordFinder.Core;

namespace WordFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            var wordFinder = WordFinderBuilder.FromFile("words.txt");
            Console.WriteLine("Dictionary indexed");

            do
            {
                Console.WriteLine();

                Console.Write("Length:");
                var length = Convert.ToInt32(Console.ReadLine());

                Console.Write("Letters:");
                var searchLetters = Console.ReadLine();

                var result = wordFinder.SearchWord(searchLetters, length);

                foreach (var word in result.OrderBy(_ => _))
                {
                    Console.WriteLine(word);
                }
            } while (true);

            Console.WriteLine("Press any key");
            Console.ReadKey();
        }
    }
}
