/*------------------------------------------------------------------------------
File:       PascalsTriangle.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a singleton class for the generation and storage of 
            Pascal's Triangle, with accessors to specific rows and values
            (see binomial coefficients).
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-02-19 03:05:11 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Helpers;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace AlchemicalFlux.Utilities.Math
{
    /// <summary>
    /// Singleton class for the generation and storage of Pascals Triangle, with
    /// accessors to specific rows and values (see binomial coefficient).
    /// </summary>
    public sealed class PascalsTriangle : Singleton<PascalsTriangle>
    {
        #region Fields

        /// <summary>
        /// Holds the generated rows of a Pascal triangle.
        /// Designed to only hold unique values (left half of the row).
        /// </summary>
        private List<BigInteger[]> _halfRows;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Singleton designed constructor.
        /// </summary>
        private PascalsTriangle()
        {
            _halfRows = new() { new BigInteger[] { 1 } };
        }

        #region Accessors

        /// <summary>
        /// Gets the associated digit in Pascals Triangle (see binomial 
        /// coefficients for uses).
        /// </summary>
        /// <param name="row">0-based row to be accessed.</param>
        /// <param name="col">0-based col to be accessed [0, row].</param>
        /// <returns>The appropriate value from Pascal's Triangle.</returns>
        static public BigInteger GetValue(int row, int col) 
        { 
            return Get.GetValueImpl(row, col); 
        }

        /// <summary>
        /// Gets a full row from Pascal's Triangle.
        /// </summary>
        /// <param name="row">0-based row index to be returned.</param>
        /// <returns>The full row from Pascal's Triangle.</returns>
        static public ReadOnlySpan<BigInteger> GetRow(int row)
        {
            return Get.GetRowImpl(row);
        }

        /// <summary>
        /// Gets the left-portion of Pascal's Triangle for a given row.
        /// </summary>
        /// <param name="row">0-based row index to be returned.</param>
        /// <returns>The left-portion of a row from Pascal's Triangle.</returns>
        static public ReadOnlySpan<BigInteger> GetUniquesOnlyRow(int row)
        {
            return Get.GetHalfRow(row);
        }

        #endregion Accessors

        #region Implementations

        /// <summary>
        /// Implementation to access a given value from Pascal's Triangle. 
        /// See binomial coefficients from more usages.
        /// </summary>
        /// <param name="row">0-based row to be accessed.</param>
        /// <param name="col">0-based col to be accessed [0 - row].</param>
        /// <returns>The appropriate value from Pascal's Triangle.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Error thrown when col is outside range of [0, row].
        /// </exception>
        private BigInteger GetValueImpl(int row, int col)
        {
            if(col < 0 || col > row)
            {
                throw new ArgumentOutOfRangeException(
                    $"Col({col}) must be between 0 and {row}");
            }
            var curRow = GetHalfRow(row);
            if(col > (row >> 1)) { col = row - col; }
            return curRow[col];
        }

        /// <summary>
        /// Implementation to access a full row from Pascal's Triangle.
        /// </summary>
        /// <param name="row">0-based row index to be returned.</param>
        /// <returns>The full row from Pascal's Triangle.</returns>
        private ReadOnlySpan<BigInteger> GetRowImpl(int row)
        {
            var curRow = GetHalfRow(row);
            var expanded = new BigInteger[row + 1];
            row >>= 1;
            for(var index = 0; index <= row; ++index)
            {
                expanded[^(index + 1)] = expanded[index] = curRow[index];
            }
            return expanded;
        }

        /// <summary>
        /// Implementation to access the left-portion of Pascal's Triangle for a
        /// given row.
        /// </summary>
        /// <param name="row">0-based row index to be returned.</param>
        /// <returns>The left-portion of a row from Pascal's Triangle.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Error thrown when row is less than 0.
        /// </exception>
        private ReadOnlySpan<BigInteger> GetHalfRow(int row)
        {
            if(row < 0)
            {
                throw new ArgumentException("Row index must be non-negative.");
            }
            GenerateHalfRows(row);
            return _halfRows[row];
        }

        /// <summary>
        /// Creates any ungenerated rows up to and including the requested row
        /// index.
        /// </summary>
        /// <param name="requestedRow">
        /// Last row to be generated, if necessary.
        /// </param>
        private void GenerateHalfRows(int requestedRow)
        {
            if(_halfRows.Count > requestedRow) { return; }

            var prevRow = _halfRows[^1];
            var evenRow = (_halfRows.Count & 1) == 0;
            var length = prevRow.Length;
            for(var row = _halfRows.Count; row <= requestedRow; 
                ++row, evenRow = !evenRow, length = prevRow.Length)
            {
                var curRow = new BigInteger[length + (evenRow ? 1 : 0)];
                curRow[0] = 1;
                for(var index = 1; index < length; ++index)
                {
                    curRow[index] = prevRow[index - 1] + prevRow[index];
                }
                if(evenRow) { curRow[^1] = prevRow[^1] << 1; }

                _halfRows.Add(curRow);
                prevRow = curRow;
            }
        }

        #endregion Implementations

        #endregion Methods
    }
}
