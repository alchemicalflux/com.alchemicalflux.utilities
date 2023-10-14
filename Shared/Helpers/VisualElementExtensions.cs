/*------------------------------------------------------------------------------
  File:           VisualElementExtensions.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Helper functions to extend the VisualElement class.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-13 19:00:09 
------------------------------------------------------------------------------*/
using UnityEngine.UIElements;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Stores extensions for the VisualElement class.
    /// </summary>
    public static class VisualElementExtensions
    {
        #region Methods

        /// <summary>
        /// Query extension that searches by the template inferred class type.
        /// </summary>
        /// <typeparam name="TType">Class type that will be querried.</typeparam>
        /// <param name="element">Element whose hierachy will be searched.</param>
        /// <param name="target">Variable that will store the querried search.</param>
        /// <param name="name">Name of the item to be querried.</param>
        public static void Q<TType>(this VisualElement element, ref TType target, string name)
            where TType : VisualElement
        {
            target = element.Q<TType>(name);
        }

        #endregion Methods
    }
}
