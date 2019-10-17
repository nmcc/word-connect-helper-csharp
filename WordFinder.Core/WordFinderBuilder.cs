using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordFinder.Core
{
    public static class WordFinderBuilder
    {
        private static Regex lowercaseRegex = new Regex(@"^[a-z]+$", RegexOptions.Compiled);

        public static WordFinder FromFile(string filePath)
        {
            var wordFilter = new Regex(@"^[a-zA-Z]{3,7}$", RegexOptions.Compiled);

            var words = File.ReadLines(filePath)
                .Where(line => wordFilter.IsMatch(line))
                .Where(word => IsLowercase(word));

            return new WordFinder(words);
        }

        private static bool IsLowercase(string word) => lowercaseRegex.IsMatch(word);
    }
}
