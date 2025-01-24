/*------------------------------------------------------------------------------
File:       ReflectionUtilityTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Test cases for the ReflectionUtility class.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-23 21:02:40 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    [TestFixture]
    public class ReflectionUtilityTests
    {
        #region Methods

        [Test]
        public void GetFieldsWithAttribute_SimpleClass_ReturnsCorrectFields()
        {
            // Arrange
            var testObject = new TestClass();
            
            // Act
            var result = ReflectionUtility
                .GetFieldsWithAttribute<StubAttribute>(testObject);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].field.Name, Is.EqualTo("TestField"));
        }

        [Test]
        public void GetFieldsWithAttribute_WithIgnoreCheck_IgnoresSpecifiedFields()
        {
            // Arrange
            var testObject = new TestClass();

            // Act
            var result = ReflectionUtility
                .GetFieldsWithAttribute<StubAttribute>(
                    testObject,
                    attributeIgnoreCheck: 
                        (field, obj, attr) => field.Name == "TestField");

            // Assert
            Assert.That(result.Count, Is.EqualTo(0));
        }

        [Test]
        public void GetFieldsWithAttribute_NestedObjects_ReturnsAllMatchingFields()
        {
            // Arrange
            var testObject = new ParentClass();

            // Act
            var result = ReflectionUtility
                .GetFieldsWithAttribute<StubAttribute>(testObject);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Select(r => r.field.Name), 
                Contains.Item("ParentField"));
            Assert.That(result.Select(r => r.field.Name), 
                Contains.Item("ChildField"));
        }

        [Test]
        public void GetFieldsWithAttribute_EnumerableFields_ReturnsMatchingFields()
        {
            // Arrange
            var testObject = new ClassWithEnumerable();

            // Act
            var result = ReflectionUtility
                .GetFieldsWithAttribute<StubAttribute>(testObject);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Select(r => r.field.Name), 
                Contains.Item("ListField"));
            Assert.That(result.Select(r => r.field.Name), 
                Contains.Item("ChildField"));
        }

        [Test]
        public void GetFieldsWithAttribute_WithBindingFlags_ReturnsCorrectFields()
        {
            // Arrange
            var testObject = new ClassWithPrivateField();
            
            // Act
            var result = ReflectionUtility
                .GetFieldsWithAttribute<StubAttribute>(testObject, 
                    BindingFlags.NonPublic | BindingFlags.Instance);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].field.Name, Is.EqualTo("_privateField"));
        }

        [Test]
        public void GetFieldsWithAttribute_SimpleNodeClass_ReturnsCorrectFields()
        {
            // Arrange
            var testObject = new ClassWithNodeField();

            // Act
            var result = ReflectionUtility
                .GetFieldsWithAttribute<StubAttribute>(testObject);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].field.Name, Is.EqualTo("NodeField"));
        }

        [Test]
        public void GetFieldsWithAttribute_MuliNodeClass_ReturnsCorrectFields()
        {
            // Arrange
            var testObject = new ClassWithNodeField()
            {
                NodeField = new ClassWithNodeField()
                {
                    NodeField = new ClassWithNodeField()
                }
            };

            // Act
            var result = ReflectionUtility
                .GetFieldsWithAttribute<StubAttribute>(testObject);

            // Assert
            Assert.That(result.Count, Is.EqualTo(3));
            foreach(var value in result)
            {
                Assert.That(value.field.Name, Is.EqualTo("NodeField"));
            }
        }

        [Test]
        public void GetFieldsWithAttribute_SimpleMixedNodeClass_ReturnsCorrectFields()
        {
            // Arrange
            var testObject = new ClassWithStructWithNodeField();

            // Act
            var result = ReflectionUtility
                .GetFieldsWithAttribute<StubAttribute>(testObject);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.Count(f => f.field.Name == "NodeField"),
                Is.EqualTo(1));
            Assert.That(result.Count(f => f.field.Name == "StructField"),
                Is.EqualTo(1));
        }

        [Test]
        public void GetFieldsWithAttribute_MuliMixedNodeClass_ReturnsCorrectFields()
        {
            // Arrange
            var testObject = new ClassWithStructWithNodeField()
            {
                StructField = new StructWithNodeField()
                {
                    NodeField = new ClassWithStructWithNodeField()
                    {
                        StructField = new StructWithNodeField()
                    }
                }
            };

            // Act
            var result = ReflectionUtility
                .GetFieldsWithAttribute<StubAttribute>(testObject);

            // Assert
            Assert.That(result.Count, Is.EqualTo(4));
            Assert.That(result.Count(f => f.field.Name == "NodeField"),
                Is.EqualTo(2));
            Assert.That(result.Count(f => f.field.Name == "StructField"),
                Is.EqualTo(2));
        }



        #endregion Methods

        #region Test Types

        public class TestClass
        {
            [StubAttribute]
            public object TestField;

            public object NonAttributeField;
        }

        public class ParentClass
        {
            [StubAttribute]
            public object ParentField;

            public ChildClass Child = new ChildClass();
        }

        public class ChildClass
        {
            [StubAttribute]
            public object ChildField;
        }

        public class ClassWithEnumerable
        {
            [StubAttribute]
            public List<ChildClass> ListField = 
                new List<ChildClass> { new ChildClass() };
        }

        public class ClassWithPrivateField
        {
            [StubAttribute]
            private int _privateField;
        }

        public class ClassWithNodeField
        {
            [StubAttribute]
            public ClassWithNodeField NodeField;
        }

        public class ClassWithStructWithNodeField
        {
            [StubAttribute]
            public StructWithNodeField StructField;
        }

        public struct StructWithNodeField
        {
            [StubAttribute]
            public ClassWithStructWithNodeField NodeField;
        }

        public sealed class StubAttribute : Attribute { }

        #endregion Test Types
    }
}