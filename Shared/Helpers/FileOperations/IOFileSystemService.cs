/*------------------------------------------------------------------------------
  File:           IOFileSystemService.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Contains System.IO implementation of IFileOperations.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-11-29 21:18:21 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// File opertaions that use System.IO functionality.
    /// </summary>
    public class IOFileSystemService : IFileSystemService
    {
        #region Members

        /// <summary>
        /// Handle to manipulator used to process string replacements.
        /// </summary>
        private readonly IStringManipulator _stringManipulator;

        #endregion

        #region Members

        #region Constructors

        /// <summary>
        /// Prevent empty constructors from being called.
        /// </summary>
        private IOFileSystemService() { }

        /// <summary>
        /// Initializes class.
        /// </summary>
        /// <param name="stringManipulator">Handles all string replacements.</param>
        public IOFileSystemService(IStringManipulator stringManipulator)
        {
            _stringManipulator = stringManipulator ?? throw new ArgumentNullException(nameof(stringManipulator));
        }

        #endregion Constructors

        #region Interface Methods

        /// <inheritdoc />
        public void CopyDirectory(string sourcePath, string targetPath)
        {
            var directoryInfo = new DirectoryInfo(targetPath);
            CopyDirectory(new DirectoryInfo(sourcePath), directoryInfo);
        }

        /// <inheritdoc />
        public void OverwriteDirectory(string sourcePath, string targetPath)
        {
            DeleteDirectory(targetPath);
            CopyDirectory(sourcePath, targetPath);
        }

        /// <inheritdoc />
        public void DeleteDirectory(string targetPath)
        {
            var directoryInfo = new DirectoryInfo(targetPath);
            if(directoryInfo.Exists)
            {
                directoryInfo.Delete(true);
            }
        }

        /// <inheritdoc />
        public void RemoveFoldersByName(string sourcePath, List<string> filters)
        {
            var directoryInfo = new DirectoryInfo(sourcePath);
            foreach(var filter in filters)
            {
                var directories =
                    directoryInfo.GetDirectories(filter, SearchOption.AllDirectories);

                foreach(var directory in directories)
                {
                    directory.Delete(true);
                }
            }
        }

        /// <inheritdoc />
        public void RemoveFilesByName(string sourcePath, string filter)
        {
            var directoryInfo = new DirectoryInfo(sourcePath);
            var files = directoryInfo.GetFiles(filter, SearchOption.AllDirectories);

            foreach(var file in files)
            {
                file.Delete();
            }
        }

        /// <inheritdoc />
        public void RenameFiles(string sourcePath,
            Dictionary<string, string> replacements,
            Action<string> processFile = null)
        {
            var directoryInfo = new DirectoryInfo(sourcePath);
            var files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
            foreach(var file in files)
            {
                // If processFile is provided, use it for additional processing.
                processFile?.Invoke(file.FullName);

                var fileName = _stringManipulator.MultipleReplace(file.Name, replacements);
                var newPath = Path.Combine(file.Directory.FullName, fileName);
                File.Move(file.FullName, newPath);
            }
        }

        /// <inheritdoc />
        public void ReplaceFileText(string filePath,
            Dictionary<string, string> replacements)
        {
            var text = File.ReadAllText(filePath);
            text = _stringManipulator.MultipleReplace(text, replacements);
            File.WriteAllText(filePath, text);
        }

        #endregion Interface Methods

        #region Internal Methods

        /// <summary>
        /// Fully copies a source directory to the target directory.
        /// </summary>
        /// <param name="source">Source path to be copied.</param>
        /// <param name="target">Target path to be copied to.</param>
        private void CopyDirectory(DirectoryInfo source, DirectoryInfo target)
        {
            // Create and copy the top level folders and files.
            CreateFolderAndCopyFiles(source, target.FullName);

            var directories = source.GetDirectories("*", SearchOption.AllDirectories);
            foreach(var directory in directories)
            {
                // Calculate the relative path between source and current directory.
                string relativePath = Path.GetRelativePath(source.FullName, directory.FullName);

                // Create the target directory path by combining target path and relative path.
                string targetDirPath = Path.Combine(target.FullName, relativePath);

                // Create the target directory if it doesn't exist.
                CreateFolderAndCopyFiles(directory, targetDirPath);
            }
        }

        /// <summary>
        /// Creates a top level folder and copies the files the files in it.
        /// </summary>
        /// <param name="source">Source path to be copied.</param>
        /// <param name="target">Target path to be copied to.</param>
        private void CreateFolderAndCopyFiles(DirectoryInfo source, string targetPath)
        {
            Directory.CreateDirectory(targetPath);
            foreach(var file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(targetPath, file.Name), true);
            }
        }

        #endregion Internal Methods

        #endregion Members
    }
}
