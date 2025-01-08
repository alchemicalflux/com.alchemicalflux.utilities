/*------------------------------------------------------------------------------
File:       FolderDataController.cs 
Project:    AlchemicalFlux Utilities
Overview:   Controller associated with the Git operation list view.
Copyright:  2023-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 17:05:53 
------------------------------------------------------------------------------*/
using UnityEngine.UIElements;

namespace AlchemicalFlux.Utilities.GitOperations
{
    /// <summary>
    /// Handles the storing and callbacks needed for UI data binding.
    /// </summary>
    public class FolderDataController
    {
        #region Members

        /// <summary>Handle to the UI elemenet's specific data.</summary>
        public FolderData Data;

        /// <summary>Handle to the UI element for the folder path.</summary>
        public Label FolderPathLabel;

        /// <summary>
        /// Handle to the UI element for the pre-commit toggle.
        /// </summary>
        public Toggle IncludePreCommitsToggle;

        /// <summary>
        /// Handle to the UI element for the semantic release toggle.
        /// </summary>
        public Toggle IncludeSemanticReleaseToggle;

        #endregion Members

        #region Methods

        /// <summary>
        /// Callback for updating the pre-commit flag on UI change.
        /// </summary>
        /// <param name="evt">Event information regarding UI change.</param>
        public void OnPreCommitChange(ChangeEvent<bool> evt)
        {
            Data.IncludePreCommits = evt.newValue;
        }

        /// <summary>
        /// Callback for updating the semantic release flag on UI change.
        /// </summary>
        /// <param name="evt">Event information regarding UI change.</param>
        public void OnSemanticReleaseChange(ChangeEvent<bool> evt)
        {
            Data.IncludeSemanticRelease = evt.newValue;
        }

        #endregion Methods
    }
}