using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace AlchemicalFlux.Utilities.Helpers
{
    public static class FileOperations
    {
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

        public static void ProcessUnwantedFolders(DirectoryInfo directoryInfo, Dictionary<string, bool> folderConditions)
        {
            foreach (var folderCondition in folderConditions)
            {
                RemoveUnwantedFolders(directoryInfo, folderCondition.Key, !folderCondition.Value);
            }
        }

        public static void RemoveUnwantedFolders(DirectoryInfo directoryInfo, string folderName, bool deleteFolder)
        {
            if (deleteFolder)
            {
                var directories =
                    directoryInfo.GetDirectories("*" + folderName + "*", SearchOption.AllDirectories);

                foreach (var dir in directories)
                {
                    dir.Delete(true);
                }
            }
        }

        public static void RemoveFilesContaining(DirectoryInfo directoryInfo, string extenstion)
        {
            var files = directoryInfo.GetFiles(extenstion, SearchOption.AllDirectories);

            foreach (var file in files)
            {
                file.Delete();
            }
        }

        public static void RenameFiles(DirectoryInfo directoryInfo, Dictionary<string, string> replacements, Action<string> processFile = null)
        {
            var files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                // If processFile is provided, use it for additional processing
                processFile?.Invoke(file.FullName);

                var fileName = StringManipulation.MultipleReplace(file.Name, replacements);
                var newPath = Path.Combine(file.Directory.FullName, fileName);
                File.Move(file.FullName, newPath);
            }
        }

        public static void ReplaceFileText(string fullFileName, Dictionary<string, string> replacements)
        {
            var text = File.ReadAllText(fullFileName);
            text = StringManipulation.RegexMultipleReplace(text, replacements);
            File.WriteAllText(fullFileName, text);
        }
    }
}