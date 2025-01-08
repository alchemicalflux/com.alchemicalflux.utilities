/*------------------------------------------------------------------------------
File:       NullCheckTests_Shared.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for the NullCheck attribute.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 17:05:53 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    public partial class NullCheckTests
    {
        #region Members

        /// <summary>
        /// Handle used to access GameObjects for testing purposes.
        /// </summary>
        private NullCheckTestSO _testObjects;

        #endregion Members

        #region Methods

        [SetUp]
        public void Setup()
        {
            // Arrange
            _testObjects = 
                Resources.Load<NullCheckTestSO>(
                    "Helpers/NullCheck/NullCheckTestSO");
        }

        [TearDown]
        public void TearDown()
        {
            Resources.UnloadAsset(_testObjects);
        }

        #endregion Methods
    }
}