/*------------------------------------------------------------------------------
  File:           GitOperationsEditorUI.cs 
  Project:        AlchemicalFlux Utilities
  Description:    UI encapsulation for handling Git Operations.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-13 01:50:32 
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

        /// <summary>UI list view interface for accessing and modifying which folders will be processed.</summary>
        private ListView gatheredFoldersList;

        private VisualTreeAsset listViewTemplate;

        /// <summary></summary>
        private Button installButton;

        /// <summary>Callbacks triggered on the folder search button press.</summary>
        public Action OnSearchPressed;

        /// <summary></summary>
        public Action OnInstallPressed;

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
            rootVisualElement.Q(ref installButton, GitConstants.InstallButtonName);

            // Bind the UI events with a callback handler.
            folderSearchButton.clicked += () => OnSearchPressed?.Invoke();
            installButton.clicked += () => OnInstallPressed?.Invoke();

            // Handle the making, binding, and unbinding of the list view items.
            listViewTemplate = listViewAsset;
            gatheredFoldersList.makeItem = MakeItem;
            gatheredFoldersList.bindItem = BindItem;
            gatheredFoldersList.unbindItem = UnbindItem;
        }

        /// <summary>
        /// Handles the creation and initialization of a reusable list entry template.
        /// </summary>
        /// <returns>Handle to the created list entry.</returns>
        private VisualElement MakeItem()
        {
            // Create and gather the UI entry and its children.
            var newListEntry = listViewTemplate.Instantiate();

            var folderPathLabel = newListEntry.Q<Label>(GitConstants.FolderPathName);
            var preCommitToggle = newListEntry.Q<Toggle>(GitConstants.PreCommitName);
            var workflowsToggle = newListEntry.Q<Toggle>(GitConstants.GitHubWorkflowName);

            // Initialize the controller that stores related handles and information.
            newListEntry.userData = new FolderDataController()
            {
                FolderPathLabel = folderPathLabel,
                IncludePreCommitsToggle = preCommitToggle,
                IncludeWorkflowsToggle = workflowsToggle,
            };

            return newListEntry;
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

            var controller = (elem.userData) as FolderDataController;

            // Set the values based on the provided data.
            controller.Data = data;
            controller.FolderPathLabel.text = data.FolderPath;
            controller.IncludePreCommitsToggle.SetValueWithoutNotify(data.IncludePreCommits);
            controller.IncludeWorkflowsToggle.SetValueWithoutNotify(data.IncludeWorkflows);

            // Register the callbacks for UI functionality.
            controller.IncludePreCommitsToggle.RegisterValueChangedCallback(controller.OnPreCommitChange);
            controller.IncludeWorkflowsToggle.RegisterValueChangedCallback(controller.OnWorkflowChange);
        }

        /// <summary>
        /// Handles the unbinding of data to UI.
        /// </summary>
        /// <param name="elem">Element that is being unbound.</param>
        /// <param name="index">Index for accessing the data associated with the element.</param>
        private void UnbindItem(VisualElement elem, int index)
        {
            // Unegister the callbacks for UI functionality.
            var controller = (elem.userData) as FolderDataController;

            controller.IncludePreCommitsToggle.UnregisterValueChangedCallback(controller.OnPreCommitChange);
            controller.IncludeWorkflowsToggle.UnregisterValueChangedCallback(controller.OnWorkflowChange);

            controller.Data = null;
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