/*------------------------------------------------------------------------------
File:       ColorConstants.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unique constants needed for color manipulation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-20 23:55:24 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Constants
{
    /// <summary>
    /// Provides unique constants needed for color manipulation.
    /// </summary>
    public static class ColorConstants
    {
        /// <summary>
        /// Gamma conversion value used for gamma correction.
        /// </summary>
        public const float Gamma = 0.43f;

        /// <summary>
        /// Inverse gamma conversion value used for inverse gamma correction.
        /// </summary>
        public const float InverseGamma = 1 / Gamma;

        /// <summary>
        /// Threshold value for color comparisons to account for floating-point 
        /// precision errors.
        /// </summary>
        public const float Threshold = 1e-6f;

        // Static constructor to validate constants.
        static ColorConstants()
        {
            // Ensure Threshold is non-negative.
            if(Threshold < 0)
            {
                throw new System.InvalidOperationException(
                    $"{nameof(Threshold)} must be non-negative.");
            }

            // Ensure Gamma is not zero to avoid division by zero.
            if(Gamma == 0)
            {
                throw new System.InvalidOperationException(
                    $"{nameof(Gamma)} must not be zero.");
            }
        }
    }
}
