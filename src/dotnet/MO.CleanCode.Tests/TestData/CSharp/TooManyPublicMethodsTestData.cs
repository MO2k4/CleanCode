using System;

namespace CleanCode.Tests.TestData.CSharp
{
    // This class should trigger TooManyPublicMethods warning (16 public methods > default 15)
    public class ClassWithTooManyPublicMethods
    {
        public void Method1() { }
        public void Method2() { }
        public void Method3() { }
        public void Method4() { }
        public void Method5() { }
        public void Method6() { }
        public void Method7() { }
        public void Method8() { }
        public void Method9() { }
        public void Method10() { }
        public void Method11() { }
        public void Method12() { }
        public void Method13() { }
        public void Method14() { }
        public void Method15() { }
        public void Method16() { }  // This puts it over the limit

        // Private methods should not count towards public method limit
        private void PrivateMethod1() { }
        private void PrivateMethod2() { }
        private void PrivateMethod3() { }

        // Protected methods should not count towards public method limit
        protected void ProtectedMethod1() { }
        protected void ProtectedMethod2() { }

        // Internal methods should not count towards public method limit
        internal void InternalMethod1() { }
        internal void InternalMethod2() { }

        // Properties should not count as methods
        public string Property1 { get; set; }
        public int Property2 { get; set; }
        public bool Property3 { get; set; }
    }

    // This class should NOT trigger warning (15 public methods = default limit)
    public class ClassWithAcceptablePublicMethodCount
    {
        public void Method1() { }
        public void Method2() { }
        public void Method3() { }
        public void Method4() { }
        public void Method5() { }
        public void Method6() { }
        public void Method7() { }
        public void Method8() { }
        public void Method9() { }
        public void Method10() { }
        public void Method11() { }
        public void Method12() { }
        public void Method13() { }
        public void Method14() { }
        public void Method15() { }  // Exactly at the limit

        // Non-public methods don't count
        private void PrivateMethod() { }
        protected void ProtectedMethod() { }
        internal void InternalMethod() { }
    }

    // This class should NOT trigger warning (small class)
    public class SmallPublicMethodsClass
    {
        public void Method1() { }
        public void Method2() { }
        public void Method3() { }

        private void PrivateMethod() { }
    }

    // This class should NOT trigger warning (no public methods)
    public class ClassWithNoPublicMethods
    {
        private void PrivateMethod1() { }
        private void PrivateMethod2() { }
        protected void ProtectedMethod1() { }
        protected void ProtectedMethod2() { }
        internal void InternalMethod1() { }
        internal void InternalMethod2() { }
    }

    // This class should trigger warning with static public methods included
    public class ClassWithMixedPublicMethods
    {
        public void Instance1() { }
        public void Instance2() { }
        public void Instance3() { }
        public void Instance4() { }
        public void Instance5() { }
        public void Instance6() { }
        public void Instance7() { }
        public void Instance8() { }
        public void Instance9() { }
        public void Instance10() { }
        public void Instance11() { }
        public void Instance12() { }
        public void Instance13() { }
        public void Instance14() { }
        public void Instance15() { }
        public void Instance16() { }  // Over the limit

        public static void Static1() { }
        public static void Static2() { }
        public static void Static3() { }

        private void Private1() { }
        private static void PrivateStatic1() { }
    }

    // Abstract class should be checked
    public abstract class AbstractClassWithManyPublicMethods
    {
        public void Method1() { }
        public void Method2() { }
        public void Method3() { }
        public void Method4() { }
        public void Method5() { }
        public void Method6() { }
        public void Method7() { }
        public void Method8() { }
        public void Method9() { }
        public void Method10() { }
        public void Method11() { }
        public void Method12() { }
        public void Method13() { }
        public void Method14() { }
        public void Method15() { }
        public void Method16() { }  // Over the limit

        public abstract void AbstractMethod();
        protected abstract void ProtectedAbstractMethod();
    }

    // Empty class should not trigger warning
    public class EmptyPublicMethodsClass
    {
    }

    // Class with only properties should not trigger warning
    public class ClassWithOnlyProperties
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public decimal Amount { get; set; }
        public Guid Id { get; set; }
        public string Description { get; set; }
        public int Count { get; set; }
        public bool IsEnabled { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsDeleted { get; set; }
        // Even with many properties, no public methods means no violation
    }

    // Interface should be checked if the analyzer covers interfaces
    public interface IInterfaceWithManyMethods
    {
        void Method1();
        void Method2();
        void Method3();
        void Method4();
        void Method5();
        void Method6();
        void Method7();
        void Method8();
        void Method9();
        void Method10();
        void Method11();
        void Method12();
        void Method13();
        void Method14();
        void Method15();
        void Method16();  // Over the limit if interfaces are checked
    }

    // Static class should be checked
    public static class StaticClassWithManyPublicMethods
    {
        public static void Method1() { }
        public static void Method2() { }
        public static void Method3() { }
        public static void Method4() { }
        public static void Method5() { }
        public static void Method6() { }
        public static void Method7() { }
        public static void Method8() { }
        public static void Method9() { }
        public static void Method10() { }
        public static void Method11() { }
        public static void Method12() { }
        public static void Method13() { }
        public static void Method14() { }
        public static void Method15() { }
        public static void Method16() { }  // Over the limit

        // Private static methods shouldn't count
        private static void PrivateStatic1() { }
        private static void PrivateStatic2() { }
    }
}