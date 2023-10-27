/*------------------------------------------------------------------------------
  File:           RegexStringManipulator.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Contains string manipulators that can handle regex parameters.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-24 12:34:07 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// String manipulation that can handle regex modifiers within parameters.
    /// </summary>
    public class RegexStringManipulator : BaseStringManipulator
    {
        /// <inheritdoc />
        /// <remarks>
        /// This method will perform a search and replace on the text using the 
        /// supplied replacements. Can process regex compatable strings.
        /// </remarks>
        public override string MultipleReplace(string originalText, Dictionary<string, string> replacements)
        {
            if (AreParametersInvalid(originalText, replacements, nameof(originalText), nameof(replacements)))
            {
                return originalText;
            }

            // Create a regex pattern that matches any of the keys and orders by length of the match.
            var orderedList = replacements.Select(m => m.Key)
                .OrderByDescending(key => key.Length)
                .ThenBy(key => key);
            var pattern = "(" + string.Join("|", orderedList) + ")";

            // Concatenates all keys to a searchable pattern.
            // Upon finding an entry to replace, it matches it with all keys that qualify,
            //     picking the first from the returned list.
            return Regex.Replace(originalText, pattern,
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
    }
}