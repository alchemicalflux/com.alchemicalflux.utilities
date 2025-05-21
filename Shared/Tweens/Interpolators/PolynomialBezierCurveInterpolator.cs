/*------------------------------------------------------------------------------
File:       PolynomialBezierCurveInterpolator.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for Bezier curve interpolators using the
            Bernstein polynomial (Pascal's Triangle) formulation.
            This class provides the structure for interpolating between a set of
            nodes of type TType using the polynomial Bezier algorithm.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-21 08:02:05 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Math;
using System.Collections.Generic;
using System.Numerics;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Abstract base class for Bezier curve interpolators using the
    /// Bernstein polynomial (Pascal's Triangle) formulation.
    /// This class provides the structure for interpolating between a set of
    /// nodes of type <typeparamref name="TType"/> using the polynomial Bezier
    /// algorithm.
    /// </summary>
    /// <typeparam name="TType">Type to be interpolated.</typeparam>
    public abstract class PolynomialBezierCurveInterpolator<TType>
        : BezierCurveInterpolator<TType>
    {
        #region Properties

        /// <summary>
        /// Gets the list of Pascal's Triangle values for the current row,
        /// used as binomial coefficients in the Bezier polynomial.
        /// </summary>
        protected IReadOnlyList<BigInteger> PascalTriangleRow { get; set; }

        /// <summary>
        /// Gets the list of multipliers for each node, generated for the
        /// current interpolation.
        /// </summary>
        protected List<float> TempMults { get; set; } = new();

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="PolynomialBezierCurveInterpolator{TType}"/> class with an
        /// empty node list.
        /// </summary>
        public PolynomialBezierCurveInterpolator() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="PolynomialBezierCurveInterpolator{TType}"/> class with
        /// the specified list of nodes.
        /// </summary>
        /// <param name="nodes">
        /// Reference to the list of nodes for generating the Bezier curve.
        /// </param>
        public PolynomialBezierCurveInterpolator(IList<TType> nodes)
            : base(nodes)
        {
        }

        #region Overrides

        /// <inheritdoc />
        protected override void RebuildNodes()
        {
            if(TempMults.Count < Nodes.Count)
            {
                for(var iter = TempMults.Count; iter < Nodes.Count; ++iter)
                {
                    TempMults.Add(0);
                }
            }
            PascalTriangleRow = PascalsTriangle.GetRow(Nodes.Count - 1);
        }

        /// <inheritdoc />
        protected override TType ProcessInterpolation(float progress)
        {
            GenerateInterpolationMultipliers(progress, 1 - progress);

            TType result = MultiplyBy(Nodes[0], TempMults[0]);
            for(var index = 1; index < Nodes.Count; ++index)
            {
                AddTo(ref result, MultiplyBy(Nodes[index], TempMults[index]));
            }
            return result;
        }

        #endregion Overrides

        /// <summary>
        /// Generates the multipliers for node interpolation using the Bernstein
        /// polynomial.
        /// Each multiplier is the product of the binomial coefficient, the
        /// appropriate power of <paramref name="prog"/>, and the appropriate
        /// power of <paramref name="invProg"/>.
        /// </summary>
        /// <param name="prog">Progress value from 0 to 1.</param>
        /// <param name="invProg">The complement of progress (1 - prog).</param>
        protected virtual void GenerateInterpolationMultipliers(
            float prog,
            float invProg)
        {
            TempMults[0] = (float)PascalTriangleRow[0];
            var mult = prog;
            for(var index = 1; index < Nodes.Count; ++index, mult *= prog)
            {
                TempMults[index] = (float)PascalTriangleRow[index];
                TempMults[index] *= mult;
            }
            mult = invProg;
            for(var index = 2; index <= Nodes.Count; ++index, mult *= invProg)
            {
                TempMults[^index] *= mult;
            }
        }

        /// <summary>
        /// Adds the node's value to the result.
        /// Must be implemented by derived classes to define addition for
        /// <typeparamref name="TType"/>.
        /// </summary>
        /// <param name="result">
        /// Reference to the value being accumulated.
        /// </param>
        /// <param name="node">Node value to add.</param>
        protected abstract void AddTo(ref TType result, TType node);

        /// <summary>
        /// Multiplies the node's value by a fractional progress value.
        /// Must be implemented by derived classes to define scaling for
        /// <typeparamref name="TType"/>.
        /// </summary>
        /// <param name="node">Node value to scale.</param>
        /// <param name="progress">Fractional progress value.</param>
        /// <returns>The scaled node value.</returns>
        protected abstract TType MultiplyBy(TType node, float progress);

        #endregion Methods
    }
}
