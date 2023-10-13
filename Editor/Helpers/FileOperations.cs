using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

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

            FileUtil.CopyFileOrDirectory(source, target);

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
        public static void RenameFiles(DirectoryInfo directoryInfo, Dictionary<string, string> replacements, Action<FileInfo> processFile = null)
        {
            var files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                // If processFile is provided, use it for additional processing.
                processFile?.Invoke(file);

                var fileName = StringManipulation.MultipleReplace(file.Name, replacements);
                var newPath = Path.Combine(file.Directory.FullName, fileName);
                File.Move(file.FullName, newPath);
            }
        }

        /// <summary>
        /// Replaces all text within file based on a dictionary of replacement values.
        /// </summary>
        /// <param name="fullFileName">Full name of the file to be processed.</param>
        /// <param name="replacements">Replacement text pairings.</param>
        public static void ReplaceFileText(string fullFileName, Dictionary<string, string> replacements)
        {
            var text = File.ReadAllText(fullFileName);
            text = StringManipulation.RegexMultipleReplace(text, replacements);
            File.WriteAllText(fullFileName, text);
        }

        #endregion Methods
    }
}