/*------------------------------------------------------------------------------
  File:           IStringManipulator.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Contains interface for string manipulating functionality.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-17 13:42:06 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Interface for string manipulation and any shared functionality.
    /// </summary>
    public abstract class IStringManipulator
    {
        /// <summary>
        /// Replaces all keys with their respective values within the supplied text.
        /// Will attempt to match using the longest keys first.
        /// </summary>
        /// <param name="text">Entry to be parsed for replacement.</param>
        /// <param name="replacements">Terms to be searched(keys) and replaced(values).</param>
        /// <returns>Parsed output containing original text with replaced values.</returns>
        public abstract string MultipleReplace(string text, Dictionary<string, string> replacements);

        /// <summary>
        /// Checks if parameters have valid values that can be processed.
        /// </summary>
        /// <param name="text">Entry to be parsed for replacement.</param>
        /// <param name="replacements">Terms to be searched(keys) and replaced(values).</param>
        protected bool AreParametersInvalid(string text, Dictionary<string, string> replacements)
        {
            // Handle parameter checks and short circuts.
            if (text == null || replacements == null)
            {
                var paramName = (text == null) ? nameof(text) : string.Empty;
                paramName += (paramName != string.Empty && replacements == null) ? " and " : string.Empty;
                paramName += (replacements == null) ? nameof(replacements) : string.Empty;

                var message = "The '" + paramName + "' parameter(s) cannot be null.";

                throw new ArgumentNullException(paramName, message);
            }

            return text == string.Empty || replacements.Count == 0;
        }
    }
}