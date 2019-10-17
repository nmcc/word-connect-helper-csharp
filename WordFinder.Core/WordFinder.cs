using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordFinder.Core
{
    public class WordFinder
    {
        private const int LETTERS = 26;
        private HashSet<string>[] wordsByFirstLetter;

        internal WordFinder(IEnumerable<string> words)
        {
            this.wordsByFirstLetter = new HashSet<string>[LETTERS];

            for (int i = 0; i < LETTERS; i++)
            {
                this.wordsByFirstLetter[i] = new HashSet<string>();
            }

            foreach (var word in words)
            {
                foreach (var letter in word.ToUpper())
                {
                    this.wordsByFirstLetter[CharPosition(letter)]
                        .Add(word.ToUpper());
                }
            }
        }

        public IEnumerable<string> SearchWord(string searchLetters, int wordLength)
        {
            searchLetters = searchLetters.ToUpper();

            var regex = new Regex("^[" + new string(searchLetters) + "]{" + wordLength + "}$");

            var result = new HashSet<string>();
            foreach (var letter in searchLetters)
            {
                var setForLetter = this.wordsByFirstLetter[CharPosition(letter)]
                    .Where(word => regex.IsMatch(word))
                    .Where(word => WordSearch(word, searchLetters));

                result.UnionWith(setForLetter);
            }

            //foreach (var letter in searchLetters)
            //{
            //    if (result == null)
            //        result = setForLetter;
            //    else
            //        result.Intersect(setForLetter);
            //}

            return result;

        }

        private static bool WordSearch(string word, string searchLetters)
        {
            var s = new string(searchLetters).ToList();

            foreach (var letter in word)
            {
                if (!s.Contains(letter))
                {
                    // Remove words that contain letters that are not in the search letters
                    return false;
                }

                // Remove the usage of this letter to avoid repetitions
                s.Remove(letter);
            }

            return true;
        }

        private static int CharPosition(char c) => c - 'A';

    }
}
