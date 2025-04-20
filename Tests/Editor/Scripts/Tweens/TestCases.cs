/*------------------------------------------------------------------------------
File:       TestCases.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a static class containing constant names for test cases
            used in interpolation tests. These names are used to identify
            specific test scenarios for progress values.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-20 06:05:04 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Provides constant names for test cases used in interpolation tests.
    /// These constants are used to identify specific progress scenarios.
    /// </summary>
    public static class TestCases
    {
        /// <summary>
        /// Test case name for progress value of 0.
        /// </summary>
        public const string ProgressOfZero = "ProgressOfZero";

        /// <summary>
        /// Test case name for progress value of 0.5.
        /// </summary>
        public const string ProgressOfHalf = "ProgressOfHalf";

        /// <summary>
        /// Test case name for progress value of 1.
        /// </summary>
        public const string ProgressOfOne = "ProgressOfOne";

        /// <summary>
        /// Test case name for progress value of -1.
        /// </summary>
        public const string ProgressOfNegativeOne = "ProgressOfNegativeOne";

        /// <summary>
        /// Test case name for progress value of 2.
        /// </summary>
        public const string ProgressOfTwo = "ProgressOfTwo";

        /// <summary>
        /// Test case name for progress value of NaN.
        /// </summary>
        public const string NaNProgress = "NaNProgress";

        /// <summary>
        /// Test case name for progress value of positive infinity.
        /// </summary>
        public const string PositiveInfinityProgress =
            "PositiveInfinityProgress";

        /// <summary>
        /// Test case name for progress value of negative infinity.
        /// </summary>
        public const string NegativeInfinityProgress =
            "NegativeInfinityProgress";
    }
}
