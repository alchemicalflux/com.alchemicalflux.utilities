/*------------------------------------------------------------------------------
  File:           GitOperationsEditorUI.cs 
  Project:        AlchemicalFlux Utilities
  Description:    UI encapsulation for handling Git Operations.
  Copyright:      ©2023-2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-11-29 20:48:48 
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
        private readonly TextField _parentFolderTextField;

        /// <summary>UI button that triggers the parent folder path search functionality.</summary>
        private readonly Button _folderSearchButton;

        /// <summary>UI list view interface for accessing and modifying which folders will be processed.</summary>
        private readonly ListView _gatheredFoldersList;

        private readonly VisualTreeAsset _listViewTemplate;

        /// <summary></summary>
        private readonly Button _installButton;

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
            get { return _parentFolderTextField.text; }
            set { _parentFolderTextField.SetValueWithoutNotify(value); }
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
            rootVisualElement.Q(ref _parentFolderTextField, GitConstants.ParentFolderFieldName);
            rootVisualElement.Q(ref _folderSearchButton, GitConstants.FolderSearchButtonName);
            rootVisualElement.Q(ref _gatheredFoldersList, GitConstants.GatheredFoldersListName);
            rootVisualElement.Q(ref _installButton, GitConstants.InstallButtonName);

            // Bind the UI events with a callback handler.
            _folderSearchButton.clicked += () => OnSearchPressed?.Invoke();
            _installButton.clicked += () => OnInstallPressed?.Invoke();

            // Handle the making, binding, and unbinding of the list view items.
            _listViewTemplate = listViewAsset;
            _gatheredFoldersList.makeItem = MakeItem;
            _gatheredFoldersList.bindItem = BindItem;
            _gatheredFoldersList.unbindItem = UnbindItem;
        }

        /// <summary>
        /// Handles the creation and initialization of a reusable list entry template.
        /// </summary>
        /// <returns>Handle to the created list entry.</returns>
        private VisualElement MakeItem()
        {
            // Create and gather the UI entry and its children.
            var newListEntry = _listViewTemplate.Instantiate();

            // Initialize the controller that stores related handles and information.
            newListEntry.userData = new FolderDataController()
            {
                FolderPathLabel = newListEntry.Q<Label>(GitConstants.FolderPathName),
                IncludePreCommitsToggle = newListEntry.Q<Toggle>(GitConstants.PreCommitName),
                IncludeSemanticReleaseToggle = newListEntry.Q<Toggle>(GitConstants.SemanticReleaseName),
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
            var data = _gatheredFoldersList.itemsSource[index] as FolderData;

            var controller = (elem.userData) as FolderDataController;

            // Set the values based on the provided data.
            controller.Data = data;
            controller.FolderPathLabel.text = data.FolderPath;
            controller.IncludePreCommitsToggle.SetValueWithoutNotify(data.IncludePreCommits);
            controller.IncludeSemanticReleaseToggle.SetValueWithoutNotify(data.IncludeSemanticRelease);

            // Register the callbacks for UI functionality.
            controller.IncludePreCommitsToggle.RegisterValueChangedCallback(controller.OnPreCommitChange);
            controller.IncludeSemanticReleaseToggle.RegisterValueChangedCallback(controller.OnSemanticReleaseChange);
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
            controller.IncludeSemanticReleaseToggle.UnregisterValueChangedCallback(controller.OnSemanticReleaseChange);

            controller.Data = null;
        }

        /// <summary>
        /// Updates the list of available Git folders and refreshes the UI to match.
        /// </summary>
        /// <param name="data">List of all Git folders and their associated data.</param>
        public void UpdateDirectories(List<FolderData> data)
        {
            _gatheredFoldersList.itemsSource = data;
            _gatheredFoldersList.Rebuild();
        }

        #endregion Methods
    }
}