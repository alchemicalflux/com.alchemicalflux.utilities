/*------------------------------------------------------------------------------
  File:           GitConstants.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Centralized location for Git operation constants.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-11-01 16:01:14 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.GitOperations
{
    /// <summary>
    /// Stores useful values for Git operations.
    /// </summary>
    public static class GitConstants
    {
        // Directory information
        public const string TempPath = "Temp/";
        public const string PackagePath = "Packages/com.alchemicalflux.git-utilities/";
        public const string DocumentsPath = PackagePath + "Documents/";
        public const string PreCommitPath = DocumentsPath + "PreCommit/";
        public const string PreCommitTargetPath = "hooks";
        public const string SemanticReleasePath = DocumentsPath + "SemanticRelease/";

        // UI interface references
        public const string ParentFolderFieldName = "ParentFolderField";
        public const string FolderSearchButtonName = "ParentFolderSearchButton";
        public const string GatheredFoldersListName = "GatheredFoldersList";
        public const string InstallButtonName = "InstallButton";

        // GatheredFolders List View interface references
        public const string FolderPathName = "FolderPath";
        public const string PreCommitName = "PreCommit";
        public const string SemanticReleaseName = "SemanticRelease";
    }
}