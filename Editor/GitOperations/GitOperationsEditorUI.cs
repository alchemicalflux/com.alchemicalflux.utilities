/*------------------------------------------------------------------------------
  File:           GitOperationsEditorUI.cs 
  Project:        AlchemicalFlux Utilities
  Description:    UI encapsulation for handling Git Operations.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-08-31 07:52:52 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Helpers;
using System;
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
        /// <summary>UI button that triggers the gathering of all git folder paths.</summary>
        private Button gatherFoldersButton;

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
        public GitOperationsEditorUI(VisualElement rootVisualElement)
        {
            rootVisualElement.Q(ref parentFolderTextField, GitConstants.ParentFolderFieldName);
            rootVisualElement.Q(ref folderSearchButton, GitConstants.FolderSearchButtonName);
            rootVisualElement.Q(ref gatherFoldersButton, GitConstants.GatherFoldersButtonName);

            folderSearchButton.clicked += () =>
            {
                OnSearchPressed?.Invoke();
            };

            gatherFoldersButton.clicked += () =>
            {
                OnGatherPressed?.Invoke();
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

        #endregion Methods
    }
}