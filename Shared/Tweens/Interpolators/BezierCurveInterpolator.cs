/*------------------------------------------------------------------------------
File:       BezierCurveInterpolator.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for interpolations using a Bezier Curve.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-12 00:48:47 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Math;
using System.Collections.Generic;
using System.Numerics;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Abstract base for interpolations using the Bezier Curve.
    /// </summary>
    /// <typeparam name="TType">Type to be interpolated.</typeparam>
    public abstract class BezierCurveInterpolator<TType> : IInterpolator<TType>
    {
        #region Fields

        /// <summary>
        /// Intermediary storage for the Nodes property.
        /// </summary>
        private List<TType> _nodes;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Tracks the currently configured node count setting.
        /// </summary>
        public int NodeCount { get; protected set; } = int.MinValue;

        /// <summary>
        /// List of nodes to be used to generate the Bezier curve.
        /// </summary>
        public IList<TType> Nodes
        {
            get { return _nodes.AsReadOnly(); }
            set 
            {
                _nodes = new List<TType>(value);
                RebuildNodes();
            }
        }

        /// <summary>
        /// List of Pascal's Triangle values for a given row of the triangle.
        /// </summary>
        protected IReadOnlyList<BigInteger> PascalTriangleRow { get; set; }

        /// <summary>
        /// A permanent list for generating multipliers for interpolation.
        /// </summary>
        protected List<float> TempMults { get; set; } = new();

        #endregion Properties

        #region Methods

        /// <summary>
        /// Parameterless constructor for the BezierCurveInterpolator class.
        /// </summary>
        public BezierCurveInterpolator()
        {
            Nodes = new List<TType>();
        }

        /// <summary>
        /// Parameterized constructor for the BezierCurveInterpolator class.
        /// </summary>
        /// <param name="nodes">
        /// Reference to the list of nodes for generating the Bezier curve.
        /// </param>
        public BezierCurveInterpolator(IList<TType> nodes)
        {
            Nodes = nodes;
        }

        /// <summary>
        /// Generates the multipliers for node interpolation.
        /// </summary>
        /// <param name="prog">Progress from 0.</param>
        /// <param name="invProg">The opposite progression from 1.</param>
        protected virtual void GenerateInterpolationMultipliers(float prog,
            float invProg)
        {
            TempMults[0] = (float)PascalTriangleRow[0];
            var mult = prog;
            for(var index = 1; index < NodeCount; ++index, mult *= prog)
            {
                TempMults[index] = (float)PascalTriangleRow[index];
                TempMults[index] *= mult;
            }
            mult = invProg;
            for(var index = 2; index <= NodeCount; ++index, mult *= invProg)
            {
                TempMults[^index] *= mult;
            }
        }

        /// <summary>
        /// Rebuilds TempMults and PascalTriangleRow to cover the current Nodes
        /// size for safe interpolation calculations.
        /// </summary>
        protected virtual void RebuildNodes()
        {
            NodeCount = Nodes.Count;
            if(NodeCount == 0) { return; }

            if(TempMults.Count < NodeCount)
            {
                for(var iter = TempMults.Count; iter < NodeCount; ++iter)
                {
                    TempMults.Add(0);
                }
            }
            PascalTriangleRow = PascalsTriangle.GetRow(NodeCount - 1);
        }

        /// <summary>
        /// Required function that adds the node's value to the result.
        /// </summary>
        /// <param name="result">TType value to be adjusted.</param>
        /// <param name="node">TType value to be added.</param>
        protected abstract void AddTo(ref TType result, TType node);

        /// <summary>
        /// Required function that multiplies the result by a fractional value.
        /// </summary>
        /// <param name="node">TType value to be multiplied.</param>
        /// <param name="progress">Value to be multiplied by.</param>
        protected abstract TType MultiplyBy(TType node, float progress);

        #endregion Methods

        #region IInterpolator Implementation

        /// <inheritdoc />
        public virtual TType Interpolate(float progress)
        {
            if(Nodes.Count != NodeCount) { RebuildNodes(); }
            if(NodeCount == 0) { return default; }

            GenerateInterpolationMultipliers(progress, 1 - progress);

            TType result = MultiplyBy(Nodes[0], TempMults[0]);
            for(var index = 1; index < NodeCount; ++index)
            {
                AddTo(ref result, MultiplyBy(Nodes[index], TempMults[index]));
            }
            return result;
        }

        #endregion IInterpolator Implementation
    }
}