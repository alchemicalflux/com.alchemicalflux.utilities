/*------------------------------------------------------------------------------
  File:           NullCheckMenuItems.cs 
  Project:        AlchemicalFlux Utilities
  Description:    This static class provides menu items for the NullCheck 
                    functionality within the Unity Editor. It offers options to 
                    process all scenes in the project and search for null check
                    violations within the current scene and assets.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-02-11 06:09:36 
------------------------------------------------------------------------------*/
using UnityEditor;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Provides menu items for the NullCheck functionality within the Unity Editor.
    /// </summary>
    public static class NullCheckMenuItems
    {
        #region Methods

        /// <summary>
        /// Menu item to process assets and all scenes in the project, searching for null check violations.
        /// </summary>
        [MenuItem("Tools/AlchemicalFlux Utilities/NullCheck/Process All")]
        public static void ProcessAllScenes()
        {
            NullCheckProcessing.ProcessGameObjectsInAssetDatabase();
            NullCheckProcessing.ProcessAllScenes();
        }

        /// <summary>
        /// Menu item to search for and report null check violations within the current scene and assets.
        /// </summary>
        [MenuItem("Tools/AlchemicalFlux Utilities/NullCheck/Process Current Scene")]
        public static void ProcessCurrentScene()
        {
            NullCheckProcessing.ProcessGameObjectsInAssetDatabase();
            NullCheckProcessing.ProcessGameObjectsInScene();
        }

        #endregion Methods
    }
}