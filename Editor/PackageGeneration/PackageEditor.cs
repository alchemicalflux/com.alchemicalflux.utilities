using AlchemicalFlux.Utilities.Helpers;
using UnityEngine.UIElements;

namespace AlchemicalFlux.Utilities.PackageGeneration
{
    public class PackageEditor
    {
        private PackageEditorUI ui;

        public void InitUIComponents(VisualElement visualElement)
        {
            ui = new PackageEditorUI(visualElement);
            ui.AddOnSaveAction(SaveButtonPressed);
        }

        public void AddOnSaveAction(System.Action action)
        {
            ui.AddOnSaveAction(action);
        }

        public void RemoveOnSaveAction(System.Action action)
        {
            ui.RemoveOnSaveAction(action);
        }

        private void SaveButtonPressed()
        {
            var tempPath = PackageConstants.TempPath + ui.PackageName;
            var directoryInfo = FileOperations.OverwriteDirectory(PackageConstants.PackagePath, tempPath);

            FileOperations.ProcessUnwantedFolders(directoryInfo, ui.FolderConditions);
            FileOperations.RemoveFilesContaining(directoryInfo, "*.meta");
            FileOperations.RenameFiles(directoryInfo, ui.TemplateNamespaces,
                filePath => FileOperations.ReplaceFileText(filePath, ui.FileTextPlacements));

            FileOperations.OverwriteDirectory(tempPath, PackageConstants.AssetsPath + ui.PackageName);
        }
    }
}