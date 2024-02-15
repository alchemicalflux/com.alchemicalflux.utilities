/*------------------------------------------------------------------------------
  File:           NullCheckFinder.cs 
  Project:        AlchemicalFlux Utilities
  Description:    This class contains method for the retrieval of failed
                    NullCheck attributes.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-02-15 04:42:09 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Utility class for finding and NullCheck violations.
    /// </summary>
    public class NullCheckFinder
    {
        #region Members

        /// <summary>Binding pattern to find fields that use NullCheck attribute.</summary>
        private const BindingFlags DefaultSearchFlags =
            BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

        #endregion Members

        #region Members

        /// <summary>
        /// Retrieves fields that fail the null check validation for a game object.
        /// </summary>
        /// <param name="gameObject">The game object to check.</param>
        /// <returns>List of null check violations.</returns>
        public static List<FieldAssociation> RetrieveErrors(GameObject gameObject)
        {
            var processing = new Stack<GameObject>();
            processing.Push(gameObject);

            var errorsOnGameObject = new List<FieldAssociation>();
            while (processing.Count > 0)
            {
                var obj = processing.Pop();

                var results = AttributeFieldFinder.FindFieldsWithAttribute<NullCheck>(obj,
                    DefaultSearchFlags, IsPrefab, NoViolation);
                errorsOnGameObject.AddRange(results);

                foreach (Transform child in obj.transform)
                {
                    processing.Push(child.gameObject);
                }
            }
            return errorsOnGameObject;
        }

        /// <summary>
        /// Checks if a field is attached to a prefab and should be ignored.
        /// </summary>
        private static bool IsPrefab(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, NullCheck att)
        {
            // Value type fields should be processed.
            if (fieldInfo.FieldType.IsValueType) { return false; }

            // Prefabs can be ignored if the flag is set.
            var prefabType = PrefabUtility.GetPrefabAssetType(monoBehaviour.gameObject);
            return prefabType != PrefabAssetType.NotAPrefab && att.IgnorePrefab;
        }

        /// <summary>
        /// Checks if there is a null violation on a field.
        /// </summary>
        private static bool NoViolation(FieldInfo fieldInfo, object obj, NullCheck att)
        {
            // Being attahed to Non-object references is considered a violation.
            if(fieldInfo.FieldType.IsValueType) { return false; }

            // Check that the object reference is not null.
            var fieldObj = fieldInfo.GetValue(obj);
            return fieldObj != null && !fieldObj.Equals(null);
        }

        #endregion Methods
    }
}
