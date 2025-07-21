/*------------------------------------------------------------------------------
File:       ITweenTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Defines a contract for testing tween progress application logic.
            Implementations should verify correct behavior for valid and invalid
            progress values.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-20 22:54:35 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Defines a contract for testing tween progress application logic.
    /// Implementations should verify correct behavior for valid and invalid
    /// progress values.
    /// </summary>
    public interface ITweenTests
    {
        /// <summary>
        /// Applies a valid progress value to the tween and verifies that no
        /// exception is thrown.
        /// </summary>
        /// <param name="progress">
        /// A valid progress value, typically in the range [0, 1].
        /// </param>
        void ApplyProgress_ValidProgress_DoesNotThrowException(float progress);

        /// <summary>
        /// Applies an invalid progress value to the tween and verifies that an
        /// <see cref="System.ArgumentOutOfRangeException"/> is thrown.
        /// </summary>
        /// <param name="progress">
        /// An invalid progress value, typically outside the range [0, 1].
        /// </param>
        void ApplyProgress_InvalidProgress_ThrowsArgumentOutOfRangeException(float progress);
    }
}
