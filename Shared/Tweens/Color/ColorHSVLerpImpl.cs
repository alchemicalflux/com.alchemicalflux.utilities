/*------------------------------------------------------------------------------
File:       ColorHSVLerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Color lerp using the HSV color space to improve from
            RGB Color transition.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-03 01:59:43 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Coler lerp class that implements a RGB to HSV to RGB conversion,
    /// performing the lerp in HSV color space.
    /// </summary>
    public sealed class ColorHSVLerpImpl : TwoPointInterpolator<Color>
    {
        #region Fields

        /// <summary>Precalulated HSV values for Start color.</summary>
        private float _hStart, _sStart, _vStart;

        /// <summary>Precalulated HSV values for End color.</summary>
        private float _hEnd, _sEnd, _vEnd;

        #endregion Fields

        #region Properties

        /// <inheritdoc />
        public override Color Start
        {
            get => base.Start;
            set
            {
                base.Start = value;
                // Generate and store HSV precalculation variables.
                Color.RGBToHSV(base.Start, out _hStart, out _sStart, 
                    out _vStart);
                _hStart *= 360f;
            }
        }

        /// <inheritdoc />
        public override Color End
        {
            get => base.End;
            set
            {
                base.End = value;
                // Generate and store HSV precalculation variables.
                Color.RGBToHSV(base.End, out _hEnd, out _sEnd, out _vEnd);
                _hEnd *= 360f;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the ColorHSVLerpImpl class, which 
        /// implements color interpolation using HSV (Hue, Saturation, Value) 
        /// color space.
        /// </summary>
        /// <param name="start">The initial color for the interpolation.</param>
        /// <param name="end">The final color for the interpolation.</param>
        public ColorHSVLerpImpl(Color start, Color end) :
            base(start, end)
        {
        }

        #region TwoPointInterpolator Implemenation

        /// <inheritdoc/>
        public override Color Interpolate(float progress)
        {
            // Slerp the hue (ensure the shortest path on the color wheel).
            var h = Mathf.LerpAngle(_hStart, _hEnd, progress);
            h = Mathf.Repeat(h, 360f) / 360f;

            // Transition the saturation and value consistently between colors.
            var s = Mathf.Lerp(_sStart, _sEnd, progress);
            var v = Mathf.Lerp(_vStart, _vEnd, progress);

            var color = Color.HSVToRGB(h, s, v);
            color.a = Mathf.Lerp(Start.a, End.a, progress);
            return color;
        }

        #endregion TwoPointInterpolator Implemenation

        #endregion Methods
    }
}