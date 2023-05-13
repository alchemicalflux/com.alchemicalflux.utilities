using NUnit.Framework;
using UnityEngine.UIElements;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.PackageGeneration.Tests
{
    public class PackageEditorUITests
    {
        private PackageEditorUI packageEditorUI;
        private VisualElement visualElement;

        private const string displayName = "Project Name";
        private const string domainName = "com";
        private const string companyName = "company-name";
        private const string projectName = "project-name";
        private const string packageName = domainName+"."+companyName+"."+projectName;
        private const string companyNamespace = "CompanyNamespace";
        private const string projectNamespace = "ProjectNamespace";
        private const bool setupRuntime = true;
        private const bool setupEditor = true;
        private const bool setupTests = true;
        private const bool setupDocumentation = false;
        private const bool setupSamples = false;

        [SetUp]
        public void Setup()
        {
            visualElement = new VisualElement();

            visualElement.Add(new TextField() { name = PackageConstants.DisplayFieldName, value = displayName });
            visualElement.Add(new TextField() { name = PackageConstants.DomainFieldName, value = domainName });
            visualElement.Add(new TextField() { name = PackageConstants.CompanyFieldName, value = companyName });
            visualElement.Add(new TextField() { name = PackageConstants.ProjectFieldName, value = projectName });
            visualElement.Add(new TextField() { name = PackageConstants.CompanyNamespaceName, value = companyNamespace });
            visualElement.Add(new TextField() { name = PackageConstants.ProjectNamespaceName, value = projectNamespace });

            visualElement.Add(new Toggle() { name = PackageConstants.RuntimeToggleName, value = setupRuntime });
            visualElement.Add(new Toggle() { name = PackageConstants.EditorToggleName, value = setupEditor });
            visualElement.Add(new Toggle() { name = PackageConstants.TestsToggleName, value = setupTests });
            visualElement.Add(new Toggle() { name = PackageConstants.DocumentationToggleName, value = setupDocumentation });
            visualElement.Add(new Toggle() { name = PackageConstants.SamplesToggleName, value = setupSamples });

            visualElement.Add(new Button() { name = PackageConstants.SaveButtonName });

            packageEditorUI = new PackageEditorUI(visualElement);
        }

        [Test]
        public void PackageName_WithValidInputs_ReturnsExpectedPackageName()
        {
            Assert.AreEqual(packageName, packageEditorUI.PackageName);
        }

        [Test]
        public void FolderConditions_WithValidInputs_ReturnsExpectedFolderConditions()
        {
            Dictionary<string, bool> expectedFolderConditions = new Dictionary<string, bool>
            {
                { PackageConstants.TestsFolderName, true },
                { PackageConstants.RuntimeFolderName, true },
                { PackageConstants.EditorFolderName, true },
                { PackageConstants.DocumentationFolderName, false },
                { PackageConstants.SamplesFolderName, false }
            };

            var folderConditions = packageEditorUI.FolderConditions;

            CollectionAssert.AreEqual(expectedFolderConditions, folderConditions);
        }

        [Test]
        public void TemplateNamespaces_WithValidInputs_ReturnsExpectedTemplateNamespaces()
        {
            Dictionary<string, string> expectedTemplateNamespaces = new Dictionary<string, string>
            {
                { PackageConstants.TemplateCompanyNamespace, companyNamespace },
                { PackageConstants.TemplateProjectNamespace, projectNamespace},
            };

            var templateNamespaces = packageEditorUI.TemplateNamespaces;

            CollectionAssert.AreEqual(expectedTemplateNamespaces, templateNamespaces);
        }

        [Test]
        public void FileTextPlacements_WithValidInputs_ReturnsExpectedFileTextPlacements()
        {
            Dictionary<string, string> expectedFileTextPlacements = new Dictionary<string, string>
            {
                { PackageConstants.TemplateDisplayName, displayName },
                { PackageConstants.TemplateDomainName, domainName },
                { PackageConstants.TemplateCompanyName, companyName },
                { PackageConstants.TemplateProjectName, projectName },
                { PackageConstants.TemplateCompanyNamespace, companyNamespace },
                { PackageConstants.TemplateProjectNamespace, projectNamespace },
                { PackageConstants.AuthorName, "" },
                { PackageConstants.Email, "" },
                { PackageConstants.VersionRegEx, PackageConstants.DevPackageVersion },
            };

            var fileTextPlacements = packageEditorUI.FileTextPlacements;

            CollectionAssert.AreEqual(expectedFileTextPlacements, fileTextPlacements);
        }
    }
}