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

        public const string TemplateCompanyNamespace = "AlchemicalFlux";
        public const string TemplateProjectNamespace = "PackageTemplate";

        public const string TestsFolderName = "Tests";
        public const string RuntimeFolderName = "Runtime";
        public const string EditorFolderName = "Editor";
        public const string DocumentationFolderName = "Documentation~";
        public const string SamplesFolderName = "Samples~";

        public const string TempPath = "Temp/";
        public const string PackagePath = "Packages/" + TemplatePackageName;
        public const string AssetsPath = "Assets/";

        public const string DisplayFieldName = "DisplayField";
        public const string DomainFieldName = "DomainField";
        public const string CompanyFieldName = "CompanyField";
        public const string ProjectFieldName = "ProjectField";
        public const string CompanyNamespaceName = "CompanyNamespace";
        public const string ProjectNamespaceName = "ProjectNamespace";

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
        private TextField companyNamespace;
        private TextField projectNamespace;

        private Toggle setupRuntimeToggle;
        private Toggle setupEditorToggle;
        private Toggle includeTestsToggle;
        private Toggle documentationToggle;
        private Toggle includeSamplesToggle;

        private Button saveButton;

        #endregion Members

        #region Properties

        string PackageName
        {
            get { return domainField.text + "." + companyField.text + "." + projectField.text; }
        }

        #endregion Properties

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
            domainField = rootVisualElement.Q<TextField>(DomainFieldName);
            companyField = rootVisualElement.Q<TextField>(CompanyFieldName);
            projectField = rootVisualElement.Q<TextField>(ProjectFieldName);
            companyNamespace = rootVisualElement.Q<TextField>(CompanyNamespaceName);
            projectNamespace = rootVisualElement.Q<TextField>(ProjectNamespaceName);

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
            var tempPath = TempPath + PackageName;
            var directoryInfo = OverwriteDirectory(PackagePath, tempPath);

            // Delete unwanted folders as necessary
            RemoveUnwantedFolders(directoryInfo, TestsFolderName, !includeTestsToggle.value);
            RemoveUnwantedFolders(directoryInfo, RuntimeFolderName, !setupRuntimeToggle.value);
            RemoveUnwantedFolders(directoryInfo, EditorFolderName, !setupEditorToggle.value);
            RemoveUnwantedFolders(directoryInfo, DocumentationFolderName, !documentationToggle.value);
            RemoveUnwantedFolders(directoryInfo, SamplesFolderName, !includeSamplesToggle.value);

            PruneMetaFiles(directoryInfo);

            RenameFiles(directoryInfo);

            OverwriteDirectory(tempPath, AssetsPath + PackageName);

            //Close();
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

        private void RemoveUnwantedFolders(DirectoryInfo directoryInfo, string folderName, bool deleteFolder)
        {
            if(deleteFolder)
            {
                var directories = directoryInfo.GetDirectories("*" + folderName + "*", SearchOption.AllDirectories);

                foreach (var dir in directories)
                {
                    dir.Delete(true);
                }
            }
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
            var files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var dir = file.Directory.FullName;

                var fileName = file.Name.Replace(TemplateCompanyNamespace, companyNamespace.text);
                fileName = fileName.Replace(TemplateProjectNamespace, projectNamespace.text);

                var text = File.ReadAllText(file.FullName);
                
                text = text.Replace(TemplateCompanyNamespace, companyNamespace.text);
                text = text.Replace(TemplateProjectNamespace, projectNamespace.text);

                text = text.Replace(TemplateDomainName, domainField.text);
                text = text.Replace(TemplateCompanyName, companyField.text);
                text = text.Replace(TemplateProjectName, projectField.text);
                
                File.WriteAllText(file.FullName, text);

                var newPath = Path.Combine(dir, fileName);
                File.Move(file.FullName, newPath);
            }
        }

        #endregion Methods
    }
}