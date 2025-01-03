/*------------------------------------------------------------------------------
  File:           GitOperationsEditor.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Logic for handling Git Operations.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-11-04 11:20:10 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace AlchemicalFlux.Utilities.GitOperations
{
    /// <summary>
    /// Handles the logic of binding of UI elements and file manipulation for Got operations.
    /// </summary>
    public class GitOperationsEditor
    {
        #region Members

        /// <summary>File types to be removed from copy.</summary>
        private const string metaFileExtension = "*.meta";

        /// <summary>Name of top level folders associated with Git.</summary>
        private const string gitFolderName = ".git";

        /// <summary>UI functionality that will be bound to file logic.</summary>
        private GitOperationsEditorUI ui;

        public Func<string> GetDirectory;

        private List<FolderData> directoryList = new List<FolderData>();

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
            ui.OnInstallPressed += InstallSelections;
        }

        /// <summary>
        /// Opens a folder search and assigns the selected folder to the parent folder path.
        /// </summary>
        public void SelectParentFolder()
        {
            ui.ParentFolderPath = GetDirectory?.Invoke();
            GatherFolders();
        }

        /// <summary>
        /// Gathers a list of all ".git" folders that exist under the parent folder.
        /// </summary>
        private void GatherFolders()
        {
            // Gather all of the child Git folders.
            var parentPathInfo = new DirectoryInfo(ui.ParentFolderPath);
            var gitDirectories =
                parentPathInfo.GetDirectories(gitFolderName, SearchOption.AllDirectories);

            // Create a list of folder data that will be bound to the UI elements of the list.
            directoryList = new List<FolderData>();
            foreach (var directory in gitDirectories)
            {
                // Trim the folder names to reduce redundancy.
                var data = ScriptableObject.CreateInstance<FolderData>();
                data.FolderPath = directory.Parent.FullName.Replace(parentPathInfo.FullName, "");
                directoryList.Add(data);
            }

            ui.UpdateDirectories(directoryList);
        }

        /// <summary>
        /// Handles the installation of all the various git operations.
        /// </summary>
        private void InstallSelections()
        {
            InstallPreCommits();
            InstallSemanticRelease();
        }

        /// <summary>
        /// Handles the processing of all folders installing the pre-commit files.
        /// </summary>
        private void InstallPreCommits()
        {
            var preCommitFolders = directoryList.Where(data => data.IncludePreCommits)
                .Select(data => data.FolderPath);

            if(preCommitFolders.Any())
            {
                var fileOperations = new IOFileSystemService(new RegexStringManipulator());

                // Copy the template to a temp location.
                var tempPath = Path.Join(GitConstants.TempPath, GitConstants.PreCommitName);
                fileOperations.CopyDirectory(GitConstants.PreCommitPath, tempPath);

                // Remove unwanted files.
                fileOperations.RemoveFilesByName(tempPath, metaFileExtension);

                foreach (var folder in preCommitFolders)
                {
                    var targetPath = Path.Join(ui.ParentFolderPath, folder); 
                    targetPath = Path.Join(targetPath, gitFolderName, GitConstants.PreCommitTargetPath);
                    fileOperations.CopyDirectory(tempPath, targetPath);
                }

                // Remove the temporary files.
                fileOperations.DeleteDirectory(tempPath);
            }
        }

        /// <summary>
        /// Handles the processing of all folders installing the semantic release files.
        /// </summary>
        private void InstallSemanticRelease()
        {
            var semanticReleaseFolders = directoryList.Where(data => data.IncludeSemanticRelease)
                .Select(data => data.FolderPath);

            if (semanticReleaseFolders.Any())
            {
                var fileOperations = new IOFileSystemService(new RegexStringManipulator());

                // Copy the template to a temp location.
                var tempPath = Path.Join(GitConstants.TempPath, GitConstants.SemanticReleaseName);
                fileOperations.CopyDirectory(GitConstants.SemanticReleasePath, tempPath);

                // Remove unwanted files.
                fileOperations.RemoveFilesByName(tempPath, metaFileExtension);

                foreach (var folder in semanticReleaseFolders)
                {
                    var targetPath = Path.Join(ui.ParentFolderPath, folder);
                    fileOperations.CopyDirectory(tempPath, targetPath);
                }

                // Remove the temporary files.
                fileOperations.DeleteDirectory(tempPath);
            }
        }

        #endregion Methods
    }
}