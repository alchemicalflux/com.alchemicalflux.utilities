/*------------------------------------------------------------------------------
  File:           BasicStringManipulator.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Contains string manipulators that handle basic string values.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-17 13:42:06 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Contains string manipulations that work on plain string parameters.
    /// </summary>
    public class BasicStringManipulator : IStringManipulator
    {
        /// <inheritdoc />
        /// <remarks>
        /// This method will perform a simple search and replace on the text
        /// using the supplied replacements.
        /// </remarks>   
        public override string MultipleReplace(string text, Dictionary<string, string> replacements)
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
    }
}