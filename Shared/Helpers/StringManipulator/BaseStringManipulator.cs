/*------------------------------------------------------------------------------
  File:           BaseStringManipulator.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Contains shared functionality for string manipulators.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-20 07:36:29 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Contains sharable functionality for derived string manipulators.
    /// </summary>
    public abstract class BaseStringManipulator : IStringManipulator
    {
        /// <inheritdoc />
        public abstract string MultipleReplace(string originalText, Dictionary<string, string> replacements);

        /// <summary>
        /// Helper function that encapsulates the checking of the MultiplerReplace parameters.
        /// Will throw an <see cref="ArgumentException"/> if either parameter is null.
        /// </summary>
        /// <param name="text">Entry to be parsed for replacement.</param>
        /// <param name="replacements">Terms to be searched(keys) and replaced(values).</param>
        /// <returns>Determines if either of the parameters are empty.</returns>
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