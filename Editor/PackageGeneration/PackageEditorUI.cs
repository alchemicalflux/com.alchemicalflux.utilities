using System;
using System.Collections.Generic;
using UnityEngine.UIElements;

namespace AlchemicalFlux.Utilities.PackageGeneration
{
    public class PackageEditorUI
    {
        private VisualElement rootVisualElement;

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
            { PackageConstants.TemplateDomainName, domainField.text },
            { PackageConstants.TemplateCompanyName, companyField.text },
            { PackageConstants.TemplateProjectName, projectField.text },
            { PackageConstants.TemplateCompanyNamespace, companyNamespace.text },
            { PackageConstants.TemplateProjectNamespace, projectNamespace.text },
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

        public void RemoveOnSaveAction(Action action)
        {
            saveButton.clicked -= action;
        }
    }
}