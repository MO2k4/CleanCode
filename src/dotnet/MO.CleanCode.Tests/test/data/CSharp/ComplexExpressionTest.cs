using System;

public class TestClass
{
    // This method should trigger complex expression highlighting
    public bool ComplexCondition(int a, int b, int c, string text, bool flag)
    {
        // Complex condition with many logical operators
        if (
            (a > 0 && b < 10)
            || (c == 5 && text != null && text.Length > 0)
            || (flag && a + b > c)
            || (!flag && a * b < c * 2)
        )
        {
            return true;
        }
        return false;
    }

    // This should be fine
    public bool SimpleCondition(int a, int b)
    {
        if (a > b)
        {
            return true;
        }
        return false;
    }
}
