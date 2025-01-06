/*------------------------------------------------------------------------------
File:       FindNullChecksOnLoad.cs 
Project:    AlchemicalFlux Utilities
Overview:   This class initializes and executes a search for null check 
            violations  upon the launch of the Unity Editor. It performs a 
            search for null check violations within scene objects and assets 
            upon the first launch of the editor.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 16:56:47 
------------------------------------------------------------------------------*/
#if UNITY_EDITOR

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
        #region Methods

        /// <summary>
        /// Static constructor. Initializes the class and schedules a one-time search for null check violations
        /// upon the launch of the Unity Editor in debug builds.
        /// </summary>
        static FindNullChecksOnLoad()
        {
            if(!Debug.isDebugBuild) { return; }

            // Schedule a one-time search for null check violations upon the first launch of the editor.
            EditorApplication.update += CheckOnStart;

            //EditorApplication.playModeStateChanged += CheckOnExitingEditMode;
        }

        /// <summary>
        /// Executes a one-time search for null check violations within scene objects and assets.
        /// </summary>
        private static void CheckOnStart()
        {
            // Unsubscribe from the update event to ensure it only runs once.
            EditorApplication.update -= CheckOnStart;

            // Being in play mode means only the assets and current scene can be proecessed.
            var foundErrors = NullCheckProcessing.ProcessGameObjectsInAssetDatabase();
            foundErrors |= NullCheckProcessing.ProcessGameObjectsInScene();

            // If null check violations are found, stop play mode in the editor.
            if(foundErrors)
            {
                EditorApplication.isPlaying = false;
            }
        }

        /// <summary>
        /// Checks for null check violations upon exiting edit mode and stops play mode if any are found.
        /// </summary>
        /// <param name="stateChange">The state change event triggered upon exiting edit mode.</param>
        /*
        private static void CheckOnExitingEditMode(PlayModeStateChange stateChange)
        {
            if(stateChange != PlayModeStateChange.ExitingEditMode) { return; }

            // Unity is still in edit mode, so assets and all scenes can be processed.
            var foundErrors = NullCheckProcessing.ProcessGameObjectsInAssetDatabase();
            foundErrors |= NullCheckProcessing.ProcessGameObjectsInAllScenes();

            // If null check violations are found, stop play mode in the editor.
            if(foundErrors)
            {
                EditorApplication.isPlaying = false;
            }
        }
        */

        #endregion Methods
    }
}
#endif