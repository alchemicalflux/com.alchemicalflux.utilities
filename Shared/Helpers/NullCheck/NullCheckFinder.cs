/*------------------------------------------------------------------------------
  File:           NullCheckFinder.cs 
  Project:        AlchemicalFlux Utilities
  Description:    This class contains method for the retrieval of failed
                    NullCheck attributes.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-11-29 20:46:10 
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
        private const BindingFlags _defaultSearchFlags =
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
            while(processing.Count > 0)
            {
                var obj = processing.Pop();
                CollectNullCheckViolations(obj);
                AddChildrenForProcessing(obj);
            }
            return errorsOnGameObject;

            #region Local Helpers

            // Helper method to collect NullCheck violations within the specified GameObject.
            void CollectNullCheckViolations(GameObject obj)
            {
                var results = AttributeFieldFinder.FindFieldsWithAttribute<NullCheck>(obj,
                    _defaultSearchFlags, IsPrefab, NoViolation);
                errorsOnGameObject.AddRange(results);
            }

            // Helper method to add a GameObject's children for processing.
            void AddChildrenForProcessing(GameObject obj)
            {
                foreach(Transform child in obj.transform) { processing.Push(child.gameObject); }
            }

            #endregion Local Helpers
        }

        /// <summary>
        /// Checks if a field is attached to a prefab and should be ignored.
        /// </summary>
        private static bool IsPrefab(MonoBehaviour monoBehaviour, FieldInfo fieldInfo, NullCheck att)
        {
            // Value type fields should be processed.
            if(fieldInfo.FieldType.IsValueType) { return false; }

            var result = false;

            #if UNITY_EDITOR

            // Prefabs can be ignored if the flag is set.
            var prefabType = PrefabUtility.GetPrefabAssetType(monoBehaviour.gameObject);
            result = prefabType != PrefabAssetType.NotAPrefab && att.IgnorePrefab;

            #endif

            return result;
        }

        /// <summary>
        /// Checks if there is a null violation on a field.
        /// </summary>
        private static bool NoViolation(FieldInfo fieldInfo, object obj, NullCheck att)
        {
            // Being attached to non-object references is considered a violation.
            if(fieldInfo.FieldType.IsValueType) { return false; }

            // Check that the object reference is not null.
            var fieldObj = fieldInfo.GetValue(obj);
            return fieldObj != null && !fieldObj.Equals(null);
        }

        #endregion Methods
    }
}
