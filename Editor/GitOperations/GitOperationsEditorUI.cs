/*------------------------------------------------------------------------------
  File:           GitOperationsEditorUI.cs 
  Project:        AlchemicalFlux Utilities
  Description:    UI encapsulation for handling Git Operations.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-09-04 14:46:24 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Helpers;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
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

        /// <summary>UI list view interface for accessing and modifying which folders will be processed.</summary>
        private ListView gatheredFoldersList;

        /// <summary>Callbacks triggered on the folder search button press.</summary>
        public Action OnSearchPressed;

        #endregion Members

        #region Properties

        /// <summary>
        /// Accessor for the current parent folder path.
        /// </summary>
        public string ParentFolderPath
        {
            get { return parentFolderTextField.text; }
            set { parentFolderTextField.SetValueWithoutNotify(value); }
        }

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
            rootVisualElement.Q(ref gatheredFoldersList, GitConstants.GatheredFoldersListName);

            // Bind the UI events with a callback handler.
            folderSearchButton.clicked += () => OnSearchPressed?.Invoke();

            // Handle the making, binding, and unbinding of the list view items.
            gatheredFoldersList.makeItem = () => listViewAsset.Instantiate();
            gatheredFoldersList.bindItem = BindItem;
            gatheredFoldersList.unbindItem = UnbindItem;
        }

        /// <summary>
        /// Handles the binding of data to UI.
        /// </summary>
        /// <param name="elem">Element that is being bound.</param>
        /// <param name="index">Index for accessing the data associated with the element.</param>
        private void BindItem(VisualElement elem, int index)
        {
            // Gather the associated UI references.
            var data = gatheredFoldersList.itemsSource[index] as FolderData;

            var folderPathLabel = elem.Q<Label>(GitConstants.FolderPathName);
            var preCommitToggle = elem.Q<Toggle>(GitConstants.PreCommitName);
            var workflowsToggle = elem.Q<Toggle>(GitConstants.GitHubWorkflowName);

            // Set the values based on the provided data.
            folderPathLabel.text = data.FolderPath;
            preCommitToggle.SetValueWithoutNotify(data.IncludePreCommits);
            workflowsToggle.SetValueWithoutNotify(data.IncludeWorkflows);

            preCommitToggle.bindingPath = nameof(FolderData.IncludePreCommits);
            preCommitToggle.Bind(new SerializedObject(data));

            workflowsToggle.bindingPath = nameof(FolderData.IncludeWorkflows);
            workflowsToggle.Bind(new SerializedObject(data));
        }

        /// <summary>
        /// Handles the unbinding of data to UI.
        /// </summary>
        /// <param name="elem">Element that is being unbound.</param>
        /// <param name="index">Index for accessing the data associated with the element.</param>
        private void UnbindItem(VisualElement elem, int index)
        {
            var preCommitToggle = elem.Q<Toggle>(GitConstants.PreCommitName);
            var workflowsToggle = elem.Q<Toggle>(GitConstants.GitHubWorkflowName);

            preCommitToggle.Unbind();
            workflowsToggle.Unbind();
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