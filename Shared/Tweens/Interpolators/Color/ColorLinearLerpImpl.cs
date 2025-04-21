/*------------------------------------------------------------------------------
File:       ColorLinearLerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Color lerp using the linear color space to improve from
            RGB Color transition.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-21 00:45:23 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Color lerp class that implements a RGB to linear to RGB conversion,
    /// performing the lerp in linear color space.
    /// </summary>
    public sealed class ColorLinearLerpImpl : TwoPointInterpolator<Color>
    {
        #region Fields

        /// <summary>
        /// Precalculated linear space for Start color.
        /// </summary>
        private Color _sLinear;

        /// <summary>
        /// Precalculated linear space for End color.
        /// </summary>
        private Color _eLinear;

        #endregion Fields

        #region Properties

        /// <inheritdoc />
        public override Color Start
        {
            get => base.Start;
            set
            {
                base.Start = value;
                _sLinear = base.Start.linear;
            }
        }

        /// <inheritdoc />
        public override Color End
        {
            get => base.End;
            set
            {
                base.End = value;
                _eLinear = base.End.linear;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ColorLinearLerpImpl class, which 
        /// implements color interpolation using linear color space.
        /// </summary>
        /// <param name="start">The initial color for the interpolation.</param>
        /// <param name="end">The final color for the interpolation.</param>
        public ColorLinearLerpImpl(Color start, Color end) :
            base(start, end)
        {
        }

        /// <inheritdoc />
        public override Color Interpolate(float progress)
        {
            if(float.IsNaN(progress)) { return Color.clear; }
            return Color.Lerp(_sLinear, _eLinear, progress).gamma;
        }

        #endregion Methods
    }
}