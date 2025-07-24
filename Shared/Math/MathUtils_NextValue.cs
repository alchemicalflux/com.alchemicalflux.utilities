/*------------------------------------------------------------------------------
File:       MathUtils_NextValue.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides utility methods for obtaining the next representable value
            above or below a given numeric value for various integer and
            floating-point types.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-20 21:48:09 
------------------------------------------------------------------------------*/
using System;

namespace AlchemicalFlux.Utilities.Math
{
    /// <summary>
    /// Provides utility methods for obtaining the next representable value
    /// above or below a given numeric value for various integer and
    /// floating-point types.
    /// <para>
    /// This class is useful for precise numeric computations, such as stepping
    /// through floating-point values or handling edge cases in algorithms that
    /// require exact control over numeric increments and decrements.
    /// </para>
    /// <para>
    /// Supported types include <see cref="int"/>, <see cref="uint"/>,
    /// <see cref="long"/>, <see cref="ulong"/>, <see cref="float"/>, and
    /// <see cref="double"/>.
    /// </para>
    /// </summary>
    public static partial class MathUtils
    {
        #region Integers

        /// <summary>
        /// Returns the next representable value greater than the specified
        /// <paramref name="value"/> for <see cref="uint"/>.
        /// If <paramref name="value"/> is <see cref="uint.MaxValue"/>, returns
        /// <see cref="uint.MaxValue"/>.
        /// </summary>
        /// <param name="value">The value to increment.</param>
        /// <returns>
        /// The next greater <see cref="uint"/> value, or
        /// <see cref="uint.MaxValue"/> if already at maximum.
        /// </returns>
        public static uint NextUp(uint value)
        {
            return value == uint.MaxValue ? uint.MaxValue : value + 1;
        }

        /// <summary>
        /// Returns the next representable value less than the specified
        /// <paramref name="value"/> for <see cref="uint"/>.
        /// If <paramref name="value"/> is <see cref="uint.MinValue"/>, returns
        /// <see cref="uint.MinValue"/>.
        /// </summary>
        /// <param name="value">The value to decrement.</param>
        /// <returns>
        /// The next smaller <see cref="uint"/> value, or
        /// <see cref="uint.MinValue"/> if already at minimum.
        /// </returns>
        public static uint NextDown(uint value)
        {
            return value == uint.MinValue ? uint.MinValue : value - 1;
        }

        /// <summary>
        /// Returns the next representable value greater than the specified
        /// <paramref name="value"/> for <see cref="int"/>.
        /// If <paramref name="value"/> is <see cref="int.MaxValue"/>, returns
        /// <see cref="int.MaxValue"/>.
        /// </summary>
        /// <param name="value">The value to increment.</param>
        /// <returns>
        /// The next greater <see cref="int"/> value, or
        /// <see cref="int.MaxValue"/> if already at maximum.
        /// </returns>
        public static int NextUp(int value)
        {
            return value == int.MaxValue ? int.MaxValue : value + 1;
        }

        /// <summary>
        /// Returns the next representable value less than the specified
        /// <paramref name="value"/> for <see cref="int"/>.
        /// If <paramref name="value"/> is <see cref="int.MinValue"/>, returns
        /// <see cref="int.MinValue"/>.
        /// </summary>
        /// <param name="value">The value to decrement.</param>
        /// <returns>
        /// The next smaller <see cref="int"/> value, or
        /// <see cref="int.MinValue"/> if already at minimum.
        /// </returns>
        public static int NextDown(int value)
        {
            return value == int.MinValue ? int.MinValue : value - 1;
        }

        /// <summary>
        /// Returns the next representable value greater than the specified
        /// <paramref name="value"/> for <see cref="ulong"/>.
        /// If <paramref name="value"/> is <see cref="ulong.MaxValue"/>, returns
        /// <see cref="ulong.MaxValue"/>.
        /// </summary>
        /// <param name="value">The value to increment.</param>
        /// <returns>
        /// The next greater <see cref="ulong"/> value, or
        /// <see cref="ulong.MaxValue"/> if already at maximum.
        /// </returns>
        public static ulong NextUp(ulong value)
        {
            return value == ulong.MaxValue ? ulong.MaxValue : value + 1;
        }

        /// <summary>
        /// Returns the next representable value less than the specified
        /// <paramref name="value"/> for <see cref="ulong"/>.
        /// If <paramref name="value"/> is <see cref="ulong.MinValue"/>, returns
        /// <see cref="ulong.MinValue"/>.
        /// </summary>
        /// <param name="value">The value to decrement.</param>
        /// <returns>
        /// The next smaller <see cref="ulong"/> value, or
        /// <see cref="ulong.MinValue"/> if already at minimum.
        /// </returns>
        public static ulong NextDown(ulong value)
        {
            return value == ulong.MinValue ? ulong.MinValue : value - 1;
        }

