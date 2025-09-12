using System;

public class TestClass
{
    // This method should trigger too many method arguments highlighting
    public void MethodWithTooManyArguments(
        string arg1,
        string arg2,
        string arg3,
        string arg4,
        string arg5,
        string arg6,
        string arg7,
        string arg8,
        string arg9,
        string arg10
    )
    {
        Console.WriteLine("Too many arguments");
    }

    // This should be fine
    public void MethodWithFewArguments(string arg1, string arg2)
    {
        Console.WriteLine("Few arguments");
    }
}
