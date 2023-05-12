using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AlchemicalFlux.Utilities.Helpers
{
    public static class StringManipulation
    {
        /// <summary>
        /// Replaces all keys with their respective values within the supplied text.
        /// Will attempt to match using the longest keys first.
        /// </summary>
        /// <param name="text">Entry to be parsed for replacement.</param>
        /// <param name="replacements">Terms to be searched(keys) and replaced(values).</param>
        /// <returns>Parsed output containing original text with replaced values.</returns>
        public static string MultipleReplace(string text, Dictionary<string, string> replacements)
        {
            if (replacements == null || replacements.Count == 0)
            {
                return text;
            }

            // Escape any special characters in the dictionary keys
            var escapedKeys = replacements.Keys.Select(key => $"(?<{key.Length}>{Regex.Escape(key)})").
                OrderByDescending(m => m);

            // Concatenates all keys to a searchable pattern.
            // Upon finding an entry to replace, it attempts to match with key / value in the replacements.
            return Regex.Replace(text,
                "(" + string.Join("|", escapedKeys) + ")",
                delegate (Match m)
                {
                    // Use TryGetValue to avoid KeyNotFoundException
                    if (replacements.TryGetValue(m.Value, out string replacement))
                    {
                        return replacement;
                    }

                    // If the key is not found, return the original value (without replacement)
                    return m.Value;
                }
            );
        }

        /// <summary>
        /// Replaces all keys with their respective values within the supplied text.
        /// Will attempt to match using the longest keys first.
        /// Should be able to handle Regex expressions as Keys.
        /// </summary>
        /// <param name="text">Entry to be parsed for replacement.</param>
        /// <param name="replacements">Terms to be searched(keys) and replaced(values).</param>
        /// <returns>Parsed output containing original text with replaced values.</returns>
        public static string RegexMultipleReplace(string text, Dictionary<string, string> replacements)
        {
            if (replacements == null || replacements.Count == 0)
            {
                return text;
            }

            // Create a regex pattern that matches any of the keys and captures the length of the match
            var orderedList = replacements.Select(m => m.Key).OrderByDescending(n => n);
            var pattern = "(" + string.Join("|", orderedList.ToArray()) + ")";

            // Concatenates all keys to a searchable pattern.
            // Upon finding an entry to replace, it matches it with all keys that qualify,
            //     picking the first from the returned list.
            return Regex.Replace(text, pattern, 
                delegate (Match m)
                {
                    // Find the longest matching replacement
                    var results = from result in replacements
                                    where Regex.Match(m.Value, result.Key, RegexOptions.Singleline).Success
                                    orderby result.Value.Length descending
                                    select result;

                    // If there are matches, use the longest one; otherwise, return the original value
                    return results.FirstOrDefault().Value ?? m.Value;
                }
            );
        }
    }
}