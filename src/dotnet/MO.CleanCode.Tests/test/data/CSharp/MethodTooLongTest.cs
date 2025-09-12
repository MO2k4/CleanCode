using System;

public class TestClass
{
    // This method should trigger MethodTooLong highlighting - has many statements
    public void MethodWithTooManyStatements()
    {
        int a = 1; // Statement 1
        int b = 2; // Statement 2
        int c = 3; // Statement 3
        int d = 4; // Statement 4
        int e = 5; // Statement 5
        int f = 6; // Statement 6
        int g = 7; // Statement 7
        int h = 8; // Statement 8
        int i = 9; // Statement 9
        int j = 10; // Statement 10
        int k = 11; // Statement 11
        int l = 12; // Statement 12
        int m = 13; // Statement 13
        int n = 14; // Statement 14
        int o = 15; // Statement 15
        int p = 16; // Statement 16
        int q = 17; // Statement 17
        int r = 18; // Statement 18
        int s = 19; // Statement 19
        int t = 20; // Statement 20
        Console.WriteLine(
            a + b + c + d + e + f + g + h + i + j + k + l + m + n + o + p + q + r + s + t
        );
    }

    // This method should trigger MethodTooManyDeclarations highlighting
    public void MethodWithTooManyDeclarations()
    {
        string local1 = "test";
        string local2 = "test";
        string local3 = "test";
        string local4 = "test";
        string local5 = "test";
        string local6 = "test";
        string local7 = "test";
        string local8 = "test";
        string local9 = "test";
        string local10 = "test";
        Console.WriteLine(
            local1 + local2 + local3 + local4 + local5 + local6 + local7 + local8 + local9 + local10
        );
    }

    // This method should be fine
    public void ShortMethod()
    {
        Console.WriteLine("This is a short method");
    }
}
