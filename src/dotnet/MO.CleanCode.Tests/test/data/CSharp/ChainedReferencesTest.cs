using System;

public class TestClass
{
    public object SomeProperty { get; set; }

    // This should trigger chained references highlighting
    public void MethodWithChainedReferences()
    {
        var result = this
            .SomeProperty.ToString()
            .ToLower()
            .Trim()
            .Substring(0, 5)
            .Replace("a", "b")
            .ToUpper();
        Console.WriteLine(result);
    }

    // This should be fine
    public void MethodWithShortChain()
    {
        var result = this.SomeProperty.ToString();
        Console.WriteLine(result);
    }
}
