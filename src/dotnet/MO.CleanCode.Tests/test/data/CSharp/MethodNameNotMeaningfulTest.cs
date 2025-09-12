using System;

public class TestClass
{
    // These methods should trigger method name not meaningful highlighting
    public void DoIt() { }

    public void Process() { }

    public void Handle() { }

    public void Execute() { }

    public void Run() { }

    // These should be fine
    public void CalculateTotal() { }

    public void SaveCustomer() { }

    public void ValidateInput() { }
}
