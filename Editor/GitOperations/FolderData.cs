/*------------------------------------------------------------------------------
  File:           FolderData.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Data associated with the Git operation list view.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-08-31 16:12:05 
------------------------------------------------------------------------------*/
using UnityEngine.UIElements;

namespace AlchemicalFlux.Utilities.GitOperations
{
    public class FolderData
    {
        #region Members

        /// <summary>Value containing the associated folder path.</summary>
        public string FolderPath;
        /// <summary>Handle to the FolderPath UI element.</summary>
        public Label FolderPathLabel;

        /// <summary>Flag indicating if the PreCommits should be processed.</summary>
        public bool IncludePreCommits;
        /// <summary>Handle to the IncludePreCommits UI element.</summary>
        public Toggle PreCommitToggle;

        /// <summary>Flag indicating if the Workflows should be processed.</summary>
        public bool IncludeWorkflows;
        /// <summary>Handle to the IncludeWorkflows UI element.</summary>
        public Toggle WorkflowsToggle;

        #endregion Members

        #region Methods

        /// <summary>
        /// Initalization of the default values.
        /// </summary>
        /// <param name="path"></param>
        public FolderData(string path)
        {
            FolderPath = path;
            FolderPathLabel = null;

            IncludePreCommits = false;
            PreCommitToggle = null;

            IncludeWorkflows = false;
            WorkflowsToggle = null;
        }

        /// <summary>
        /// Registers callbacks for the UI elements update their associated values.
        /// </summary>
        public void RegisterCallbacks()
        {
            PreCommitToggle.RegisterCallback<ChangeEvent<bool>>(UpdatePreCommitValue);
            WorkflowsToggle.RegisterCallback<ChangeEvent<bool>>(UpdateWorkflowsValue);
        }

        /// <summary>
        /// Unregisters the UI element callbacks.
        /// </summary>
        public void UnregisterCallbacks()
        {
            PreCommitToggle?.UnregisterCallback<ChangeEvent<bool>>(UpdatePreCommitValue);
            WorkflowsToggle?.UnregisterCallback<ChangeEvent<bool>>(UpdateWorkflowsValue);
        }

        /// <summary>
        /// Callback for updating the precommit flag.
        /// </summary>
        /// <param name="evt">Event data.</param>
        private void UpdatePreCommitValue(ChangeEvent<bool> evt)
        {
            IncludePreCommits = evt.newValue;
        }

        /// <summary>
        /// Callback for updatinf the workflows flag.
        /// </summary>
        /// <param name="evt">Event data.</param>
        private void UpdateWorkflowsValue(ChangeEvent<bool> evt)
        {
            IncludeWorkflows = evt.newValue;
        }

        #endregion Methods
    }
}
