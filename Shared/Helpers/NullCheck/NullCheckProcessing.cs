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
  Last commit at: 2024-11-29 20:46:10 
------------------------------------------------------------------------------*/
#if UNITY_EDITOR

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    public class NullCheckProcessing : EditorWindow
    {
        #region Members

        /// <summary>Output for objects loaded in the current scene.</summary>
        private const string _defaultSceneOutputText = "In current scene.";

        /// <summary>Location of unit test assets.</summary>
        private const string _unitTestLocation = "Tests/Runtime/Resources/Helpers/NullCheck";

        #endregion Members

        #region Methods

        /// <summary>
        /// Processes all scenes in the project, checking for null check violations.
        /// </summary>
        /// <returns>True if any violations are found; otherwise, false.</returns>
        public static bool ProcessGameObjectsInAllScenes()
        {
            var foundErrors = false;

            if(!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) { return false; }

            var originalSceneSetup = EditorSceneManager.GetSceneManagerSetup();

            var scenePaths = AssetDatabase.FindAssets("t:Scene", new[] { "Assets" });
            foreach(var scenePath in scenePaths)
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
        public static bool ProcessGameObjectsInScene(string pathToAsset = _defaultSceneOutputText)
        {
            var sceneGameObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);
            var foundErrors = false;
            foreach(var sceneGameObject in sceneGameObjects)
            {
                if(sceneGameObject.transform.parent != null) { continue; }

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
            foreach(var guid in guidsForAllGameObjects)
            {
                var pathToGameObject = AssetDatabase.GUIDToAssetPath(guid);

                if(pathToGameObject.Contains(_unitTestLocation)) { continue; }

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
            var errorsOnGameObject = NullCheckFinder.RetrieveErrors(gameObject);
            foreach(var violation in errorsOnGameObject)
            {
                Debug.LogError($"{violation}\nPath: {pathToAsset}", violation.GameObject);
            }
            return errorsOnGameObject.Count > 0;
        }

        #endregion Methods
    }
}

#endif