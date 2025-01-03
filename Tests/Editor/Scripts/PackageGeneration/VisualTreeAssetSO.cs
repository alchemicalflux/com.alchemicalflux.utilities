/*------------------------------------------------------------------------------
  File:           VisualTreeAssetSO.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Wrapper for decoupled asset references.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-01 22:20:10 
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