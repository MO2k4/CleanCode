using System;

namespace CleanCode.Tests.TestData.CSharp
{
    // This class should trigger ClassTooBig warning (21 methods > default 20)
    public class ClassWithTooManyMethods
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
        public void Method16() { }
        public void Method17() { }
        public void Method18() { }
        public void Method19() { }
        public void Method20() { }
        public void Method21() { } // This puts it over the limit
    }

    // This class should NOT trigger warning (20 methods = default limit)
    public class ClassWithAcceptableMethodCount
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
        public void Method16() { }
        public void Method17() { }
        public void Method18() { }
        public void Method19() { }
        public void Method20() { }
    }

    // This class should NOT trigger warning (small class)
    public class SmallClass
    {
        public void Method1() { }
        public void Method2() { }
        public void Method3() { }
    }

    // Properties and fields should not count towards method limit
    public class ClassWithMixedMembers
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        private int field1;
        private int field2;

        public void Method1() { }
        public void Method2() { }
        public void Method3() { }
        // Only 3 methods, should not trigger
    }

    // Empty class should not trigger warning
    public class EmptyClass
    {
    }
}