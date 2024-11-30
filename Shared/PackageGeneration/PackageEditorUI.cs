/*------------------------------------------------------------------------------
  File:           PackageEditorUI.cs 
  Project:        AlchemicalFlux Utilities
  Description:    UI encapsulation for handling Unity package generation.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-11-29 20:46:10 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;

namespace AlchemicalFlux.Utilities.PackageGeneration
{
    /// <summary>
    /// Handles the collection and setup of necessary UI elements for package generation.
    /// </summary>
    public class PackageEditorUI
    {
        #region Members

        /// <summary>UI text entry for package display name in Package folder.</summary>
        private TextField _displayField;
        /// <summary>UI text entry for domain portion of package name.</summary>
        private TextField _domainField;
        /// <summary>UI text entry for company portion of package name.</summary>
        private TextField _companyField;
        /// <summary>UI text entry for project portion of package name.</summary>
        private TextField _projectField;
        /// <summary>UI text entry for company portion of folder and file names.</summary>
        private TextField _companyNamespace;
        /// <summary>UI text entry for project portion of folder and file names.</summary>
        private TextField _projectNamespace;

        /// <summary>UI flag for creating runtime related folders/files.</summary>
        private Toggle _setupRuntimeToggle;
        /// <summary>UI flag for creating editor related folders/files.</summary>
        private Toggle _setupEditorToggle;
        /// <summary>UI flag indicating if test structures should be included.</summary>
        private Toggle _includeTestsToggle;
        /// <summary>UI flag indicating if documentation structures should be included.</summary>
        private Toggle _documentationToggle;
        /// <summary>UI flag indicating if sample structures should be included.</summary>
        private Toggle _includeSamplesToggle;

        /// <summary>UI button that triggers save feature.</summary>
        private Button _saveButton;

        /// <summary>Callbacks triggered on save button press.</summary>
        public Action OnSavePressed;

        #endregion Members

        #region Properties

        /// <summary>
        /// The name of the package folder.
        /// </summary>
        public string PackageName => $"{_domainField.text}.{_companyField.text}.{_projectField.text}";

        /// <summary>
        /// The deletion status of various folders.
        /// </summary>
        public Dictionary<string, bool> FolderConditions => new() {
            { PackageConstants.TestsFolderName, _includeTestsToggle.value },
            { PackageConstants.RuntimeFolderName, _setupRuntimeToggle.value },
            { PackageConstants.EditorFolderName, _setupEditorToggle.value },
            { PackageConstants.DocumentationFolderName, _documentationToggle.value },
            { PackageConstants.SamplesFolderName, _includeSamplesToggle.value },
        };

        /// <summary>
        /// A list of folders that should be removed on copy.
        /// </summary>
        public List<string> FoldersToRemove => FolderConditions
            .Where(folderCondition => !folderCondition.Value)
            .Select(folderCondition => $"*{folderCondition.Key}*")
            .ToList();

        /// <summary>
        /// Mapping of file names to be replaced.
        /// </summary>
        public Dictionary<string, string> TemplateNamespaces => new() {
            { PackageConstants.TemplateCompanyNamespace, _companyNamespace.text },
            { PackageConstants.TemplateProjectNamespace, _projectNamespace.text },
            { PackageConstants.TemplateProjectName, _projectField.text },
        };

        /// <summary>
        /// Mapping of file text to be replaced.
        /// </summary>
        public Dictionary<string, string> FileTextPlacements => new()
        {
            { PackageConstants.TemplateDisplayName, _displayField.text },
            { PackageConstants.TemplateDomainName, _domainField.text },
            { PackageConstants.TemplateCompanyName, _companyField.text },
            { PackageConstants.TemplateProjectName, _projectField.text },
            { PackageConstants.TemplateCompanyNamespace, _companyNamespace.text },
            { PackageConstants.TemplateProjectNamespace, _projectNamespace.text },
            { PackageConstants.AuthorName, "" },
            { PackageConstants.Email, "" },
            { PackageConstants.VersionRegEx, PackageConstants.DevPackageVersion },
        };

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes all of the UI elements necessary for generating a package setup.
        /// </summary>
        /// <param name="rootVisualElement">Parent element that contains all the necessary UI.</param>
        public PackageEditorUI(VisualElement rootVisualElement)
        {
            rootVisualElement.Q(ref _displayField, PackageConstants.DisplayFieldName);
            rootVisualElement.Q(ref _domainField, PackageConstants.DomainFieldName);
            rootVisualElement.Q(ref _companyField, PackageConstants.CompanyFieldName);
            rootVisualElement.Q(ref _projectField, PackageConstants.ProjectFieldName);
            rootVisualElement.Q(ref _companyNamespace, PackageConstants.CompanyNamespaceName);
            rootVisualElement.Q(ref _projectNamespace, PackageConstants.ProjectNamespaceName);

            rootVisualElement.Q(ref _setupRuntimeToggle, PackageConstants.RuntimeToggleName);
            rootVisualElement.Q(ref _setupEditorToggle, PackageConstants.EditorToggleName);
            rootVisualElement.Q(ref _includeTestsToggle, PackageConstants.TestsToggleName);
            rootVisualElement.Q(ref _documentationToggle, PackageConstants.DocumentationToggleName);
            rootVisualElement.Q(ref _includeSamplesToggle, PackageConstants.SamplesToggleName);

            rootVisualElement.Q(ref _saveButton, PackageConstants.SaveButtonName);

            _saveButton.clicked += () =>
            {
                OnSavePressed?.Invoke();
            };
        }

        #endregion Methods
    }
}