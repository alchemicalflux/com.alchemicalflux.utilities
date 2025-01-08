<h1 align="center" style="border-bottom: none;">AlchemicalFlux Unity Utilities</h1>

# Overview

**AlchemicalFlux Unity Utilities** provides access to common functionality through the use of the Unity Package Manager.

# Package Contents

### List of Available Utilities
- Generate Package Unity Editor
- Git Utilities
  * Precommits
  * Semantic Release
- Helpers
  * Extensions
  * File Operations
  * LRUCache
  * NullCheck
  * Reflection Utilities
  * Singleton
  * SmartCoroutine
  * String Manipulator
- Tweens
  
# Installation Instructions

### Package Dependencies
**For automatic dependency installation:**
Install via Git through the Unity Package Manager:  
https://github.com/hibzzgames/Hibzz.DependencyResolver.git  

**For manual setup:**
1) Open the Unity Utilities package.json  
2) Copy all of the packages under the "git-dependencies" section, including any version numbers  
3) Add the copied packages to your projects Packages/manifest.json  
4) Ensure all packages are correctly installed

### Installing Package
Install via Git through the Unity Package Manager:  
https://github.com/alchemicalflux/com.alchemicalflux.utilities.git  

# Limitations

# Workflows

### Generate Package Unity Editor
1) Select from tool bar: Tools/AlchemicalFlux Utilities/Generate Package  
2) Update all entries to match package requirements  
3) Press the Save button and wait for altered template to be added to Assets folder  

### Git Utilities
1) Select from tool bar: Tools/AlchemicalFlux Utilities/Git Operations  
2) Check if folder search field meets needs  
    - The search will scan all folders under the selection  
3) Select which utilities to be installed for any found git folder(s)  
4) Press "Install Selections" and wait for files to be copied  
- Addtional steps required for Semantic Release option  
  + Create access token (such as using npmjs.com)
  + Assign token to repository secrets as NPM_TOKEN
  + Verify that package.json contains "version": "0.0.0-development"

### NullCheck
The NullCheck attribute generates a warning if a field is left unassigned
1) Apply the NullCheck attribute to object reference fields in MonoBehaviour scripts
2) IgnorePrefab parameter prevents processing of errors for null fields in prefabs.
3) Manually triggered tests exist under tool bar: Tools/AlchemicalFlux Utilities/NullCheck

# Advanced Topics

# Reference

# Samples

# Tutorials
