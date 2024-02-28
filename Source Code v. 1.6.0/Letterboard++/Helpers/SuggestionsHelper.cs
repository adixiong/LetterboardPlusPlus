using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Letterboard__.Helpers
{
    public class SuggestionsHelper
    {
        private static List<string> Dictionary;
        private async static Task LoadDictionary()
        {
            await Task.Run(() => Dictionary = File.ReadAllLines("dictionary.txt").ToList());
        }

        public async static Task<List<string>> GetSuggestions(string partialWord)
        {
            if (Dictionary == null)
            {
                await LoadDictionary();
            }

            List<string> suggestions = new List<string>();

            await Task.Run(() => suggestions = Dictionary.Where(d => d.StartsWith(partialWord)).Take(3).ToList());

            return suggestions;
        }
    }
}
