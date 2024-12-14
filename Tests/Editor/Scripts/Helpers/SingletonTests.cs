/*------------------------------------------------------------------------------
  File:           SingletonTests.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Test cases for the generic abstract Singleton class.
  Copyright:      2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-12-14 11:48:55 
------------------------------------------------------------------------------*/
using NUnit.Framework;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    using SEM = SingletonErrorMessages;

    public class SingletonTests
    {
        #region Test Cases

        [Test]
        public void Singleton_LazyInitialization_WrappedReturnsSameInstance()
        {
            // Act
            var instance1 = WrappedSingleton.Get;
            var instance2 = WrappedSingleton.Get;

            // Assert
            Assert.AreSame(instance1, instance2);
        }

        [Test]
        public void Singleton_LazyInitialization_ReturnsSameInstance()
        {
            // Act
            var instance1 = Singleton<UnwrappedSingleton>.Get;
            var instance2 = Singleton<UnwrappedSingleton>.Get;

            // Assert
            Assert.AreSame(instance1, instance2);
        }

        [Test]
        public void Singleton_ThrowsException_IfClassIsUnsealed()
        {
            // Assemble
            var name = typeof(UnsealedSingleton).Name;
            var expectedResult = SEM.SealedErrMsg(name);

            // Assert
            Assert.That(() => Singleton<UnsealedSingleton>.Get,
                Throws.InvalidOperationException.With.Message.Contains(expectedResult));
        }


        [Test]
        public void Singleton_ThrowsException_IfMultipleConstructors()
        {
            // Assemble
            var name = typeof(SingletonWithMultipleConstructors).Name;
            var length = 2;
            var expectedResult = SEM.TooManyConstructorsErrMsg(name, length);

            // Assert
            Assert.That(() => Singleton<SingletonWithMultipleConstructors>.Get,
                Throws.InvalidOperationException.With.Message.Contains(expectedResult));
        }

        [Test]
        public void Singleton_ThrowsException_IfConstructorHasParameters()
        {
            // Assemble
            var name = typeof(SingletonWithParameteredConstuctor).Name;
            var expectedResult = SEM.TooManyParametersErrMsg(name);

            // Assert
            Assert.That(() => Singleton<SingletonWithParameteredConstuctor>.Get,
                Throws.InvalidOperationException.With.Message.Contains(expectedResult));
        }

        [Test]
        public void Singleton_ThrowsException_IfConstructorIsNotPrivate()
        {
            // Assemble
            var name = typeof(SingletonWithPublicConstructor).Name;
            var expectedResult = SEM.PublicConstructorErrMsg(name);

            // Assert
            Assert.That(() => Singleton<SingletonWithPublicConstructor>.Get,
                Throws.InvalidOperationException.With.Message.Contains(expectedResult));
        }

        [Test]
        public void Singleton_ThrowsException_IfMethodHasSingletonReturn()
        {
            // Assemble
            var name = typeof(SingletonWithMethodReturningInstance).Name;
            var method = "TestMethod";
            var expectedResult = SEM.MethodReturnErrMsg(name, method);

            // Assert
            Assert.That(() => Singleton<SingletonWithMethodReturningInstance>.Get,
                Throws.ArgumentException.With.Message.Contains(expectedResult));
        }

        [Test]
        public void Singleton_ThrowsException_IfMethodHasSingletonParameter()
        {
            // Assemble
            var name = typeof(SingletonWithMethodWithInstanceParameter).Name;
            var method = "TestMethod";
            var expectedResult = SEM.MethodParameterErrMsg(name, method);

            // Assert
            Assert.That(() => Singleton<SingletonWithMethodWithInstanceParameter>.Get,
                Throws.ArgumentException.With.Message.Contains(expectedResult));
        }

        [Test]
        public void Singleton_ThrowsException_IfGetterIsSingletonInstance()
        {
            // Assemble
            var name = typeof(SingletonWithGetterInstanceProperty).Name;
            var method = "get_Test";
            var expectedResult = SEM.MethodReturnErrMsg(name, method);

            // Assert
            Assert.That(() => Singleton<SingletonWithGetterInstanceProperty>.Get,
                Throws.ArgumentException.With.Message.Contains(expectedResult));
        }

        [Test]
        public void Singleton_ThrowsException_IfSetterIsSingletonInstance()
        {
            // Assemble
            var name = typeof(SingletonWithSetterInstanceProperty).Name;
            var method = "set_Test";
            var expectedResult = SEM.MethodParameterErrMsg(name, method);

            // Assert
            Assert.That(() => Singleton<SingletonWithSetterInstanceProperty>.Get,
                Throws.ArgumentException.With.Message.Contains(expectedResult));
        }

        [Test]
        public void Singleton_ThrowsException_IfExpressionIsSingletonInstance()
        {
            // Assemble
            var name = typeof(SingletonWithExpressionInstanceProperty).Name;
            var method = "get_Test";
            var expectedResult = SEM.MethodReturnErrMsg(name, method);

            // Assert
            Assert.That(() => Singleton<SingletonWithExpressionInstanceProperty>.Get,
                Throws.ArgumentException.With.Message.Contains(expectedResult));
        }
        
        [Test]
        public void Singleton_ThrowsException_IfFieldIsSingletonInstance()
        {
            // Assemble
            var name = typeof(SingletonWithFieldInstance).Name;
            var field = "Test";
            var expectedResult = SEM.FieldErrMsg(name, field);

            // Assert
            Assert.That(() => Singleton<SingletonWithFieldInstance>.Get,
                Throws.ArgumentException.With.Message.Contains(expectedResult));
        }

        #endregion Test Cases

        #region Test Types

        public sealed class WrappedSingleton : Singleton<WrappedSingleton>
        {
            private WrappedSingleton() { }
        }

        public sealed class UnwrappedSingleton
        {
            private UnwrappedSingleton() { }
        }

        public class UnsealedSingleton
        {
            private UnsealedSingleton() { }
        }

        public sealed class SingletonWithParameteredConstuctor
        {
            private SingletonWithParameteredConstuctor(string param) { }
        }

        public sealed class SingletonWithPublicConstructor 
        {
            public SingletonWithPublicConstructor() { }
        }

        public sealed class SingletonWithMultipleConstructors
        {
            private SingletonWithMultipleConstructors() { }
            public SingletonWithMultipleConstructors(string param) { }
        }

        public sealed class SingletonWithMethodReturningInstance
        {
            private SingletonWithMethodReturningInstance() { }
            public SingletonWithMethodReturningInstance TestMethod()
            {
                return new SingletonWithMethodReturningInstance();
            }
        }

        public sealed class SingletonWithMethodWithInstanceParameter
        {
            private SingletonWithMethodWithInstanceParameter() { }
            public void TestMethod(SingletonWithMethodWithInstanceParameter param) { }
        }

        public sealed class SingletonWithGetterInstanceProperty
        {
            private SingletonWithGetterInstanceProperty _test;
            private SingletonWithGetterInstanceProperty() { }
            private SingletonWithGetterInstanceProperty Test { get { return _test; } }
        }

        public sealed class SingletonWithSetterInstanceProperty
        {
            private SingletonWithSetterInstanceProperty _test;
            private SingletonWithSetterInstanceProperty() { }
            private SingletonWithSetterInstanceProperty Test { set { _test = value; } }
        }

        public sealed class SingletonWithExpressionInstanceProperty
        {
            private SingletonWithExpressionInstanceProperty() { }
            public SingletonWithExpressionInstanceProperty Test => new();
        }

        public sealed class SingletonWithFieldInstance
        {
            private SingletonWithFieldInstance() { }
            public SingletonWithFieldInstance Test;
        }

        #endregion Test Types
    }
}
