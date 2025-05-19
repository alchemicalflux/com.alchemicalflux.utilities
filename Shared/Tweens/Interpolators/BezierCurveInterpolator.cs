/*------------------------------------------------------------------------------
File:       BezierCurveInterpolator.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for interpolations using a Bezier Curve.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-19 01:27:00 
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
        : IInterpolator<TType>
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

        #region IInterpolator Implementation

        /// <summary>
        /// Generates an interpolated value based on the given progress amount.
        /// </summary>
        /// <param name="progress">
        /// Interpolation value, typically expected to be between [0-1] but not
        /// guaranteed.
        /// </param>
        /// <returns>
        /// A value generated based on the progress amount.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown if <paramref name="progress"/> is NaN.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if the node count is zero.
        /// </exception>
        public TType Interpolate(float progress)
        {
            if(CheckAndLimitProgress(ref progress, out var failValue))
            {
                return failValue;
            }
            return ProcessInterpolation(progress);
        }

        #endregion IInterpolator Implementation

        /// <summary>
        /// Rebuilds internal data structures to match the current
        /// <see cref="Nodes"/> size.
        /// </summary>
        protected virtual void RebuildNodes()
        {
        }

        /// <summary>
        /// Checks and limits the progress value to the [0,1] range.
        /// Handles special cases for NaN progress and invalid node counts.
        /// </summary>
        /// <param name="progress">
        /// Reference to the progress value to check and clamp.
        /// </param>
        /// <param name="failValue">
        /// Output value to use if interpolation cannot proceed (e.g., only one
        /// node).
        /// </param>
        /// <returns>
        /// True if interpolation should return <paramref name="failValue"/>
        /// immediately; otherwise, false.
        /// </returns>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// Thrown if <paramref name="progress"/> is NaN.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if the node count is zero.
        /// </exception>
        protected virtual bool CheckAndLimitProgress(
            ref float progress,
            out TType failValue)
        {
            if(float.IsNaN(progress))
            {
                throw new System.ArgumentOutOfRangeException(
                    nameof(progress), "Progress cannot be NaN.");
            }
            else if(Nodes.Count == 0)
            {
                throw new System.InvalidOperationException(
                    $"{nameof(Nodes.Count)} cannot be zero.");
            }
            else if(Nodes.Count == 1)
            {
                failValue = Nodes[0];
                return true;
            }

            progress = Mathf.Clamp01(progress);
            failValue = default;
            return false;
        }

        /// <summary>
        /// When implemented in a derived class, generates an interpolated value
        /// based on the given progress amount.
        /// </summary>
        /// <param name="progress">
        /// Interpolation value, typically expected to be between [0-1] but not
        /// guaranteed.
        /// </param>
        /// <returns>
        /// A value generated based on the progress amount.
        /// </returns>
        protected abstract TType ProcessInterpolation(float progress);

        #endregion Methods
    }
}
