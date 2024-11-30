/*------------------------------------------------------------------------------
  File:           NullCheckTests_Shared.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Unit tests for the NullCheck attribute.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-11-29 20:48:48 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    public partial class NullCheckTests
    {
        #region Members

        /// <summary>Handle used to access GameObjects for testing purposes.</summary>
        private NullCheckTestSO _testObjects;

        #endregion Members

        #region Methods

        [SetUp]
        public void Setup()
        {
            // Arrange
            _testObjects = Resources.Load<NullCheckTestSO>("Helpers/NullCheck/NullCheckTestSO");
        }

        [TearDown]
        public void TearDown()
        {
            Resources.UnloadAsset(_testObjects);
        }

        #endregion Methods
    }
}