/*------------------------------------------------------------------------------
  File:           StringManipulation.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Helper functions for useful string manipulations.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-13 01:50:32 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Store helpful operations for string manipulation.
    /// </summary>
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
            if (AreParametersInvalid(text, replacements))
            {
                return text;
            }

            // Escape any special characters in the dictionary keys and order by longest, then alphabetical.
            var escapedKeys = replacements.Keys.Select(key => $"(?<{key.Length}>{Regex.Escape(key)})").
                OrderByDescending(key => key.Length).
                ThenBy(key => key);

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

                    // If the key is not found, return the original value (without replacement).
                    return m.Value;
                }
            );
        }

        /// <summary>
        /// Replaces all keys with their respective values within the supplied text.
        /// Will attempt to match using the longest keys first.
        /// Should be able to handle Regex expressions as Keys.
        /// Escape and special characters will need to be encapsulated with a Regex.Escape().
        /// </summary>
        /// <param name="text">Entry to be parsed for replacement.</param>
        /// <param name="replacements">Terms to be searched(keys) and replaced(values).</param>
        /// <returns>Parsed output containing original text with replaced values.</returns>
        public static string RegexMultipleReplace(string text, Dictionary<string, string> replacements)
        {
            if (AreParametersInvalid(text, replacements))
            {
                return text;
            }

            // Create a regex pattern that matches any of the keys and orders by length of the match.
            var orderedList = replacements.Select(m => m.Key)
                .OrderByDescending(key => key.Length)
                .ThenBy(key => key);
            var pattern = "(" + string.Join("|", orderedList) + ")";

            // Concatenates all keys to a searchable pattern.
            // Upon finding an entry to replace, it matches it with all keys that qualify,
            //     picking the first from the returned list.
            return Regex.Replace(text, pattern,
                delegate (Match m)
                {
                    // Find the longest matching key
                    var results = from replacement in replacements
                                  where Regex.Match(m.Value, replacement.Key, RegexOptions.Singleline).Success
                                  orderby replacement.Key.Length descending
                                  select replacement;

                    // If there are matches, use the longest one; otherwise, return the original value.
                    return results.FirstOrDefault().Value ?? m.Value;
                }
           );
        }

        /// <summary>
        /// Checks if parameters have valid values that can be processed.
        /// </summary>
        /// <param name="text">Entry to be parsed for replacement.</param>
        /// <param name="replacements">Terms to be searched(keys) and replaced(values).</param>
        private static bool AreParametersInvalid(string text, Dictionary<string, string> replacements)
        {
            // Handle parameter checks and short circuts.
            if (text == null || replacements == null)
            {
                var paramName = (text == null) ? nameof(text) : string.Empty;
                paramName += ((paramName != string.Empty && replacements == null) ? " and " : string.Empty);
                paramName += (replacements == null ? nameof(replacements) : string.Empty);

                var message = "The '" + paramName + "' parameter(s) cannot be null.";

                throw new ArgumentNullException(paramName, message);
            }

            return text == string.Empty || replacements.Count == 0;
        }
    }
}