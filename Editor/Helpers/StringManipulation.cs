using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AlchemicalFlux.Utilities.Helpers
{
    public static class StringManipulation
    {
        /// <summary>
        /// Replaces all keys with their respective values within the supplied text.
        /// </summary>
        /// <param name="text">Entry to be parsed for replacement.</param>
        /// <param name="replacements">Terms to be search(keys) and replaced(values).</param>
        /// <returns>Parsed output containing original text with replaced values.</returns>
        public static string MultipleReplace(string text, Dictionary<string, string> replacements)
        {
            // Concatenates all keys to a searchable pattern.
            // Upon finding an entry to replace, it attempts to match with key/value in the replacements.
            return Regex.Replace(text,
                "(" + String.Join("|", replacements.Keys.ToArray()) + ")",
                delegate (Match m) { return replacements[m.Value]; }
            );
        }

        /// <summary>
        /// Replaces all keys with their respective values within the supplied text.
        /// Should be able to handle Regex expressions as Keys.
        /// </summary>
        /// <param name="text">Entry to be parsed for replacement.</param>
        /// <param name="replacements">Terms to be search(keys) and replaced(values).</param>
        /// <returns>Parsed output containing original text with replaced values.</returns>
        public static string RegexMultipleReplace(string text, Dictionary<string, string> replacements)
        {
            // Concatenates all keys to a searchable pattern.
            // Upon finding an entry to replace, it matches it with all keys that qualify,
            //     picking the first from the returned list.
            return Regex.Replace(text,
                "(" + String.Join("|", replacements.Keys.ToArray()) + ")",
                delegate (Match m) {
                    var results = from result in replacements
                                  where Regex.Match(m.Value, result.Key, RegexOptions.Singleline).Success
                                  select result;
                    return results.First().Value;
                }
            );
        }
    }
}