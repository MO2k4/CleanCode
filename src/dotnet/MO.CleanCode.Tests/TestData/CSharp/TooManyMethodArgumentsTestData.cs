using System;

namespace CleanCode.Tests.TestData.CSharp
{
    public class TooManyMethodArgumentsTestData
    {
        // This method should trigger TooManyMethodArguments warning (4 parameters > default 3)
        public void MethodWithTooManyArguments(string arg1, int arg2, bool arg3, double arg4)
        {
            Console.WriteLine($"{arg1}, {arg2}, {arg3}, {arg4}");
        }

        // This method should NOT trigger warning (3 parameters = default limit)
        public void MethodWithAcceptableArguments(string arg1, int arg2, bool arg3)
        {
            Console.WriteLine($"{arg1}, {arg2}, {arg3}");
        }

        // This method should trigger warning with more parameters
        public void MethodWithManyArguments(
            string firstName,
            string lastName,
            int age,
            string email,
            string phone,
            string address,
            string city)  // 7 parameters - definitely over limit
        {
            Console.WriteLine($"{firstName} {lastName}, {age}, {email}, {phone}, {address}, {city}");
        }

        // This method should NOT trigger warning (2 parameters < limit)
        public void MethodWithFewArguments(string name, int age)
        {
            Console.WriteLine($"{name}, {age}");
        }

        // This method should NOT trigger warning (1 parameter < limit)
        public void MethodWithOneArgument(string message)
        {
            Console.WriteLine(message);
        }

        // This method should NOT trigger warning (0 parameters)
        public void MethodWithNoArguments()
        {
            Console.WriteLine("No arguments");
        }

        // This method should trigger warning with mixed parameter types
        public void MethodWithMixedParameterTypes(
            string text,
            int number,
            DateTime date,
            bool flag)  // 4 parameters > 3 limit
        {
            Console.WriteLine($"{text}, {number}, {date}, {flag}");
        }

        // Constructor should also be checked
        public TooManyMethodArgumentsTestData(string arg1, int arg2, bool arg3, double arg4)  // 4 > 3
        {
            Console.WriteLine($"Constructor: {arg1}, {arg2}, {arg3}, {arg4}");
        }

        // Constructor with acceptable parameters
        public TooManyMethodArgumentsTestData(string arg1, int arg2, bool arg3)  // 3 = limit
        {
            Console.WriteLine($"Constructor: {arg1}, {arg2}, {arg3}");
        }

        // Constructor with no parameters
        public TooManyMethodArgumentsTestData()
        {
            Console.WriteLine("Default constructor");
        }

        // Static method should also be checked
        public static void StaticMethodWithTooManyArguments(string arg1, int arg2, bool arg3, double arg4)
        {
            Console.WriteLine($"Static: {arg1}, {arg2}, {arg3}, {arg4}");
        }

        // Private method should also be checked
        private void PrivateMethodWithTooManyArguments(string arg1, int arg2, bool arg3, double arg4)
        {
            Console.WriteLine($"Private: {arg1}, {arg2}, {arg3}, {arg4}");
        }

        // Method with optional parameters
        public void MethodWithOptionalParameters(
            string required1,
            string required2,
            string optional1 = "default1",
            string optional2 = "default2")  // 4 parameters total > 3 limit
        {
            Console.WriteLine($"{required1}, {required2}, {optional1}, {optional2}");
        }

        // Method with params array
        public void MethodWithParamsArray(string message, params object[] args)  // 2 parameters < limit
        {
            Console.WriteLine($"{message}: {string.Join(", ", args)}");
        }

        // Method with ref/out parameters
        public void MethodWithRefOutParameters(
            string input,
            out string output,
            ref int counter,
            bool flag)  // 4 parameters > 3 limit
        {
            output = input.ToUpper();
            counter++;
            Console.WriteLine($"{input}, {output}, {counter}, {flag}");
        }
    }

    // Interface methods should also be checked
    public interface ITestInterface
    {
        void InterfaceMethodWithTooManyArguments(string arg1, int arg2, bool arg3, double arg4);
        void InterfaceMethodWithAcceptableArguments(string arg1, int arg2, bool arg3);
    }

    // Abstract methods should also be checked
    public abstract class AbstractTestClass
    {
        public abstract void AbstractMethodWithTooManyArguments(string arg1, int arg2, bool arg3, double arg4);
        public abstract void AbstractMethodWithAcceptableArguments(string arg1, int arg2, bool arg3);
    }
}