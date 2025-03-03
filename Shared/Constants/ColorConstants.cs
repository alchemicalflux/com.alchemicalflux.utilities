/*------------------------------------------------------------------------------
File:       ColorConstants.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unique constants needed for color manipulation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-03 01:01:35 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Constants
{
    public static class ColorConstants
    {
        /// <summary>Gamma coversion value.</summary>
        public const float Gamma = 0.43f;

        /// <summary>Inverse gamma coversion value.</summary>
        public const float InverseGamma = 1 / Gamma;

        /// <summary>Gamma coversion value.</summary>
        public const float Threshold = 1e-6f;
    }
}