        /// <summary>
        /// Returns the next representable value greater than the specified
        /// <paramref name="value"/> for <see cref="long"/>.
        /// If <paramref name="value"/> is <see cref="long.MaxValue"/>, returns
        /// <see cref="long.MaxValue"/>.
        /// </summary>
        /// <param name="value">The value to increment.</param>
        /// <returns>
        /// The next greater <see cref="long"/> value, or
        /// <see cref="long.MaxValue"/> if already at maximum.
        /// </returns>
        public static long NextUp(long value)
        {
            return value == long.MaxValue ? long.MaxValue : value + 1;
        }

        /// <summary>
        /// Returns the next representable value less than the specified
        /// <paramref name="value"/> for <see cref="long"/>.
        /// If <paramref name="value"/> is <see cref="long.MinValue"/>, returns
        /// <see cref="long.MinValue"/>.
        /// </summary>
        /// <param name="value">The value to decrement.</param>
        /// <returns>
        /// The next smaller <see cref="long"/> value, or
        /// <see cref="long.MinValue"/> if already at minimum.
        /// </returns>
        public static long NextDown(long value)
        {
            return value == long.MinValue ? long.MinValue : value - 1;
        }

        #endregion Integers

        #region Floating Point

        /// <summary>
        /// Returns the next representable <see cref="float"/> value greater
        /// than the specified <paramref name="value"/>.
        /// If <paramref name="value"/> is NaN, positive infinity, or negative
        /// infinity, returns <paramref name="value"/>.
        /// If <paramref name="value"/> is zero, returns
        /// <see cref="float.Epsilon"/>.
        /// </summary>
        /// <param name="value">The value to increment.</param>
        /// <returns>
        /// The next greater <see cref="float"/> value, or
        /// <paramref name="value"/> if not representable.
        /// </returns>
        public static float NextUp(float value)
        {
            if(float.IsNaN(value) ||
                value == float.PositiveInfinity ||
                value == float.NegativeInfinity)
            {
                return value;
            }
            if(value == 0f) { return float.Epsilon; }

            int bits = BitConverter.SingleToInt32Bits(value);
            if(value > 0) { ++bits; }
            else { --bits; }
            return BitConverter.Int32BitsToSingle(bits);
        }

        /// <summary>
        /// Returns the next representable <see cref="float"/> value less than
        /// the specified <paramref name="value"/>.
        /// If <paramref name="value"/> is NaN, positive infinity, or negative
        /// infinity, returns <paramref name="value"/>.
        /// If <paramref name="value"/> is zero, returns
        /// <c>-<see cref="float.Epsilon"/></c>.
        /// </summary>
        /// <param name="value">The value to decrement.</param>
        /// <returns>
        /// The next smaller <see cref="float"/> value, or
        /// <paramref name="value"/> if not representable.
        /// </returns>
        public static float NextDown(float value)
        {
            if(float.IsNaN(value) ||
                value == float.PositiveInfinity ||
                value == float.NegativeInfinity)
            {
                return value;
            }
            if(value == 0f) { return -float.Epsilon; }

            int bits = BitConverter.SingleToInt32Bits(value);
            if(value > 0) { --bits; }
            else { ++bits; }
            return BitConverter.Int32BitsToSingle(bits);
        }

        /// <summary>
        /// Returns the next representable <see cref="double"/> value greater
        /// than the specified <paramref name="value"/>.
        /// If <paramref name="value"/> is NaN, positive infinity, or negative
        /// infinity, returns <paramref name="value"/>.
        /// If <paramref name="value"/> is zero, returns
        /// <see cref="double.Epsilon"/>.
        /// </summary>
        /// <param name="value">The value to increment.</param>
        /// <returns>
        /// The next greater <see cref="double"/> value, or
        /// <paramref name="value"/> if not representable.
        /// </returns>
        public static double NextUp(double value)
        {
            if(double.IsNaN(value) ||
                value == double.PositiveInfinity ||
                value == double.NegativeInfinity)
            {
                return value;
            }
            if(value == 0d) { return double.Epsilon; }

            long bits = BitConverter.DoubleToInt64Bits(value);
            if(value > 0) { ++bits; }
            else { --bits; }
            return BitConverter.Int64BitsToDouble(bits);
        }

        /// <summary>
        /// Returns the next representable <see cref="double"/> value less than
        /// the specified <paramref name="value"/>.
        /// If <paramref name="value"/> is NaN, positive infinity, or negative
        /// infinity, returns <paramref name="value"/>.
        /// If <paramref name="value"/> is zero, returns
        /// <c>-<see cref="double.Epsilon"/></c>.
        /// </summary>
        /// <param name="value">The value to decrement.</param>
        /// <returns>
        /// The next smaller <see cref="double"/> value, or
        /// <paramref name="value"/> if not representable.
        /// </returns>
        public static double NextDown(double value)
        {
            if(double.IsNaN(value) ||
                value == double.PositiveInfinity ||
                value == double.NegativeInfinity)
            {
                return value; 
            }
            if(value == 0d) { return -double.Epsilon; }

            long bits = BitConverter.DoubleToInt64Bits(value);
            if(value > 0) { --bits; }
            else { ++bits; }
            return BitConverter.Int64BitsToDouble(bits);
        }

        #endregion Floating Point
    }
}
