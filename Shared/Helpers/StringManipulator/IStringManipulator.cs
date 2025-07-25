/*------------------------------------------------------------------------------
File:       IStringManipulator.cs 
Project:    AlchemicalFlux Utilities
Overview:   Contains interface for string manipulating functionality.
Copyright:  2023-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 17:05:53 
------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Interface for string manipulation and any shared functionality.
    /// </summary>
    public interface IStringManipulator
    {
        /// <summary>
        /// Replaces all keys with their respective values within the supplied 
        /// text. Will attempt to match using the longest keys first.
        /// </summary>
        /// <param name="originalText">
        /// Entry to be parsed for replacement.
        /// </param>
        /// <param name="replacements">
        /// Terms to be searched(keys) and replaced(values).
        /// </param>
        /// <returns>
        /// Parsed output containing original text with replaced values.
        /// </returns>
        string MultipleReplace(string originalText, 
            Dictionary<string, string> replacements);
    }
}