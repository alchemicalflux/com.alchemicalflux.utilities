/*------------------------------------------------------------------------------
  File:           GeneratePackageEditorWindow.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Editor Window construction for handling Unity package generation.
  Copyright:      ©2023-2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-11-29 20:48:48 
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
        private VisualTreeAsset _tree;

        /// <summary>Handle to the editor logic is handled.</summary>
        private readonly GeneratePackageEditor _packageEditor = new();

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
            _tree.CloneTree(rootVisualElement);
            _packageEditor.InitUIComponents(rootVisualElement);
            _packageEditor.OnPackageCreation += Close;
            _packageEditor.OnPackageCreation += PostCreationProcessing;
        }

        /// <summary>
        /// Handles the uninitialization of the UI and interface actions.
        /// </summary>
        public void OnDestroy()
        {
            if(_packageEditor != null)
            {
                _packageEditor.OnPackageCreation -= Close;
                _packageEditor.OnPackageCreation -= PostCreationProcessing;
            }
        }

        /// <summary>
        /// Handles any remaining actions that are required after package creation.
        /// </summary>
        private void PostCreationProcessing()
        {
            AssetDatabase.Refresh();
        }

        #endregion Methods
    }
}