/*------------------------------------------------------------------------------
  File:           GitOperationsEditor.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Logic for handling Git Operations.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-08-31 07:52:52 
------------------------------------------------------------------------------*/
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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

        #endregion Members

        #region Methods

        /// <summary>
        /// Initializes the UI and binds file manipulation logic.
        /// </summary>
        /// <param name="rootVisualElement"></param>
        public void InitUIComponents(VisualElement rootVisualElement)
        {
            ui = new GitOperationsEditorUI(rootVisualElement);

            ui.SetParentFolder(Directory.GetCurrentDirectory());

            ui.OnSearchPressed += SelectParentFolder;
            ui.OnGatherPressed += GatherFolders;
        }

        /// <summary>
        /// Opens a folder search and assigns the selected folder to the parent folder path.
        /// </summary>
        public void SelectParentFolder()
        {
            string directory = EditorUtility.OpenFolderPanel("Select Directory", "", "");
            ui.SetParentFolder(directory);
        }

        /// <summary>
        /// Gathers a list of all ".git" folders that exist under the parent folder.
        /// </summary>
        public void GatherFolders()
        {
            var directoryInfo = new DirectoryInfo(ui.ParentFolderPath);

            var directories =
                directoryInfo.GetDirectories(gitFolderName, SearchOption.AllDirectories);

            foreach(var dic in directories)
            {
                Debug.Log(dic.FullName);
            }
        }

        #endregion Methods
    }
}