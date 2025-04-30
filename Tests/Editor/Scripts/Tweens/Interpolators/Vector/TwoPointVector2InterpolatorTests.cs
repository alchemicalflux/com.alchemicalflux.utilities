/*------------------------------------------------------------------------------
File:       TwoPointVector2InterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for unit tests of TwoPointInterpolator<Vector2>.
            Provides a foundation for testing Vector2 interpolation logic.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-29 20:16:53 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Abstract base class for unit tests of 
    /// <see cref="TwoPointInterpolator{TType}"/> with <see cref="Vector2"/>.
    /// </summary>
    public abstract class TwoPointVector2InterpolatorTests
        : TwoPointInterpolatorTests<Vector2>
    {
    }
}
