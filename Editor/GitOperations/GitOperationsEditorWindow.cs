/*------------------------------------------------------------------------------
  File:           GitOperationsEditorWindow.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Editor Window construction for handling Git Operations.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-08-22 09:50:51 
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

        /// <summary>Handle to the editor logic is handled.</summary>
        private GitOperationsEditor gitOperationsEditor = new();

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
            gitOperationsEditor.InitUIComponents(rootVisualElement);
        }

        #endregion Methods
    }
}