/*------------------------------------------------------------------------------
  File:           GeneratePackageEditor.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Logic for handling Unity package generation.
  Copyright:      2023-2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-11-30 22:23:47 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Helpers;
using System;
using UnityEngine.UIElements;

namespace AlchemicalFlux.Utilities.PackageGeneration
{
    /// <summary>
    /// Handles the logic of binding of UI elements and file manipulation for package generation.
    /// </summary>
    public class GeneratePackageEditor
    {
        #region Members

        /// <summary></summary>
        private const string _metaFileExtension = "*.meta";

        /// <summary>UI functionality that will be bound to file logic.</summary>
        private PackageEditorUI _ui;

        /// <summary>Callback handle to trigger on package creation.</summary>
        public Action OnPackageCreation;

        #endregion Members

        #region Methods

        /// <summary>
        /// Initializes the UI and binds file manipulation logic.
        /// </summary>
        /// <param name="rootVisualElement"></param>
        public void InitUIComponents(VisualElement rootVisualElement)
        {
            _ui = new PackageEditorUI(rootVisualElement);
            _ui.OnSavePressed += GeneratePackage;
        }

        /// <summary>
        /// Handles the creation of a new package layout based on the template project.
        /// </summary>
        private void GeneratePackage()
        {
            var fileOperations = new IOFileSystemService(new RegexStringManipulator());

            // Copy the template to a temp location.
            var tempPath = PackageConstants.TempPath + _ui.PackageName;
            fileOperations.OverwriteDirectory(PackageConstants.PackagePath, tempPath);

            // Remove unwanted files and update file names and contents.
            fileOperations.RemoveFoldersByName(tempPath, _ui.FoldersToRemove);
            fileOperations.RemoveFilesByName(tempPath, _metaFileExtension);

            var regexStringManipulator = new RegexStringManipulator();
            fileOperations.RenameFiles(tempPath, _ui.TemplateNamespaces,
                filePath => fileOperations.ReplaceFileText(filePath, _ui.FileTextPlacements));

            // Move new package to the project Assests folder and refresh the interface.
            fileOperations.OverwriteDirectory(tempPath, PackageConstants.AssetsPath + _ui.PackageName);

            // Remove the temporary files.
            fileOperations.DeleteDirectory(tempPath);

            OnPackageCreation?.Invoke();
        }

        #endregion Methods
    }
}