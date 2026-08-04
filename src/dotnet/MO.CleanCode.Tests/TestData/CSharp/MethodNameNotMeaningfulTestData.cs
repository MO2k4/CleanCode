using System;

namespace CleanCode.Tests.TestData.CSharp
{
    public class MethodNameNotMeaningfulTestData
    {
        // These methods should trigger MethodNameNotMeaningful warning (< 4 characters)
        public void Do()  // 2 characters
        {
            Console.WriteLine("Do something");
        }

        public void Go()  // 2 characters
        {
            Console.WriteLine("Go somewhere");
        }

        public void Run()  // 3 characters
        {
            Console.WriteLine("Run process");
        }

        public void Get()  // 3 characters
        {
            Console.WriteLine("Get data");
        }

        // These methods should NOT trigger warning (>= 4 characters)
        public void Save()  // 4 characters = limit
        {
            Console.WriteLine("Save data");
        }

        public void Process()  // 7 characters
        {
            Console.WriteLine("Process data");
        }

        public void ExecuteOperation()  // 15 characters
        {
            Console.WriteLine("Execute operation");
        }

        public void CalculateTotal()  // 14 characters
        {
            Console.WriteLine("Calculate total");
        }

        // Property getters/setters should be checked if they're explicit methods
        public string Name
        {
            get { return "test"; }
            set { Console.WriteLine(value); }
        }

        // Constructor should not be checked (it doesn't have a "meaningful" name requirement)
        public MethodNameNotMeaningfulTestData()
        {
        }

        // Static methods should also be checked
        public static void Add()  // 3 characters - should trigger
        {
            Console.WriteLine("Add items");
        }

        public static void CreateInstance()  // 14 characters - should not trigger
        {
            Console.WriteLine("Create instance");
        }

        // Private methods should also be checked
        private void Set()  // 3 characters - should trigger
        {
            Console.WriteLine("Set value");
        }

        private void UpdateConfiguration()  // 19 characters - should not trigger
        {
            Console.WriteLine("Update configuration");
        }

        // Async methods should be checked
        public async void Load()  // 4 characters - should not trigger
        {
            Console.WriteLine("Load data");
        }

        // Generic methods should be checked
        public void Map<T>()  // 3 characters - should trigger
        {
            Console.WriteLine("Map data");
        }

        public void ConvertToType<T>()  // 13 characters - should not trigger
        {
            Console.WriteLine("Convert to type");
        }

        // Methods with parameters should still be checked
        public void Fix(string input)  // 3 characters - should trigger
        {
            Console.WriteLine($"Fix: {input}");
        }

        public void ValidateInput(string input)  // 13 characters - should not trigger
        {
            Console.WriteLine($"Validate: {input}");
        }

        // Interface implementation methods should be checked
        public void Act()  // 3 characters - should trigger (if this implements an interface)
        {
            Console.WriteLine("Act on data");
        }

        // Override methods should be checked
        public override string ToString()  // 8 characters - should not trigger
        {
            return "Test";
        }
    }

    // Interface methods should also be checked
    public interface ITestInterface
    {
        void Do();  // 2 characters - should trigger
        void ProcessData();  // 11 characters - should not trigger
    }

    // Abstract methods should also be checked
    public abstract class AbstractTestClass
    {
        public abstract void Go();  // 2 characters - should trigger
        public abstract void ExecuteCommand();  // 14 characters - should not trigger
    }
}