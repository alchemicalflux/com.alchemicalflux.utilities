/*------------------------------------------------------------------------------
  File:           PackageEditorUI.cs 
  Project:        AlchemicalFlux Utilities
  Description:    UI encapsulation for handling Unity package generation.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-08-22 09:50:51 
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
        private TextField displayField;
        /// <summary>UI text entry for domain portion of package name.</summary>
        private TextField domainField;
        /// <summary>UI text entry for company portion of package name.</summary>
        private TextField companyField;
        /// <summary>UI text entry for project portion of package name.</summary>
        private TextField projectField;
        /// <summary>UI text entry for company portion of folder and file names.</summary>
        private TextField companyNamespace;
        /// <summary>UI text entry for project portion of folder and file names.</summary>
        private TextField projectNamespace;

        /// <summary>UI flag for creating runtime related folders/files.</summary>
        private Toggle setupRuntimeToggle;
        /// <summary>UI flag for creating editor related folders/files.</summary>
        private Toggle setupEditorToggle;
        /// <summary>UI flag indicating if test structures should be included.</summary>
        private Toggle includeTestsToggle;
        /// <summary>UI flag indicating if documentation structures should be included.</summary>
        private Toggle documentationToggle;
        /// <summary>UI flag indicating if sample structures should be included.</summary>
        private Toggle includeSamplesToggle;

        /// <summary>UI button that triggers save feature.</summary>
        private Button saveButton;

        /// <summary>Callbacks triggered on save button press.</summary>
        public Action OnSavePressed;

        #endregion Members

        #region Properties

        /// <summary>
        /// The name of the package folder.
        /// </summary>
        public string PackageName => $"{domainField.text}.{companyField.text}.{projectField.text}";

        /// <summary>
        /// The deletion status of various folders.
        /// </summary>
        public Dictionary<string, bool> FolderConditions => new() {
            { PackageConstants.TestsFolderName, includeTestsToggle.value},
            { PackageConstants.RuntimeFolderName, setupRuntimeToggle.value},
            { PackageConstants.EditorFolderName, setupEditorToggle.value },
            { PackageConstants.DocumentationFolderName, documentationToggle.value },
            { PackageConstants.SamplesFolderName, includeSamplesToggle.value },
        };

        /// <summary>
        /// A list of folders that should be removed on copy.
        /// </summary>
        public List<string> FoldersToRemove => FolderConditions
            .Where(folderCondition => !folderCondition.Value)
            .Select(folderCondition => "*" + folderCondition.Key + "*")
            .ToList();

        /// <summary>
        /// Mapping of file names to be replaced.
        /// </summary>
        public Dictionary<string, string> TemplateNamespaces => new() {
            { PackageConstants.TemplateCompanyNamespace, companyNamespace.text },
            { PackageConstants.TemplateProjectNamespace, projectNamespace.text },
        };

        /// <summary>
        /// Mapping of file text to be replaced.
        /// </summary>
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

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes all of the UI elements necessary for generating a package setup.
        /// </summary>
        /// <param name="rootVisualElement">Parent element that contains all the necessary UI.</param>
        public PackageEditorUI(VisualElement rootVisualElement)
        {
            rootVisualElement.Q(ref displayField, PackageConstants.DisplayFieldName);
            rootVisualElement.Q(ref domainField, PackageConstants.DomainFieldName);
            rootVisualElement.Q(ref companyField, PackageConstants.CompanyFieldName);
            rootVisualElement.Q(ref projectField, PackageConstants.ProjectFieldName);
            rootVisualElement.Q(ref companyNamespace, PackageConstants.CompanyNamespaceName);
            rootVisualElement.Q(ref projectNamespace, PackageConstants.ProjectNamespaceName);

            rootVisualElement.Q(ref setupRuntimeToggle, PackageConstants.RuntimeToggleName);
            rootVisualElement.Q(ref setupEditorToggle, PackageConstants.EditorToggleName);
            rootVisualElement.Q(ref includeTestsToggle, PackageConstants.TestsToggleName);
            rootVisualElement.Q(ref documentationToggle, PackageConstants.DocumentationToggleName);
            rootVisualElement.Q(ref includeSamplesToggle, PackageConstants.SamplesToggleName);

            rootVisualElement.Q(ref saveButton, PackageConstants.SaveButtonName);

            saveButton.clicked += () =>
            {
                OnSavePressed?.Invoke();
            };
        }

        #endregion Methods
    }
}