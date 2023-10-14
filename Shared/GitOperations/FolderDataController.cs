/*------------------------------------------------------------------------------
  File:           FolderDataController.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Controller associated with the Git operation list view.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-13 19:00:09 
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

        /// <summary>Handle to the UI element for the precommit toggle.</summary>
        public Toggle IncludePreCommitsToggle;

        /// <summary>Handle to the UI element for the workflow toggle.</summary>
        public Toggle IncludeWorkflowsToggle;

        #endregion Members

        #region Methods

        /// <summary>
        /// Callback for updating the precommit flag on UI change.
        /// </summary>
        /// <param name="evt">Event information regarding UI change.</param>
        public void OnPreCommitChange(ChangeEvent<bool> evt)
        {
            Data.IncludePreCommits = evt.newValue;
        }

        /// <summary>
        /// Callback for updating the workflow flag on UI change.
        /// </summary>
        /// <param name="evt">Event information regarding UI change.</param>
        public void OnWorkflowChange(ChangeEvent<bool> evt)
        {
            Data.IncludeWorkflows = evt.newValue;
        }

        #endregion Methods
    }
}