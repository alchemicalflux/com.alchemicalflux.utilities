/*------------------------------------------------------------------------------
File:       VisualTreeAssetSO.cs 
Project:    AlchemicalFlux Utilities
Overview:   Wrapper for decoupled asset references.
Copyright:  2023-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 16:56:47 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Helpers;
using UnityEngine;
using UnityEngine.UIElements;

namespace AlchemicalFlux.Utilities.PackageGeneration.Tests
{
    /// <summary>
    /// Asset for decoupling unit testing functionality from project assets.
    /// </summary>
    [CreateAssetMenu(menuName = "AlchemicalFlux/Tests/VisualTreeAssetSO")]
    public class VisualTreeAssetSO : ScriptableObjectWrapper<VisualTreeAsset>
    {
    };
}