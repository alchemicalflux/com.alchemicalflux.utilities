/*------------------------------------------------------------------------------
  File:           GitConstants.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Centralized location for Git operation constants.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-09-04 14:46:24 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.GitOperations
{
    /// <summary>
    /// Stores useful values for Git operations.
    /// </summary>
    public static class GitConstants
    {
        // UI interface references
        public const string ParentFolderFieldName = "ParentFolderField";
        public const string FolderSearchButtonName = "ParentFolderSearchButton";
        public const string GatheredFoldersListName = "GatheredFoldersList";

        // GatheredFolders List View interface references
        public const string FolderPathName = "FolderPath";
        public const string PreCommitName = "PreCommit";
        public const string GitHubWorkflowName = "GitHubWorkflow";
    }
}