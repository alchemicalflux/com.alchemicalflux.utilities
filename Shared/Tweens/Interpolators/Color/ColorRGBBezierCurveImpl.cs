/*------------------------------------------------------------------------------
File:       ColorRGBBezierCurveImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Bezier curve interpolation in the RGB Color space.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-19 01:27:00 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Class that implements a Bezier curve interpolation in the RGB Color 
    /// space.
    /// </summary>
    public sealed class ColorRGBBezierCurveImpl : ColorBezierCurveImpl
    {
        #region Methods

        /// <summary>
        /// Constructor for the ColorRGBBezierCurveImpl class, which implements 
        /// a Bezier curve interpolation in the RGB Color space.
        /// </summary>
        /// <param name="nodes">
        /// Reference to the list of nodes for generating the Bezier curve.
        /// </param>
        public ColorRGBBezierCurveImpl(IList<Color> nodes) : base(nodes)
        {
        }

        #endregion Methods
    }
}