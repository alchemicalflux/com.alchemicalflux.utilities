/*------------------------------------------------------------------------------
  File:           GeneratePackageEditorWindow.cs 
  Project:        YourProjectName  # You should replace this with your project name
  Description:    YourDescription  # You should replace this with your description
  Copyright:      ©2023 YourName/YourCompany. All rights reserved.  # You should replace this with your copyright details

  Last commit by: alchemicalflux 
  Last commit at: 2023-08-22 09:21:25 
------------------------------------------------------------------------------*/
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AlchemicalFlux.Utilities.PackageGeneration
{
    /// <summary>
    /// Editor window that handles the generation of a templatized package folder structure.
    /// </summary>
    public class GeneratePackageEditorWindow : EditorWindow
    {
        #region Members

        /// <summary>UI tree containing the appropriate interface elements.</summary>
        [SerializeField]
        private VisualTreeAsset tree;

        /// <summary>Handle to the editor logic is handled.</summary>
        private PackageEditor packageEditor = new();

        #endregion Members

        #region Methods

        /// <summary>
        /// Handles the creation of the editor window.
        /// </summary>
        [MenuItem("Tools/AlchemicalFlux Utilities/Generate Package")]
        public static void ShowEditorWindow()
        {
            var wnd = GetWindow<GeneratePackageEditorWindow>();
            wnd.titleContent = new GUIContent("Generate Package");
        }

        /// <summary>
        /// Handles the initialization of the UI and interface actions.
        /// </summary>
        public void CreateGUI()
        {
            tree.CloneTree(rootVisualElement);
            packageEditor.InitUIComponents(rootVisualElement);
            packageEditor.OnPackageCreation += Close;
        }

        #endregion Methods
    }
}