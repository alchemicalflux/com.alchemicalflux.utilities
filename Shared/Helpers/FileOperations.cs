/*------------------------------------------------------------------------------
  File:           FileOperations.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Helper functions for common file operations.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-17 13:57:45 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.IO;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Stores helpful operations for file manipulation.
    /// </summary>
    public static class FileOperations
    {
        #region Methods

        /// <summary>
        /// Destroys all files under the target directory and copies the source files to it.
        /// </summary>
        /// <param name="source">Path for the directory to be copied.</param>
        /// <param name="target">Path to directory that will be cleared and filled with the source copy.</param>
        /// <returns>DirectoryInfo of the target directory.</returns>
        public static DirectoryInfo OverwriteDirectory(string source, string target)
        {
            var directoryInfo = new DirectoryInfo(target);
            if (directoryInfo.Exists)
            {
                directoryInfo.Delete(true);
            }

            CopyDirectory(new DirectoryInfo(source), new DirectoryInfo(target));

            return directoryInfo;
        }

        /// <summary>
        /// Removes unwanted folders from a directory based on the supplied folderConditions.
        /// </summary>
        /// <param name="directoryInfo">Directory to be modified.</param>
        /// <param name="foldersToDelete">List of names that will be that will be searched and removed.</param>
        public static void ProcessUnwantedFolders(DirectoryInfo directoryInfo, List<string> foldersToDelete)
        {
            foreach (var folderName in foldersToDelete)
            {
                var directories =
                    directoryInfo.GetDirectories(folderName, SearchOption.AllDirectories);

                foreach (var dir in directories)
                {
                    dir.Delete(true);
                }
            }
        }

        /// <summary>
        /// Removes all files from a directory that contain a given string value.
        /// </summary>
        /// <param name="directoryInfo">Directory to be searched.</param>
        /// <param name="fileString">String value that deleted files will contain.</param>
        public static void RemoveFilesContaining(DirectoryInfo directoryInfo, string fileString)
        {
            var files = directoryInfo.GetFiles(fileString, SearchOption.AllDirectories);

            foreach (var file in files)
            {
                file.Delete();
            }
        }

        /// <summary>
        /// Renames all files from a directory based on a dictionary of replacement values.
        /// Allows for processing of files before replacement occurs.
        /// </summary>
        /// <param name="directoryInfo">Directory to be searched.</param>
        /// <param name="replacements">String replacement pairings.</param>
        /// <param name="processFile">Potential actions that can process the file before replacements occur.</param>
        public static void RenameFiles(DirectoryInfo directoryInfo,
            IStringManipulator stringManipulator,
            Dictionary<string, string> replacements,
            Action<FileInfo> processFile = null)
        {
            var files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                // If processFile is provided, use it for additional processing.
                processFile?.Invoke(file);

                var fileName = stringManipulator.MultipleReplace(file.Name, replacements);
                var newPath = Path.Combine(file.Directory.FullName, fileName);
                File.Move(file.FullName, newPath);
            }
        }

        /// <summary>
        /// Replaces all text within file based on a dictionary of replacement values.
        /// </summary>
        /// <param name="fullFileName">Full name of the file to be processed.</param>
        /// <param name="replacements">Replacement text pairings.</param>
        public static void ReplaceFileText(string fullFileName, 
            IStringManipulator stringManipulator,
            Dictionary<string, string> replacements)
        {
            var text = File.ReadAllText(fullFileName);
            text = stringManipulator.MultipleReplace(text, replacements);
            File.WriteAllText(fullFileName, text);
        }

        /// <summary>
        /// Fully copies a source directory to the target directory.
        /// </summary>
        /// <param name="source">Source path to be copied.</param>
        /// <param name="target">Target path to be copied to.</param>
        public static void CopyDirectory(DirectoryInfo source, DirectoryInfo target)
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
        public static void CopyFiles(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (var file in source.GetFiles())
            {
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);
            }
        }

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

        #endregion Methods
    }
}