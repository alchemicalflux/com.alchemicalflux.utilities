/*------------------------------------------------------------------------------
  File:           IOFileSystemService.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Contains System.IO implementation of IFileOperations.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-20 07:31:05 
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
        private IStringManipulator _stringManipulator;

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
            if(stringManipulator == null)
            {
                throw new ArgumentNullException(nameof(stringManipulator));
            }

            _stringManipulator = stringManipulator;
        }

        #endregion Constructors

        #region Interface Methods

        /// <inheritdoc />
        public void OverwriteDirectory(string sourcePath, string targetPath)
        {
            var directoryInfo = new DirectoryInfo(targetPath);
            if (directoryInfo.Exists)
            {
                directoryInfo.Delete(true);
            }

            CopyDirectory(new DirectoryInfo(sourcePath), directoryInfo);
        }

        /// <inheritdoc />
        public void RemoveFoldersByName(string sourcePath, List<string> filters)
        {
            var directoryInfo = new DirectoryInfo(sourcePath);
            foreach (var filter in filters)
            {
                var directories =
                    directoryInfo.GetDirectories(filter, SearchOption.AllDirectories);

                foreach (var directory in directories)
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

            foreach (var file in files)
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
            foreach (var file in files)
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
            // Create top folder and initialize snapshot stack for child processing.
            var snapshotStack = new Stack<CopySnapshot>();
            var currentSnapshot = new CopySnapshot()
            {
                Source = source,
                Target = target,
                CurrentIndex = 0
            };
            snapshotStack.Push(currentSnapshot);
            Directory.CreateDirectory(currentSnapshot.Target.FullName);

            // Loop to process all the child directories and their children.
            do
            {
                // Determine if there are any directories left for current snapshot.
                var directories = currentSnapshot.Source.GetDirectories();
                if (directories.Length > 0 && currentSnapshot.CurrentIndex < directories.Length)
                {
                    // Save snapshot with index of the new directory.
                    var currentDirectory = directories[currentSnapshot.CurrentIndex];
                    ++currentSnapshot.CurrentIndex;
                    snapshotStack.Push(currentSnapshot);

                    // Create new snapshot and folder for new directory processing.
                    var newSource = new DirectoryInfo(currentDirectory.FullName);
                    var newTargetPath = Path.Combine(currentSnapshot.Target.FullName, newSource.Name);
                    var newTarget = new DirectoryInfo(newTargetPath);
                    currentSnapshot = new CopySnapshot()
                    {
                        Source = newSource,
                        Target = newTarget,
                        CurrentIndex = 0,
                    };
                    Directory.CreateDirectory(currentSnapshot.Target.FullName);
                }
                else
                {
                    // Copy current directory files and switch reference to parent folder snapshot.
                    CopyFiles(currentSnapshot.Source, currentSnapshot.Target);
                    currentSnapshot = snapshotStack.Pop();
                }
            } while (snapshotStack.Count > 0);
        }

        /// <summary>
        /// Copies the files from one directory to another.
        /// </summary>
        /// <param name="source">Source path to be copied.</param>
        /// <param name="target">Target path to be copied to.</param>
        private void CopyFiles(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (var file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }
        }

        #endregion Internal Methods

        #endregion Members

        #region Internal Classes

        /// <summary>
        /// Helper class that stores related data for the deep copying of directories.
        /// </summary>
        private struct CopySnapshot
        {
            /// <summary>Source information to be copied.</summary>
            public DirectoryInfo Source;

            /// <summary>Target information for copy.</summary>
            public DirectoryInfo Target;

            /// <summary>Current index of the source directory being copied.</summary>
            public uint CurrentIndex;
        }

        #endregion Internal Classes
    }
}
