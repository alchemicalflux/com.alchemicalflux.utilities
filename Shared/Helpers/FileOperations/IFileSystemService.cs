/*------------------------------------------------------------------------------
  File:           IFileSystemService.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Interface for useful file operation functions.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-20 07:31:05 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Defines a contract for file system service operations, providing common file manipulation functionalities.
    /// </summary>
    public interface IFileSystemService
    {
        /// <summary>
        /// Destroys all files under the target directory and copies the source files to it.
        /// </summary>
        /// <param name="sourcePath">Directory to be copied.</param>
        /// <param name="targetPath">Directory that will be cleared and filled with the source copy.</param>
        public void OverwriteDirectory(string sourcePath, string targetPath);

        /// <summary>
        /// Removes unwanted folders from a directory based on the supplied filters.
        /// </summary>
        /// <param name="sourcePath">Directory to be modified.</param>
        /// <param name="filters">Filters used to query and remove folders.</param>
        public void RemoveFoldersByName(string sourcePath, List<string> filters);

        /// <summary>
        /// Removes all files from a directory that contain a given string value.
        /// </summary>
        /// <param name="sourcePath">Directory to be searched.</param>
        /// <param name="filter">Filter used to query and remove files.</param>
        public void RemoveFilesByName(string sourcePath, string filter);

        /// <summary>
        /// Renames all files from a directory based on a dictionary of replacement values.
        /// Allows for processing of files before replacement occurs.
        /// </summary>
        /// <param name="sourcePath">Directory to be searched.</param>
        /// <param name="replacements">String replacement pairings.</param>
        /// <param name="processFile">
        /// Actions that can process the file before renaming occurs.
        /// Recieves the full path of the file to be acted upon.
        /// </param>
        public void RenameFiles(string sourcePath,
            Dictionary<string, string> replacements,
            Action<string> processFile = null);

        /// <summary>
        /// Replaces all text within file based on a dictionary of replacement values.
        /// </summary>
        /// <param name="filePath">Full path of the file to be processed.</param>
        /// <param name="replacements">Replacement text pairings.</param>
        public void ReplaceFileText(string filePath, Dictionary<string, string> replacements);
    }
}
