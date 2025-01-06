/*------------------------------------------------------------------------------
File:       PackageConstants.cs 
Project:    AlchemicalFlux Utilities
Overview:   Centralized location for Unity package generation constants.
Copyright:  2023-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 16:56:47 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.PackageGeneration
{
    /// <summary>
    /// Stores useful values for package generation editing.
    /// </summary>
    public static class PackageConstants
    {
        // Values from the Package template
        public const string TemplateDomainName = "com";
        public const string TemplateCompanyName = "alchemicalflux";
        public const string TemplateProjectName = "package-template";
        public const string TemplatePackageName =
            TemplateDomainName + "." + TemplateCompanyName + "." + TemplateProjectName;

        // Directory information
        public const string TempPath = "Temp/";
        public const string PackagePath = "Packages/" + TemplatePackageName + "/Documents";
        public const string AssetsPath = "Assets/";

        // UI interface references
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

        // Values to replace in files from the Package template
        public const string TemplateDisplayName = "AlchemicalFlux Package Template";
        public const string TemplateCompanyNamespace = "AlchemicalFlux";
        public const string TemplateProjectNamespace = "PackageTemplate";

        public const string AuthorName = "Jeremy Miller";
        public const string Email = "alchemicalflux@gmail.com";

        public const string DevPackageVersion = "\"version\": \"0.0.0-development\"";
        public const string VersionRegEx = "\"version\": \".*\"";

        // File names that can be modified from the Package template
        public const string TestsFolderName = "Tests";
        public const string RuntimeFolderName = "Runtime";
        public const string EditorFolderName = "Editor";
        public const string DocumentationFolderName = "Documentation~";
        public const string SamplesFolderName = "Samples~";
    }
}