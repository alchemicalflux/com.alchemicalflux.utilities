using System.Collections.Generic;
using UnityEngine.UIElements;

namespace AlchemicalFlux.Utilities.PackageGeneration
{
    public class PackageEditorUI
    {
        private VisualElement rootVisualElement;

        public TextField displayField;
        public TextField domainField;
        public TextField companyField;
        public TextField projectField;
        public TextField companyNamespace;
        public TextField projectNamespace;

        public Toggle setupRuntimeToggle;
        public Toggle setupEditorToggle;
        public Toggle includeTestsToggle;
        public Toggle documentationToggle;
        public Toggle includeSamplesToggle;

        public Button saveButton;

        public string PackageName => $"{domainField.text}.{companyField.text}.{projectField.text}";

        public Dictionary<string, bool> FolderConditions => new() {
            { PackageConstants.TestsFolderName, includeTestsToggle.value},
            { PackageConstants.RuntimeFolderName, setupRuntimeToggle.value},
            { PackageConstants.EditorFolderName, setupEditorToggle.value },
            { PackageConstants.DocumentationFolderName, documentationToggle.value },
            { PackageConstants.SamplesFolderName, includeSamplesToggle.value },
        };

        public Dictionary<string, string> TemplateNamespaces => new() {
            { PackageConstants.TemplateCompanyNamespace, companyNamespace.text },
            { PackageConstants.TemplateProjectNamespace, projectNamespace.text },
        };

        public Dictionary<string, string> FileTextPlacements => new()
        {
            { PackageConstants.TemplateDisplayName, displayField.text },
            { PackageConstants.TemplateCompanyNamespace, companyNamespace.text },
            { PackageConstants.TemplateProjectNamespace, projectNamespace.text },
            { PackageConstants.TemplateDomainName, domainField.text },
            { PackageConstants.TemplateCompanyName, companyField.text },
            { PackageConstants.TemplateProjectName, projectField.text },
            { PackageConstants.AuthorName, "" },
            { PackageConstants.Email, "" },
            { PackageConstants.VersionRegEx, PackageConstants.DevPackageVersion },
        };

        public PackageEditorUI(VisualElement visualElement)
        {
            rootVisualElement = visualElement;

            AssignElement(ref displayField, PackageConstants.DisplayFieldName);
            AssignElement(ref domainField, PackageConstants.DomainFieldName);
            AssignElement(ref companyField, PackageConstants.CompanyFieldName);
            AssignElement(ref projectField, PackageConstants.ProjectFieldName);
            AssignElement(ref companyNamespace, PackageConstants.CompanyNamespaceName);
            AssignElement(ref projectNamespace, PackageConstants.ProjectNamespaceName);

            AssignElement(ref setupRuntimeToggle, PackageConstants.RuntimeToggleName);
            AssignElement(ref setupEditorToggle, PackageConstants.EditorToggleName);
            AssignElement(ref includeTestsToggle, PackageConstants.TestsToggleName);
            AssignElement(ref documentationToggle, PackageConstants.DocumentationToggleName);
            AssignElement(ref includeSamplesToggle, PackageConstants.SamplesToggleName);

            AssignElement(ref saveButton, PackageConstants.SaveButtonName);
        }

        private void AssignElement<TType>(ref TType target, string name)
            where TType : VisualElement
        {
            target = rootVisualElement.Q<TType>(name);
        }

        public void AddOnSaveAction(System.Action action)
        {
            saveButton.clicked += action;
        }
    }
}