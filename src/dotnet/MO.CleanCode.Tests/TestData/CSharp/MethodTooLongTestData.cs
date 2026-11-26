using System;

namespace CleanCode.Tests.TestData.CSharp
{
    public class MethodTooLongTestData
    {
        // This method should trigger MethodTooLong warning (16 statements > default 15)
        public void MethodWithTooManyStatements()
        {
            var x = 1;      // Statement 1
            var y = 2;      // Statement 2
            var z = 3;      // Statement 3
            x = x + 1;      // Statement 4
            y = y + 1;      // Statement 5
            z = z + 1;      // Statement 6
            Console.WriteLine(x);  // Statement 7
            Console.WriteLine(y);  // Statement 8
            Console.WriteLine(z);  // Statement 9
            x = x * 2;      // Statement 10
            y = y * 2;      // Statement 11
            z = z * 2;      // Statement 12
            Console.WriteLine(x);  // Statement 13
            Console.WriteLine(y);  // Statement 14
            Console.WriteLine(z);  // Statement 15
            Console.WriteLine("Done");  // Statement 16 - over the limit
        }

        // This method should NOT trigger warning (15 statements = default limit)
        public void MethodWithAcceptableStatementCount()
        {
            var x = 1;      // Statement 1
            var y = 2;      // Statement 2
            var z = 3;      // Statement 3
            x = x + 1;      // Statement 4
            y = y + 1;      // Statement 5
            z = z + 1;      // Statement 6
            Console.WriteLine(x);  // Statement 7
            Console.WriteLine(y);  // Statement 8
            Console.WriteLine(z);  // Statement 9
            x = x * 2;      // Statement 10
            y = y * 2;      // Statement 11
            z = z * 2;      // Statement 12
            Console.WriteLine(x);  // Statement 13
            Console.WriteLine(y);  // Statement 14
            Console.WriteLine(z);  // Statement 15
        }

        // This method should trigger MethodTooManyDeclarations warning (7 declarations > default 6)
        public void MethodWithTooManyDeclarations()
        {
            var var1 = 1;       // Declaration 1
            var var2 = 2;       // Declaration 2
            var var3 = 3;       // Declaration 3
            var var4 = 4;       // Declaration 4
            var var5 = 5;       // Declaration 5
            var var6 = 6;       // Declaration 6
            var var7 = 7;       // Declaration 7 - over the limit

            Console.WriteLine(var1 + var2 + var3 + var4 + var5 + var6 + var7);
        }

        // This method should NOT trigger declaration warning (6 declarations = default limit)
        public void MethodWithAcceptableDeclarationCount()
        {
            var var1 = 1;       // Declaration 1
            var var2 = 2;       // Declaration 2
            var var3 = 3;       // Declaration 3
            var var4 = 4;       // Declaration 4
            var var5 = 5;       // Declaration 5
            var var6 = 6;       // Declaration 6

            Console.WriteLine(var1 + var2 + var3 + var4 + var5 + var6);
        }

        // Small method should not trigger any warnings
        public void SmallMethod()
        {
            var x = 1;
            Console.WriteLine(x);
        }

        // Empty method should not trigger warnings
        public void EmptyMethod()
        {
        }

        // Method with arrow expression should not trigger warnings
        public int SimpleExpression(int x) => x * 2;

        // Property should not be analyzed
        public int Property { get; set; }
    }
}