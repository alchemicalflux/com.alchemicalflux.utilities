/*------------------------------------------------------------------------------
  File:           VisualTreeAssetSO.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Wrapper for decoupled asset references.
  Copyright:      2023-2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-11-30 22:23:47 
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