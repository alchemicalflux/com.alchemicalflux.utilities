using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AlchemicalFlux.Utilities.PackageGeneration
{
    /// <summary>
    /// Editor window that handles the generation of a templatized package folder structure.
    /// </summary>
    [CreateAssetMenu]
    public class GeneratePackageEditorWindow : EditorWindow
    {
        /// <summary>UI tree containing the appropriate interface elements.</summary>
        [SerializeField]
        private VisualTreeAsset tree;

        /// <summary>Handle to the editor logic is handled.</summary>
        private PackageEditor packageEditor = new();

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