/*------------------------------------------------------------------------------
  File:           FindNullChecksOnLoad.cs 
  Project:        AlchemicalFlux Utilities
  Description:    This class initializes and executes a search for null check 
                    violations  upon the launch of the Unity Editor. It performs
                    a search for null check violations within scene objects and 
                    assets upon the first launch of the editor.
  Copyright:      �2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-02-11 04:10:06 
------------------------------------------------------------------------------*/
using UnityEditor;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Initializes and executes a search for null check violations upon the launch of the Unity Editor.
    /// </summary>
    [InitializeOnLoad]
    public class FindNullChecksOnLoad
    {
        #region Members

        /// <summary>Location of unit test assets.</summary>
        private const string _unitTestLocation = "NullCheck/Tests";

        #endregion Members

        #region Methods

        /// <summary>
        /// Static constructor. Initializes the class and schedules a one-time search for null check violations
        /// upon the launch of the Unity Editor in debug builds.
        /// </summary>
        static FindNullChecksOnLoad()
        {
            if (!Debug.isDebugBuild) { return; }

            // Schedule a one-time search for null check violations upon the first launch of the editor.
            //EditorApplication.update += CheckOnStart;

            EditorApplication.playModeStateChanged += CheckOnExitingEditMode;
        }

        /// <summary>
        /// Executes a one-time search for null check violations within scene objects and assets.
        /// </summary>
        private static void CheckOnStart()
        {
            // Unsubscribe from the update event to ensure it only runs once.
            EditorApplication.update -= CheckOnStart;

            // Being in play mode means only the assets and current scene can be proecessed.
            var foundErrors = NullCheckFinder.ProcessGameObjectsInAssetDatabase(_unitTestLocation);
            foundErrors |= NullCheckFinder.ProcessGameObjectsInScene();

            // If null check violations are found, stop play mode in the editor.
            if (foundErrors)
            {
                #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
                #endif
            }
        }

        private static void CheckOnExitingEditMode(PlayModeStateChange stateChange)
        {
            if(stateChange != PlayModeStateChange.ExitingEditMode) { return; }

            // Unity is still in edit mode, so assets and all scenes can be processed.
            var foundErrors = NullCheckFinder.ProcessGameObjectsInAssetDatabase(_unitTestLocation);
            foundErrors |= NullCheckFinder.ProcessAllScenes();

            // If null check violations are found, stop play mode in the editor.
            if (foundErrors)
            {
                #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
                #endif
            }
        }

        #endregion Methods
    }
}