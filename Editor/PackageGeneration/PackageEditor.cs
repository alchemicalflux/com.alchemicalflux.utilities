using AlchemicalFlux.Utilities.Helpers;
using System;
using UnityEditor;
using UnityEngine.UIElements;

namespace AlchemicalFlux.Utilities.PackageGeneration
{
    /// <summary>
    /// Handles the logic of binding of UI elements and file manipulation for package generation.
    /// </summary>
    public class PackageEditor
    {
        #region Members

        /// <summary></summary>
        private const string metaFileExtension = "*.meta";

        /// <summary>UI functionality that will be bound to file logic.</summary>
        private PackageEditorUI ui;

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
            ui = new PackageEditorUI(rootVisualElement);
            ui.OnSavePressed += GeneratePackage;
            ui.OnSavePressed += () =>
            {
                OnPackageCreation?.Invoke();
            };
        }

        /// <summary>
        /// Handles the creation of a new package layout based on the template project.
        /// </summary>
        private void GeneratePackage()
        {
            // Copy the template to a temp location.
            var tempPath = PackageConstants.TempPath + ui.PackageName;
            var directoryInfo = FileOperations.OverwriteDirectory(PackageConstants.PackagePath, tempPath);

            // Remove unwanted files and update file names and contents.
            FileOperations.ProcessUnwantedFolders(directoryInfo, ui.FoldersToRemove);
            FileOperations.RemoveFilesContaining(directoryInfo, metaFileExtension);
            FileOperations.RenameFiles(directoryInfo, ui.TemplateNamespaces,
                filePath => FileOperations.ReplaceFileText(filePath.FullName, ui.FileTextPlacements));

            // Move new package to the project Assests folder and refresh the interface.
            FileOperations.OverwriteDirectory(tempPath, PackageConstants.AssetsPath + ui.PackageName);
            AssetDatabase.Refresh();
        }

        #endregion Methods
    }
}