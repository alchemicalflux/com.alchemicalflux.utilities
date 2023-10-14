/*------------------------------------------------------------------------------
  File:           FolderData.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Data associated with the Git operation list view.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-13 19:00:09 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.GitOperations
{
    /// <summary>
    /// Contains the necessary data for tracking changes made to a list view element.
    /// </summary>
    public class FolderData : ScriptableObject
    {
        #region Members

        /// <summary>Value containing the associated folder path.</summary>
        public string FolderPath;

        /// <summary>Flag indicating if the PreCommits should be processed.</summary>
        public bool IncludePreCommits;

        /// <summary>Flag indicating if the Workflows should be processed.</summary>
        public bool IncludeWorkflows;

        #endregion Members

        #region Methods

        /// <summary>
        /// Initalization of the default values.
        /// </summary>
        /// <param name="path"></param>
        public FolderData(string path)
        {
            FolderPath = path;
            IncludePreCommits = false;
            IncludeWorkflows = false;
        }

        #endregion Methods
    }
}
