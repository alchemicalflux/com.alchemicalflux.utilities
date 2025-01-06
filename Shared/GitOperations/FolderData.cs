/*------------------------------------------------------------------------------
File:       FolderData.cs 
Project:    AlchemicalFlux Utilities
Overview:   Data associated with the Git operation list view.
Copyright:  2023-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 16:56:47 
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

        /// <summary>Flag indicating if the Semantic Release should be processed.</summary>
        public bool IncludeSemanticRelease;

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
            IncludeSemanticRelease = false;
        }

        #endregion Methods
    }
}
