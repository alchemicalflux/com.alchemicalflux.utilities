using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AlchemicalFlux.Utilities.PackageGeneration
{
    [CreateAssetMenu]
    public class GeneratePackageEditorWindow : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset tree;

        private PackageEditor packageEditor = new();

        #region Methods

        [MenuItem("Tools/Generate Package")]
        public static void ShowEditorWindow()
        {
            var wnd = GetWindow<GeneratePackageEditorWindow>();
            wnd.titleContent = new GUIContent("Generate Package");
        }

        public void CreateGUI()
        {
            tree.CloneTree(rootVisualElement);
            packageEditor.InitUIComponents(rootVisualElement);
            packageEditor.AddOnSaveAction(Close);
        }

        #endregion Methods
    }
}