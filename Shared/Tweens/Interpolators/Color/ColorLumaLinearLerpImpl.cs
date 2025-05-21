/*------------------------------------------------------------------------------
File:       ColorLumaLinearLerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Color lerp using the linear color space and color
            intensity to improve from RGB Color transition.
            Details for algorithm implementation:
                https://stackoverflow.com/questions/22607043/color-gradient-algorithm
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-20 18:44:50 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    using Comsts = Constants.ColorConstants;

    /// <summary>
    /// Color lerp class that implements a RGB to linear to RGB conversion,
    /// performing the lerp in linear color space and factoring in the intensity
    /// to create a smoother transition for light/dark transitions.
    /// </summary>
    public sealed class ColorLumaLinearLerpImpl : ColorTwoPointImpl
    {
        #region Fields

        /// <summary>
        /// Precalculated linear space for Start color.
        /// </summary>
        private Color _sLinear;
        
        /// <summary>
        /// Precalculated luma values for Start color.
        /// </summary>
        private float _sLumaApproximate, _sBrightness;

        /// <summary>
        /// Precalculated linear space for End color.
        /// </summary>
        private Color _eLinear;
        
        /// <summary>
        /// Precalculated luma values for End color.
        /// </summary>
        private float _eLumaApproximate, _eBrightness;

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
                _sLumaApproximate = _sLinear.r + _sLinear.g + _sLinear.b;
                _sBrightness = Mathf.Pow(_sLumaApproximate, Comsts.Gamma);
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
                _eLumaApproximate = _eLinear.r + _eLinear.g + _eLinear.b;
                _eBrightness = Mathf.Pow(_eLumaApproximate, Comsts.Gamma);
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ColorLumaLinearLerpImpl class, 
        /// which implements color interpolation using gamma-adjusted linear 
        /// color space.
        /// </summary>
        /// <param name="start">The initial color for the interpolation.</param>
        /// <param name="end">The final color for the interpolation.</param>
        public ColorLumaLinearLerpImpl(Color start, Color end) :
            base(start, end)
        {
        }

        /// <inheritdoc />
        protected override Color ProcessInterpolation(float progress)
        {
            // Return early if both linear colors are black.
            if(_sLumaApproximate <= Comsts.Threshold &&
                _eLumaApproximate <= Comsts.Threshold) { return Start; }

            var color = Color.Lerp(_sLinear, _eLinear, progress);
            var sum = color.r + color.g + color.b;

            if(sum <= Comsts.Threshold) { return color; } // Lerped to black.

            var brightness = Mathf.Lerp(_sBrightness, _eBrightness, progress);
            var intensity = Mathf.Pow(brightness, Comsts.InverseGamma);

            var factor = intensity / sum;
            color.r *= factor;
            color.g *= factor;
            color.b *= factor;

            return color.gamma;
        }

        #endregion Methods
    }
}