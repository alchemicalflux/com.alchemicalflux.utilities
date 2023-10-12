/*------------------------------------------------------------------------------
  File:           PackageEditorUITests.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Unit tests for UI portion of Unity package generation.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-12 01:13:52 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using UnityEngine.UIElements;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.PackageGeneration.Tests
{
    public class PackageEditorUITests
    {
        #region Members

        private PackageEditorUI packageEditorUI;
        private VisualElement visualElement;

        private const string displayName = "Project Name";
        private const string domainName = "com";
        private const string companyName = "company-name";
        private const string projectName = "project-name";
        private const string packageName = domainName + "." + companyName + "." + projectName;
        private const string companyNamespace = "CompanyNamespace";
        private const string projectNamespace = "ProjectNamespace";
        private const bool setupRuntime = true;
        private const bool setupEditor = true;
        private const bool setupTests = true;
        private const bool setupDocumentation = false;
        private const bool setupSamples = false;

        private const string visualAssetTreeResourcePath = "PackageGenerationVisualTreeAsset";

        #endregion Members

        #region Methods

        [SetUp]
        public void Setup()
        {
            // Arrange
            var visualTreeAsset = Resources.Load<VisualTreeAssetSO>(visualAssetTreeResourcePath).value;
            visualElement = new VisualElement();
            visualTreeAsset.CloneTree(visualElement);
            packageEditorUI = new PackageEditorUI(visualElement);
        }

        [Test]
        public void PackageName_WithValidInputs_ReturnsExpectedPackageName()
        {
            // Assert
            Assert.AreEqual(packageName, packageEditorUI.PackageName);
        }

        [Test]
        public void FolderConditions_WithValidInputs_ReturnsExpectedFolderConditions()
        {
            // Arrange
            var expectedFolderConditions = new Dictionary<string, bool>
            {
                { PackageConstants.TestsFolderName, setupRuntime },
                { PackageConstants.RuntimeFolderName, setupEditor },
                { PackageConstants.EditorFolderName, setupTests },
                { PackageConstants.DocumentationFolderName, setupDocumentation },
                { PackageConstants.SamplesFolderName, setupSamples }
            };

            // Act
            var folderConditions = packageEditorUI.FolderConditions;

            // Assert
            CollectionAssert.AreEqual(expectedFolderConditions, folderConditions);
        }

        [Test]
        public void FolderConditions_WithValidInputs_ReturnsExpectedFoldersToRemove()
        {
            // Arrange
            var expectedFoldersToRemove = new List<string>
            {
                "*" + PackageConstants.DocumentationFolderName + "*",
                "*" + PackageConstants.SamplesFolderName + "*",
            };

            // Act
            var foldersToRemove = packageEditorUI.FoldersToRemove;

            // Assert
            CollectionAssert.AreEqual(expectedFoldersToRemove, foldersToRemove);
        }

        [Test]
        public void TemplateNamespaces_WithValidInputs_ReturnsExpectedTemplateNamespaces()
        {
            // Arrange
            var expectedTemplateNamespaces = new Dictionary<string, string>
            {
                { PackageConstants.TemplateCompanyNamespace, companyNamespace },
                { PackageConstants.TemplateProjectNamespace, projectNamespace},
            };

            // Act
            var templateNamespaces = packageEditorUI.TemplateNamespaces;

            // Assert
            CollectionAssert.AreEqual(expectedTemplateNamespaces, templateNamespaces);
        }

        [Test]
        public void FileTextPlacements_WithValidInputs_ReturnsExpectedFileTextPlacements()
        {
            // Arrange
            var expectedFileTextPlacements = new Dictionary<string, string>
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

            // Act
            var fileTextPlacements = packageEditorUI.FileTextPlacements;

            // Assert
            CollectionAssert.AreEqual(expectedFileTextPlacements, fileTextPlacements);
        }

        #endregion Methods
    }
}