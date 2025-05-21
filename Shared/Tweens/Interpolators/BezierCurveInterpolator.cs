/*------------------------------------------------------------------------------
File:       BezierCurveInterpolator.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for interpolations using a Bezier Curve.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-21 08:02:05 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Abstract base class for interpolations using a Bezier curve.
    /// Provides the structure for interpolating between a set of nodes
    /// of type <typeparamref name="TType"/> using a Bezier curve algorithm.
    /// </summary>
    /// <typeparam name="TType">Type to be interpolated.</typeparam>
    public abstract class BezierCurveInterpolator<TType>
        : InterpolatorBase<TType>
    {
        #region Fields

        /// <summary>
        /// Intermediary storage for the <see cref="Nodes"/> property.
        /// </summary>
        private List<TType> _nodes;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the list of nodes to be used to generate the Bezier
        /// curve. Setting this property will rebuild the internal node
        /// structure.
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

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="BezierCurveInterpolator{TType}"/> class with an empty
        /// node list.
        /// </summary>
        public BezierCurveInterpolator()
        {
            Nodes = new List<TType>();
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="BezierCurveInterpolator{TType}"/> class with the
        /// specified list of nodes.
        /// </summary>
        /// <param name="nodes">
        /// Reference to the list of nodes for generating the Bezier curve.
        /// </param>
        public BezierCurveInterpolator(IList<TType> nodes)
        {
            Nodes = nodes;
        }

        #region Overrides

        /// <inheritdoc />
        protected override bool CheckAndLimitProgress(
            ref float progress,
            ref TType failValue)
        {
            base.CheckAndLimitProgress(ref progress, ref failValue);

            if(Nodes.Count == 0)
            {
                throw new System.InvalidOperationException(
                    $"{nameof(Nodes.Count)} cannot be zero.");
            }
            else if(Nodes.Count == 1)
            {
                failValue = Nodes[0];
                return false;
            }

            progress = Mathf.Clamp01(progress);
            failValue = GetDefault();
            return true;
        }

        #endregion Overrides

        /// <summary>
        /// Rebuilds internal data structures to match the current
        /// <see cref="Nodes"/> size.
        /// </summary>
        protected virtual void RebuildNodes()
        {
        }

        #endregion Methods
    }
}
