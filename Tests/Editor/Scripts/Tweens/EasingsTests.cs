/*------------------------------------------------------------------------------
File:       EasingsTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for Easings.cs to validate the behavior of easing
            functions.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-08 00:34:36 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Helpers;
using NUnit.Framework;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="Easings"/> class.
    /// </summary>
    public class EasingsTests
    {
        #region Linear Tests

        [TestCase(0f, 0f)]
        [TestCase(0.25f, 0.25f)]
        [TestCase(0.5f, 0.5f)]
        [TestCase(0.75f, 0.75f)]
        [TestCase(1f, 1f)]
        public void Linear_ReturnsExpectedValue(float input, float expected)
        {
            // Act
            var result = Easings.Linear(input);

            // Assert
            Assert.IsTrue(expected.Approximately(result));
        }

        #endregion Linear Tests

        #region Mononomial Tests

        // Precomputed value for quadratic easing
        [TestCase(0f, 0f)]
        [TestCase(0.25f, 0.0625f)]
        [TestCase(0.5f, 0.25f)]
        [TestCase(0.75f, 0.5625f)]
        [TestCase(1f, 1f)]
        public void EaseIn_ReturnsExpectedValue(float input, float expected)
        {
            // Arrange
            var easeIn = Easings.EaseIn(2); // Quadratic easing

            // Act
            var result = easeIn(input);

            // Assert
            Assert.IsTrue(expected.Approximately(result));
        }

        // Precomputed value for quadratic easing
        [TestCase(0f, 0f)]
        [TestCase(0.25f, 0.4375f)]
        [TestCase(0.5f, 0.75f)]
        [TestCase(0.75f, 0.9375f)]
        [TestCase(1f, 1f)]
        public void EaseOut_ReturnsExpectedValue(float input, float expected)
        {
            // Arrange
            var easeOut = Easings.EaseOut(2); // Quadratic easing

            // Act
            var result = easeOut(input);

            // Assert
            Assert.IsTrue(expected.Approximately(result));
        }

        // Precomputed value for quadratic easing
        [TestCase(0f, 0f)]
        [TestCase(0.25f, 0.125f)]
        [TestCase(0.5f, 0.5f)]
        [TestCase(0.75f, 0.875f)]
        [TestCase(1f, 1f)]
        public void EaseInOut_ReturnsExpectedValue(float input, float expected)
        {
            // Arrange
            var easeInOut = Easings.EaseInOut(2); // Quadratic easing

            // Act
            var result = easeInOut(input);

            // Assert
            Assert.IsTrue(expected.Approximately(result));
        }

        #endregion Mononomial Tests

        #region Sinusoidal Tests

        // Precomputed value for 1 - Mathf.Cos(Mathf.PI * 0.25f / 2)
        [TestCase(0f, 0f)]
        [TestCase(0.25f, 0.0761205f)]
        [TestCase(0.5f, 0.2928932f)]
        [TestCase(0.75f, 0.6173166f)]
        [TestCase(1f, 1f)]
        public void EaseInSine_ReturnsExpectedValue(float input, float expected)
        {
            // Act
            var result = Easings.EaseInSine(input);

            // Assert
            Assert.IsTrue(expected.Approximately(result));
        }

        // Precomputed value for Mathf.Sin(Mathf.PI * 0.25f / 2)
        [TestCase(0f, 0f)]
        [TestCase(0.25f, 0.3826834f)]
        [TestCase(0.5f, 0.7071068f)]
        [TestCase(0.75f, 0.9238795f)]
        [TestCase(1f, 1f)]
        public void EaseOutSine_ReturnsExpectedValue(
            float input, float expected)
        {
            // Act
            var result = Easings.EaseOutSine(input);

            // Assert
            Assert.IsTrue(expected.Approximately(result));
        }

        // Precomputed value for (1 - Mathf.Cos(Mathf.PI * 0.25f)) / 2
        [TestCase(0f, 0f)]
        [TestCase(0.25f, 0.1464466f)]
        [TestCase(0.5f, 0.5f)]
        [TestCase(0.75f, 0.8535534f)]
        [TestCase(1f, 1f)]
        public void EaseInOutSine_ReturnsExpectedValue(
            float input, float expected)
        {
            // Act
            var result = Easings.EaseInOutSine(input);

            // Assert
            Assert.IsTrue(expected.Approximately(result));
        }

        #endregion Sinusoidal Tests

        #region Circular Tests

        // Precomputed value for 1 - Mathf.Sqrt(1 - Mathf.Pow(0.25f, 2))
        [TestCase(0f, 0f)]
        [TestCase(0.25f, 0.03175416f)]
        [TestCase(0.5f, 0.1339746f)]
        [TestCase(0.75f, 0.3385622f)]
        [TestCase(1f, 1f)]
        public void EaseInCircle_ReturnsExpectedValue(
            float input, float expected)
        {
            // Act
            var result = Easings.EaseInCircle(input);

            // Assert
            Assert.IsTrue(expected.Approximately(result));
        }

        // Precomputed value for Mathf.Sqrt(1 - Mathf.Pow(0.25f - 1, 2))
        [TestCase(0f, 0f)]
        [TestCase(0.25f, 0.6614378f)]
        [TestCase(0.5f, 0.8660254f)]
        [TestCase(0.75f, 0.9682458f)]
        [TestCase(1f, 1f)]
        public void EaseOutCircle_ReturnsExpectedValue(
            float input, float expected)
        {
            // Act
            var result = Easings.EaseOutCircle(input);

            // Assert
            Assert.IsTrue(expected.Approximately(result));
        }

        // Precomputed value for
        // (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * 0.25f, 2))) / 2
        [TestCase(0f, 0f)]
        [TestCase(0.25f, 0.0669873f)]
        [TestCase(0.5f, 0.5f)]
        [TestCase(0.75f, 0.9330127f)]
        [TestCase(1f, 1f)]
        public void EaseInOutCircle_ReturnsExpectedValue(
            float input, float expected)
        {
            // Act
            var result = Easings.EaseInOutCircle(input);

            // Assert
            Assert.IsTrue(expected.Approximately(result));
        }

        #endregion Circular Tests
    }
}
