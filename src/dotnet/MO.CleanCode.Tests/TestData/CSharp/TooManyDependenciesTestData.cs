using System;

namespace CleanCode.Tests.TestData.CSharp
{
    // This class should trigger TooManyDependencies warning (5 dependencies > default 4)
    public class ClassWithTooManyDependencies
    {
        public ClassWithTooManyDependencies(
            IService1 service1,
            IService2 service2,
            IService3 service3,
            IService4 service4,
            IService5 service5)
        {
        }
    }

    // This class should NOT trigger warning (4 dependencies = default limit)
    public class ClassWithAcceptableDependencies
    {
        public ClassWithAcceptableDependencies(
            IService1 service1,
            IService2 service2,
            IService3 service3,
            IService4 service4)
        {
        }
    }

    // This class should NOT trigger warning (concrete types don't count)
    public class ClassWithConcreteTypes
    {
        public ClassWithConcreteTypes(
            ConcreteService1 service1,
            ConcreteService2 service2,
            ConcreteService3 service3,
            ConcreteService4 service4,
            ConcreteService5 service5,
            ConcreteService6 service6)
        {
        }
    }

    // Mixed dependencies - only interfaces should count
    public class ClassWithMixedDependencies
    {
        public ClassWithMixedDependencies(
            IService1 service1,
            IService2 service2,
            ConcreteService1 concrete1,
            IService3 service3,
            string simpleParam,
            IService4 service4,
            IService5 service5) // 5 interfaces > 4 limit
        {
        }
    }

    // Empty constructor should not trigger warning
    public class ClassWithNoConstructor
    {
    }

    // Interface definitions for test
    public interface IService1 { }
    public interface IService2 { }
    public interface IService3 { }
    public interface IService4 { }
    public interface IService5 { }

    // Concrete class definitions for test
    public class ConcreteService1 { }
    public class ConcreteService2 { }
    public class ConcreteService3 { }
    public class ConcreteService4 { }
    public class ConcreteService5 { }
    public class ConcreteService6 { }
}