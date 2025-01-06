/*------------------------------------------------------------------------------
File:       NullCheckMenuItems.cs 
Project:    AlchemicalFlux Utilities
Overview:   This static class provides menu items for the NullCheck 
            functionality within the Unity Editor. It offers options to process
            all scenes in the project and search for null check violations 
            within the current scene and assets.
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
    /// Provides menu items for the NullCheck functionality within the Unity Editor.
    /// </summary>
    public static class NullCheckMenuItems
    {
        #region

        private const string _noErrorsMessage = "No NullCheck violations found.";

        #endregion

        #region Methods

        /// <summary>
        /// Menu item to process assets and all scenes in the project, searching for null check violations.
        /// </summary>
        [MenuItem("Tools/AlchemicalFlux Utilities/NullCheck/Process All")]
        public static void ProcessAll()
        {
            var errorsFound = false;
            errorsFound |= NullCheckProcessing.ProcessGameObjectsInAssetDatabase();
            errorsFound |= NullCheckProcessing.ProcessGameObjectsInAllScenes();
            DisplayNoErrorMessage(errorsFound);
        }

        /// <summary>
        /// Menu item to process assets in the project, searching for null check violations.
        /// </summary>
        [MenuItem("Tools/AlchemicalFlux Utilities/NullCheck/Process Asset Database")]
        public static void ProcessAssetDatabase()
        {
            var errorsFound = NullCheckProcessing.ProcessGameObjectsInAssetDatabase();
            DisplayNoErrorMessage(errorsFound);
        }

        /// <summary>
        /// Menu item to process all scenes in the project, searching for null check violations.
        /// </summary>
        [MenuItem("Tools/AlchemicalFlux Utilities/NullCheck/Process All Scenes")]
        public static void ProcessAllScenes()
        {
            var errorsFound = NullCheckProcessing.ProcessGameObjectsInAllScenes();
            DisplayNoErrorMessage(errorsFound);
        }

        /// <summary>
        /// Menu item to search for and report null check violations within the current scene and assets.
        /// </summary>
        [MenuItem("Tools/AlchemicalFlux Utilities/NullCheck/Process Current Scene")]
        public static void ProcessCurrentScene()
        {
            var errorsFound = NullCheckProcessing.ProcessGameObjectsInScene();
            DisplayNoErrorMessage(errorsFound);
        }

        /// <summary>
        /// Displays a message indicating the absence of null check violations.
        /// </summary>
        /// <param name="errorsFound">Flag indicating whether any errors were found.</param>
        private static void DisplayNoErrorMessage(bool errorsFound)
        {
            if(!errorsFound)
            {
                Debug.Log(_noErrorsMessage);
            }
        }

        #endregion Methods
    }
}

#endif