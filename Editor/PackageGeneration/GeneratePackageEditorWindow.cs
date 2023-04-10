using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AlchemicalFlux.Utilities
{
    public class GeneratePackageEditorWindow : EditorWindow
    {
        [SerializeField]
        private VisualTreeAsset tree;

        #region Constants

        public const string TemplateDomainName = "com";
        public const string TemplateCompanyName = "alchemicalflux";
        public const string TemplateProjectName = "package-template";
        public const string TemplatePackageName = TemplateDomainName + "." + TemplateCompanyName + "." + TemplateProjectName;

        public const string TempPath = "Temp/" + TemplatePackageName;
        public const string PackagePath = "Packages/" + TemplatePackageName;

        public const string DisplayFieldName = "DisplayField";
        public const string DomainFieldName = "DomainField";
        public const string CompanyFieldName = "CompanyField";
        public const string ProjectFieldName = "ProjectField";

        public const string RuntimeToggleName = "RuntimeToggle";
        public const string EditorToggleName = "EditorToggle";
        public const string TestsToggleName = "TestsToggle";
        public const string DocumentationToggleName = "DocumentationToggle";
        public const string SamplesToggleName = "SamplesToggle";

        public const string SaveButtonName = "SaveButton";
        
        #endregion Constants

        #region Members

        private TextField displayField;
        private TextField domainField;
        private TextField companyField;
        private TextField projectField;

        private Toggle setupRuntimeToggle;
        private Toggle setupEditorToggle;
        private Toggle includeTestsToggle;
        private Toggle documentationToggle;
        private Toggle includeSamplesToggle;

        private Button saveButton;

        #endregion Members

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
            InitUIComponents();
        }

        private void InitUIComponents()
        {
            displayField = rootVisualElement.Q<TextField>(DisplayFieldName);
            companyField = rootVisualElement.Q<TextField>(CompanyFieldName);
            projectField = rootVisualElement.Q<TextField>(ProjectFieldName);

            setupRuntimeToggle = rootVisualElement.Q<Toggle>(RuntimeToggleName);
            setupEditorToggle = rootVisualElement.Q<Toggle>(EditorToggleName);
            includeTestsToggle = rootVisualElement.Q<Toggle>(TestsToggleName);
            documentationToggle = rootVisualElement.Q<Toggle>(DocumentationToggleName);
            includeSamplesToggle = rootVisualElement.Q<Toggle>(SamplesToggleName);

            saveButton = rootVisualElement.Q<Button>(SaveButtonName);
            if (saveButton != null)
            {
                saveButton.clicked += SaveButtonPressed;
            }
        }

        private void SaveButtonPressed()
        {
            var directoryInfo = OverwriteDirectory(PackagePath, TempPath);
            PruneMetaFiles(directoryInfo);
            RenameFiles(directoryInfo);
        }

        private DirectoryInfo OverwriteDirectory(string source, string target)
        {
            var directoryInfo = new DirectoryInfo(target);
            if (directoryInfo.Exists)
            {
                directoryInfo.Delete(true);
            }

            FileUtil.CopyFileOrDirectory(source, target);

            return directoryInfo;
        }

        private void PruneMetaFiles(DirectoryInfo directoryInfo)
        {
            var files = directoryInfo.GetFiles("*.meta", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                file.Delete();
            }
        }

        private void RenameFiles(DirectoryInfo directoryInfo)
        {
            var prefix = "abc_";
            var files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var dir = file.Directory.FullName;
                var fileName = file.Name;
                var newPath = Path.Combine(dir, prefix + fileName);
                File.Move(file.FullName, newPath);
            }
        }

        #endregion Methods
    }
}