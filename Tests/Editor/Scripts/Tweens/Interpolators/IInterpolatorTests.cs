/*------------------------------------------------------------------------------
File:       IInterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Defines an interface for testing interpolator implementations, 
            ensuring they handle valid and invalid progress values correctly.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-20 06:05:04 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Interface for testing interpolator implementations.
    /// </summary>
    /// <typeparam name="TType">
    /// The type of the value being interpolated.
    /// </typeparam>
    public interface IInterpolatorTests<TType>
    {
        /// <summary>
        /// Tests that the interpolator returns the expected value for valid
        /// progress inputs.
        /// </summary>
        /// <param name="progress">
        /// The progress value to test, typically between 0 and 1.
        /// </param>
        /// <param name="expectedValue">
        /// The expected interpolated value for the given progress.
        /// </param>
        void InterpolatorTests_Progress_ReturnsExpectedValue(
            float progress, TType expectedValue);

        /// <summary>
        /// Tests that the interpolator throws an 
        /// <see cref="ArgumentOutOfRangeException"/> 
        /// for invalid progress inputs.
        /// </summary>
        /// <param name="progress">
        /// The invalid progress value to test, such as values less than 0 or 
        /// greater than 1.
        /// </param>
        void InterpolatorTests_Progress_ThrowsArgumentOutOfRangeException(
            float progress);
    }
}
