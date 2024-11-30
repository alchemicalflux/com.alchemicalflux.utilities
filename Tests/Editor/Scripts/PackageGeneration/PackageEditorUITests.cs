/*------------------------------------------------------------------------------
  File:           PackageEditorUITests.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Unit tests for UI portion of Unity package generation.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-11-29 20:46:10 
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

        private PackageEditorUI _packageEditorUI;
        private VisualElement _visualElement;

        private const string _displayName = "Project Name";
        private const string _domainName = "com";
        private const string _companyName = "company-name";
        private const string _projectName = "project-name";
        private const string _packageName = _domainName + "." + _companyName + "." + _projectName;
        private const string _companyNamespace = "CompanyNamespace";
        private const string _projectNamespace = "ProjectNamespace";
        private const bool _setupRuntime = true;
        private const bool _setupEditor = true;
        private const bool _setupTests = true;
        private const bool _setupDocumentation = false;
        private const bool _setupSamples = false;

        private const string _visualAssetTreeResourcePath = "PackageGeneration/PackageGenerationVisualTreeAsset";

        #endregion Members

        #region Methods

        [SetUp]
        public void Setup()
        {
            // Arrange
            var visualTreeAsset = Resources.Load<VisualTreeAssetSO>(_visualAssetTreeResourcePath).value;
            _visualElement = new VisualElement();
            visualTreeAsset.CloneTree(_visualElement);
            _packageEditorUI = new PackageEditorUI(_visualElement);
        }

        [Test]
        public void PackageName_WithValidInputs_ReturnsExpectedPackageName()
        {
            // Assert
            Assert.AreEqual(_packageName, _packageEditorUI.PackageName);
        }

        [Test]
        public void FolderConditions_WithValidInputs_ReturnsExpectedFolderConditions()
        {
            // Arrange
            var expectedFolderConditions = new Dictionary<string, bool>
            {
                { PackageConstants.TestsFolderName, _setupRuntime },
                { PackageConstants.RuntimeFolderName, _setupEditor },
                { PackageConstants.EditorFolderName, _setupTests },
                { PackageConstants.DocumentationFolderName, _setupDocumentation },
                { PackageConstants.SamplesFolderName, _setupSamples }
            };

            // Act
            var folderConditions = _packageEditorUI.FolderConditions;

            // Assert
            CollectionAssert.AreEqual(expectedFolderConditions, folderConditions);
        }

        [Test]
        public void FolderConditions_WithValidInputs_ReturnsExpectedFoldersToRemove()
        {
            // Arrange
            var expectedFoldersToRemove = new List<string>
            {
                $"*{PackageConstants.DocumentationFolderName}*",
                $"*{PackageConstants.SamplesFolderName}*",
            };

            // Act
            var foldersToRemove = _packageEditorUI.FoldersToRemove;

            // Assert
            CollectionAssert.AreEqual(expectedFoldersToRemove, foldersToRemove);
        }

        [Test]
        public void TemplateNamespaces_WithValidInputs_ReturnsExpectedTemplateNamespaces()
        {
            // Arrange
            var expectedTemplateNamespaces = new Dictionary<string, string>
            {
                { PackageConstants.TemplateCompanyNamespace, _companyNamespace },
                { PackageConstants.TemplateProjectNamespace, _projectNamespace},
                { PackageConstants.TemplateProjectName, _projectName },
            };

            // Act
            var templateNamespaces = _packageEditorUI.TemplateNamespaces;

            // Assert
            CollectionAssert.AreEqual(expectedTemplateNamespaces, templateNamespaces);
        }

        [Test]
        public void FileTextPlacements_WithValidInputs_ReturnsExpectedFileTextPlacements()
        {
            // Arrange
            var expectedFileTextPlacements = new Dictionary<string, string>
            {
                { PackageConstants.TemplateDisplayName, _displayName },
                { PackageConstants.TemplateDomainName, _domainName },
                { PackageConstants.TemplateCompanyName, _companyName },
                { PackageConstants.TemplateProjectName, _projectName },
                { PackageConstants.TemplateCompanyNamespace, _companyNamespace },
                { PackageConstants.TemplateProjectNamespace, _projectNamespace },
                { PackageConstants.AuthorName, "" },
                { PackageConstants.Email, "" },
                { PackageConstants.VersionRegEx, PackageConstants.DevPackageVersion },
            };

            // Act
            var fileTextPlacements = _packageEditorUI.FileTextPlacements;

            // Assert
            CollectionAssert.AreEqual(expectedFileTextPlacements, fileTextPlacements);
        }

        #endregion Methods
    }
}