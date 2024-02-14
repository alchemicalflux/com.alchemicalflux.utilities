/*------------------------------------------------------------------------------
  File:           NullCheckProcessing.cs 
  Project:        AlchemicalFlux Utilities
  Description:    This utility class provides functionality processing NullCheck
                    violations in Unity scenes and assets. It offers methods to 
                    process all scenes in a project and handle errors associated
                    with NullCheck violations. The class is designed to be used
                    within the Unity Editor environment to aid in debugging and
                    ensuring code quality.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-02-13 18:35:48 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    public class NullCheckProcessing : EditorWindow
    {
        #region Members

        /// <summary>Output for objects loaded in the current scene.</summary>
        private const string DefaultSceneOutputText = "In current scene.";

        /// <summary>Location of unit test assets.</summary>
        private const string UnitTestLocation = "Tests/Runtime/Resources/Helpers/NullCheck";

        #endregion Members

        #region Methods

        /// <summary>
        /// Processes all scenes in the project, checking for null check violations.
        /// </summary>
        /// <returns>True if any violations are found; otherwise, false.</returns>
        public static bool ProcessAllScenes()
        {
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) { return false; }

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
        /// <returns>True if any violations are found; otherwise, false.</returns>
        public static bool ProcessGameObjectsInAssetDatabase()
        {
            var guidsForAllGameObjects = AssetDatabase.FindAssets("t:GameObject");
            var foundErrors = false;
            foreach (var guid in guidsForAllGameObjects)
            {
                var pathToGameObject = AssetDatabase.GUIDToAssetPath(guid);

                if (pathToGameObject.Contains(UnitTestLocation)) { continue; }

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

                var errorsOnGameObject = NullCheckFinder.RetrieveErrors(gameObject);
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

        #endregion Methods
    }
}