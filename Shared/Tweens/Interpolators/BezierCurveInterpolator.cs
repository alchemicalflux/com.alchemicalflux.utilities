/*------------------------------------------------------------------------------
File:       BezierCurveInterpolator.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for interpolations using a Bezier Curve.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-02-23 23:00:22 
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

        private List<float> _tempMults = new();
        private IReadOnlyList<BigInteger> _pascalTriangleRow;
        private int _nodeCount = int.MinValue;

        #endregion Fields

        #region Properties

        public IList<TType> Nodes { get; set; }

        #endregion Properties

        #region Methods

        protected BezierCurveInterpolator()
        {
        }

        public BezierCurveInterpolator(IList<TType> nodes) : base()
        {
            Nodes = nodes;
        }

        public virtual TType Interpolate(float progress)
        {
            if(Nodes.Count != _nodeCount) { RebuildNodes(); }

            var invProg = 1 - progress;
            var mult = 1f;
            for(var index = 0; index < _nodeCount; ++index, mult *= progress)
            {
                _tempMults[index] = mult;
            }
            mult = invProg;
            for(var index = 2; index <= _nodeCount; ++index, mult *= invProg)
            {
                _tempMults[^index] *= mult;
            }

            TType result = default;
            for(var index = 0; index < _nodeCount; ++index)
            {
                _tempMults[index] *= (float)_pascalTriangleRow[index];
                AddTo(ref result, MultiplyBy(Nodes[index], _tempMults[index]));
            }
            return result;
        }

        public abstract void AddTo(ref TType result, TType node);
        public abstract TType MultiplyBy(TType node, float progress);

        protected void RebuildNodes()
        {
            var count = Nodes.Count;
            if(_tempMults.Count < count)
            {
                for(var iter = _tempMults.Count; iter < count; ++iter)
                {
                    _tempMults.Add(0);
                }
            }
            _pascalTriangleRow = PascalsTriangle.GetRow(count - 1);
            _nodeCount = count;
        }

        #endregion Methods
    }
}