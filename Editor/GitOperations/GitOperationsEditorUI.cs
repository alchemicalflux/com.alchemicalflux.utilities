/*------------------------------------------------------------------------------
  File:           GitOperationsEditorUI.cs 
  Project:        AlchemicalFlux Utilities
  Description:    UI encapsulation for handling Git Operations.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-08-31 16:12:05 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Helpers;
using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace AlchemicalFlux.Utilities.GitOperations
{
    /// <summary>
    /// Handles the collection and setup of necessary UI elements for package generation.
    /// </summary>
    public class GitOperationsEditorUI
    {
        #region Members

        /// <summary>UI text entry for the parent folder path.</summary>
        private TextField parentFolderTextField;

        /// <summary>UI button that triggers the parent folder path search functionality.</summary>
        private Button folderSearchButton;
        /// <summary>UI button that triggers the gathering of all Git folder paths.</summary>
        private Button gatherFoldersButton;

        /// <summary>UI list view interface for accessing and modifying which folders will be processed.</summary>
        private ListView gatheredFoldersList;

        /// <summary>Callbacks triggered on the folder search button press.</summary>
        public Action OnSearchPressed;
        /// <summary>Callbacks triggered on the gather folder button press.</summary>
        public Action OnGatherPressed;

        #endregion Members

        #region Properties

        /// <summary>
        /// Current parent folder path.
        /// </summary>
        public string ParentFolderPath => parentFolderTextField.text;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes all of the UI elements necessary for handling Git operations.
        /// </summary>
        /// <param name="rootVisualElement">Parent element that contains all the necessary UI.</param>
        public GitOperationsEditorUI(VisualElement rootVisualElement, VisualTreeAsset listViewAsset)
        {
            // Gather the associated UI references.
            rootVisualElement.Q(ref parentFolderTextField, GitConstants.ParentFolderFieldName);
            rootVisualElement.Q(ref folderSearchButton, GitConstants.FolderSearchButtonName);
            rootVisualElement.Q(ref gatherFoldersButton, GitConstants.GatherFoldersButtonName);
            rootVisualElement.Q(ref gatheredFoldersList, GitConstants.GatheredFoldersListName);

            // Bind the UI events with a callback handler.
            folderSearchButton.clicked += () => OnSearchPressed?.Invoke();
            gatherFoldersButton.clicked += () => OnGatherPressed?.Invoke();

            // Handle the making, binding, and unbinding of the list view items.
            gatheredFoldersList.makeItem = () => listViewAsset.Instantiate();

            gatheredFoldersList.bindItem = (elem, index) =>
            {
                // Gather the associated UI references.
                var data = gatheredFoldersList.itemsSource[index] as FolderData;

                elem.Q(ref data.FolderPathLabel, GitConstants.FolderPathName);
                elem.Q(ref data.PreCommitToggle, GitConstants.PreCommitName);
                elem.Q(ref data.WorkflowsToggle, GitConstants.GitHubWorkflowName);

                // Set the values based on the provided data.
                data.FolderPathLabel.text = data.FolderPath;
                data.PreCommitToggle.SetValueWithoutNotify(data.IncludePreCommits);
                data.WorkflowsToggle.SetValueWithoutNotify(data.IncludeWorkflows);

                data.RegisterCallbacks();
            };

            gatheredFoldersList.unbindItem = (elem, index) =>
            {
                // Gather the associated UI references.
                var data = gatheredFoldersList.itemsSource[index] as FolderData;

                data.UnregisterCallbacks();
            };
        }

        /// <summary>
        /// Sets the parent folder path without notifications.
        /// </summary>
        /// <param name="path">Value to be displayed in the parent folder text field.</param>
        public void SetParentFolder(string path)
        {
            parentFolderTextField.SetValueWithoutNotify(path);
        }

        /// <summary>
        /// Updates the list of available Git folders and refreshes the UI to match.
        /// </summary>
        /// <param name="data">List of all Git folders and their associated data.</param>
        public void UpdateDirectories(List<FolderData> data)
        {
            gatheredFoldersList.itemsSource = data;
            gatheredFoldersList.Rebuild();
        }

        #endregion Methods
    }
}