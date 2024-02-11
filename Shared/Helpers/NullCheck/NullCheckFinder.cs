/*------------------------------------------------------------------------------
  File:           NullCheckFinder.cs 
  Project:        AlchemicalFlux Utilities
  Description:    This utility class provides functionality for finding and 
                    processing null check violations in Unity scenes and assets. 
                    It offers methods to process all scenes in a project, search
                    for null check violations in game objects within scenes or 
                    assets, and handle errors associated with null check 
                    violations. The class is designed to be used within the 
                    Unity Editor environment to aid in debugging and ensuring 
                    code quality.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-02-10 22:52:43 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Utility class for finding and processing null check violations in Unity scenes and assets.
    /// </summary>
    public class NullCheckFinder : EditorWindow
    {
        #region Members

        /// <summary>Binding pattern needed to </summary>
        private const BindingFlags DefaultSearchFlags =
            BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

        private const string DefaultSceneOutputText = "In current scene.";

        #endregion Members

        #region Members

        /// <summary>
        /// Processes all scenes in the project, checking for null check violations.
        /// </summary>
        /// <returns>True if any violations are found; otherwise, false.</returns>
        public static bool ProcessAllScenes()
        {
            if(!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) { return false; }

            var originalSceneSetup = EditorSceneManager.GetSceneManagerSetup();

            var scenePaths = AssetDatabase.FindAssets("t:Scene", new[] { "Assets" });
            var foundErrors = false;
            foreach (var scenePath in scenePaths)
            {
                string sceneName = AssetDatabase.GUIDToAssetPath(scenePath);
                EditorSceneManager.OpenScene(sceneName, OpenSceneMode.Single);
                foundErrors |= ProcessGameObjectsInScene(sceneName);
            }

            EditorSceneManager.RestoreSceneManagerSetup(originalSceneSetup);

            return foundErrors;
        }

        /// <summary>
        /// Processes game objects in the current scene, checking for null check violations.
        /// </summary>
        /// <param name="pathToAsset">Path to the asset.</param>
        /// <returns>True if any violations are found; otherwise, false.</returns>
        public static bool ProcessGameObjectsInScene(string pathToAsset = DefaultSceneOutputText)
        {
            var sceneGameObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            var foundErrors = false;
            foreach (var sceneGameObject in sceneGameObjects)
            {
                if (sceneGameObject.transform.parent != null) { continue; }

                foundErrors |= ProcessErrorsForObject(sceneGameObject, pathToAsset);
            }
            return foundErrors;
        }

        /// <summary>
        /// Processes game objects in assets, checking for null check violations.
        /// </summary>
        /// <param name="ignorePath">Path to ignore while searching for game objects.</param>
        /// <returns>True if any violations are found; otherwise, false.</returns>
        public static bool ProcessGameObjectsInAssetDatabase(string ignorePath)
        {
            var guidsForAllGameObjects = AssetDatabase.FindAssets("t:GameObject");
            var foundErrors = false;
            foreach (var guid in guidsForAllGameObjects)
            {
                var pathToGameObject = AssetDatabase.GUIDToAssetPath(guid);

                if (pathToGameObject.Contains(ignorePath)) { continue; }

                var gameObject = (GameObject)AssetDatabase.LoadAssetAtPath(pathToGameObject, 
                    typeof(GameObject));

                foundErrors |= ProcessErrorsForObject(gameObject, pathToGameObject);
            }
            return foundErrors;
        }

        /// <summary>
        /// Processes null check errors for a specific game object.
        /// </summary>
        /// <param name="gameObject">The game object to process.</param>
        /// <param name="pathToAsset">Path to the asset.</param>
        /// <returns>True if any violations are found; otherwise, false.</returns>
        private static bool ProcessErrorsForObject(GameObject gameObject, string pathToAsset)
        {
            var processing = new Stack<GameObject>();
            processing.Push(gameObject);

            var foundErrors = false;
            while (processing.Count > 0)
            {
                var obj = processing.Pop();

                var errorsOnGameObject = RetrieveErrors(gameObject);
                foreach (var violation in errorsOnGameObject)
                {
                    Debug.LogError($"{violation}\nPath: {pathToAsset}", violation.GameObject);
                    foundErrors = true;
                }

                foreach (Transform child in obj.transform)
                {
                    processing.Push(child.gameObject);
                }
            }
            return foundErrors;
        }

        /// <summary>
        /// Retrieves fields that fail the null check validation for a game object.
        /// </summary>
        /// <param name="gameObject">The game object to check.</param>
        /// <returns>List of null check violations.</returns>
        private static List<FieldAssociation> RetrieveErrors(GameObject gameObject)
        {
            return AttributeFieldFinder.FindFieldsWithAttribute<NullCheck>(gameObject, 
                DefaultSearchFlags, IsPrefab, NoViolation);
        }

        /// <summary>
        /// Checks if a field is attached to a prefab and should be ignored.
        /// </summary>
        private static bool IsPrefab(MonoBehaviour monoBehaviour, FieldInfo field, NullCheck att)
        {
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
            if(fieldInfo.FieldType.IsValueType || fieldInfo.FieldType.IsEnum) { return false; }

            // Check that the object reference is not null.
            var fieldObj = fieldInfo.GetValue(obj);
            return fieldObj != null && !fieldObj.Equals(null);
        }

        #endregion Methods
    }
}
