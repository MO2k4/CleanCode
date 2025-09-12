using System;

// This class should trigger too many public methods highlighting
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

    public void Method16() { }

    public void Method17() { }

    public void Method18() { }

    public void Method19() { }

    public void Method20() { }

    // Private methods don't count
    private void PrivateMethod1() { }

    private void PrivateMethod2() { }
}

// This should be fine
public class ClassWithFewPublicMethods
{
    public void Method1() { }

    public void Method2() { }

    private void PrivateMethod1() { }

    private void PrivateMethod2() { }
}
