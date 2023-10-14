/*------------------------------------------------------------------------------
  File:           GitOperationsEditorWindow.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Editor Window construction for handling Git Operations.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-13 19:00:09 
------------------------------------------------------------------------------*/
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AlchemicalFlux.Utilities.GitOperations
{
    /// <summary>
    /// Editor window that handles various Git related functionality and file generation.
    /// </summary>
    public class GitOperationsEditorWindow : EditorWindow
    {
        #region Members

        /// <summary>UI tree containing the appropriate interface elements.</summary>
        [SerializeField]
        private VisualTreeAsset tree;

        /// <summary>UI tree containing the template for the gathered folder list view.</summary>
        [SerializeField]
        private VisualTreeAsset listViewAsset;

        /// <summary>Handle to the editor logic is handled.</summary>
        private GitOperationsEditor gitOperationsEditor = new();

        private GitOperationsEditorUI gitOperationsEditorUI;

        #endregion Members

        #region Methods

        /// <summary>
        /// Handles the creation of the editor window.
        /// </summary>
        [MenuItem("Tools/AlchemicalFlux Utilities/Git Operations")]
        public static void ShowExample()
        {
            GitOperationsEditorWindow wnd = GetWindow<GitOperationsEditorWindow>();
            wnd.titleContent = new GUIContent("Git Operations");
        }

        /// <summary>
        /// Handles the initialization of the UI and interface actions.
        /// </summary>
        public void CreateGUI()
        {
            tree.CloneTree(rootVisualElement);

            gitOperationsEditorUI = new GitOperationsEditorUI(rootVisualElement, listViewAsset);
            gitOperationsEditor.GetDirectory = GetDirectory;
            gitOperationsEditor.BindUIComponents(gitOperationsEditorUI);
        }

        /// <summary>
        /// Gathers the directory to be processed for git operations.
        /// </summary>
        /// <returns>Path of the selected folder.</returns>
        private string GetDirectory()
        {
            return EditorUtility.OpenFolderPanel("Select Directory", "", "");
        }

    #endregion Methods
}
}