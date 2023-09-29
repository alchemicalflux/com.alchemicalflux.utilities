/*------------------------------------------------------------------------------
  File:           GitOperationsEditor.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Logic for handling Git Operations.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-09-28 20:08:06 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace AlchemicalFlux.Utilities.GitOperations
{
    /// <summary>
    /// Handles the logic of binding of UI elements and file manipulation for Got operations.
    /// </summary>
    public class GitOperationsEditor
    {
        #region Members

        /// <summary>Name of top level folders associated with Git.</summary>
        private const string gitFolderName = ".git";

        /// <summary>UI functionality that will be bound to file logic.</summary>
        private GitOperationsEditorUI ui;

        public Func<string> GetDirectory;

        #endregion Members

        #region Methods

        /// <summary>
        /// Initializes the UI and binds file manipulation logic.
        /// </summary>
        /// <param name="rootVisualElement"></param>
        public void BindUIComponents(GitOperationsEditorUI uiHandle)
        {
            ui = uiHandle;
            ui.ParentFolderPath = Directory.GetCurrentDirectory();
            GatherFolders();

            ui.OnSearchPressed += SelectParentFolder;
        }

        /// <summary>
        /// Opens a folder search and assigns the selected folder to the parent folder path.
        /// </summary>
        public void SelectParentFolder()
        {
            var directory = GetDirectory.Invoke();
            ui.ParentFolderPath = directory;
            GatherFolders();
        }

        /// <summary>
        /// Gathers a list of all ".git" folders that exist under the parent folder.
        /// </summary>
        public void GatherFolders()
        {
            // Gather all of the child Git folders.
            var parentPathInfo = new DirectoryInfo(ui.ParentFolderPath);
            var gitDirectories =
                parentPathInfo.GetDirectories(gitFolderName, SearchOption.AllDirectories);

            // Create a list of folder data that will be bound to the UI elements of the list.
            var directoryList = new List<FolderData>();
            foreach (var directory in gitDirectories)
            {
                // Trim the folder names to reduce redundancy.
                var data = ScriptableObject.CreateInstance<FolderData>();
                data.FolderPath = directory.FullName.Replace(parentPathInfo.FullName, "");
                directoryList.Add(data);
            }

            ui.UpdateDirectories(directoryList);
        }

        #endregion Methods
    }
}