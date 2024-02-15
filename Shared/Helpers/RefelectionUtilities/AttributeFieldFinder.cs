/*------------------------------------------------------------------------------
  File:           AttributeFieldFinder.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Provides methods to find fields with a specified attribute 
                    within MonoBehaviours attached to a GameObject.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-02-10 11:12:07 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Provides functionality to find fields with a specified Attribute within MonoBehaviours attached to a GameObject.
    /// </summary>
    public class AttributeFieldFinder
    {
        #region Methods

        /// <summary>
        /// Finds fields with the specified Attribute within MonoBehaviours attached to a GameObject.
        /// </summary>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <param name="obj">The GameObject to inspect.</param>
        /// <param name="reflectionFlags">Optional BindingFlags to control the scope of reflection.</param>
        /// <param name="monoBehaviourIgnoreCheck">Optional function to determine whether a MonoBehaviour field should be ignored.</param>
        /// <param name="attributeIgnoreCheck">Optional function to determine whether a field with the specified attribute should be ignored.</param>
        /// <returns>A list of FieldAssociation objects representing erroring fields.</returns>
        public static List<FieldAssociation> FindFieldsWithAttribute<T>(GameObject obj,
            BindingFlags reflectionFlags = BindingFlags.Default,
            Func<MonoBehaviour, FieldInfo, T, bool> monoBehaviourIgnoreCheck = null,
            Func<FieldInfo, object, T, bool> attributeIgnoreCheck = null) where T : Attribute
        {
            monoBehaviourIgnoreCheck ??= (_, _, _) => false; // Functor false if the original is null.

            var fieldAssociations = new List<FieldAssociation>();
            var monobehaviours = obj.GetComponents<MonoBehaviour>();
            foreach (var monoBehavior in monobehaviours)
            {
                if (monoBehavior == null) { continue; }

                var fields = ReflectionUtility.GetFieldsWithAttribute(monoBehavior, 
                    reflectionFlags, attributeIgnoreCheck);
                foreach(var (field, attribute) in fields)
                {
                    if(monoBehaviourIgnoreCheck(monoBehavior, field, attribute)) { continue; }

                    fieldAssociations.Add(new FieldAssociation(field, monoBehavior));
                }
            }
            return fieldAssociations;
        }

        #endregion Methods
    }
}