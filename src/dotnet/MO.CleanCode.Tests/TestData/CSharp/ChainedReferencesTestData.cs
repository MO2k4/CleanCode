using System;
using System.Collections.Generic;
using System.Linq;

namespace CleanCode.Tests.TestData.CSharp
{
    public class ChainedReferencesTestData
    {
        public Person Person { get; set; }
        public List<int> Numbers { get; set; }

        // This method should trigger ChainedReferences warning (3 chained calls > default 2)
        public void MethodWithTooManyChainedReferences()
        {
            var result = Person.Address.Street.Length;  // 3 chains: Person.Address.Street.Length
        }

        // This method should NOT trigger warning (2 chained calls = default limit)
        public void MethodWithAcceptableChainedReferences()
        {
            var result = Person.Address.Street;  // 2 chains: Person.Address.Street
        }

        // This method should trigger warning with fluent API that returns different types
        public void MethodWithDifferentTypeChaining()
        {
            var result = new FluentBuilder()
                .SetName("test")    // Returns FluentBuilder
                .SetAge(25)         // Returns FluentBuilder
                .Build()            // Returns Person - different type
                .ToString();        // 4th call - should trigger
        }

        // This method should NOT trigger warning with fluent API returning same type
        public void MethodWithFluentAPIChaining()
        {
            var result = new FluentBuilder()
                .SetName("test")    // Returns FluentBuilder
                .SetAge(25)         // Returns FluentBuilder
                .SetCity("NYC");    // Returns FluentBuilder - same type throughout
        }

        // This method should trigger warning with LINQ when IncludeLinqInChainedReferences = true
        public void MethodWithLinqChaining()
        {
            var result = Numbers
                .Where(x => x > 0)     // Chain 1
                .Select(x => x * 2)    // Chain 2
                .OrderBy(x => x)       // Chain 3 - should trigger when LINQ included
                .ToList();
        }

        // This method should NOT trigger warning with simple property access
        public void MethodWithSimplePropertyAccess()
        {
            var name = Person.Name;
            var age = Person.Age;
        }

        // This method should NOT trigger warning with method calls on same object
        public void MethodWithSameObjectCalls()
        {
            Person.Walk();
            Person.Talk();
            Person.Sleep();
        }

        // Multiple chains in same method
        public void MethodWithMultipleChains()
        {
            var street = Person.Address.Street.Length;      // Should trigger
            var zip = Person.Address.ZipCode.ToString();    // Should trigger
        }

        // Complex expression with chaining
        public void MethodWithComplexChaining()
        {
            var result = Person.Address.Street.Substring(0, Person.Address.Street.Length / 2);
        }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }

        public void Walk() { }
        public void Talk() { }
        public void Sleep() { }
    }

    public class Address
    {
        public string Street { get; set; }
        public string ZipCode { get; set; }
    }

    public class FluentBuilder
    {
        public FluentBuilder SetName(string name) => this;
        public FluentBuilder SetAge(int age) => this;
        public FluentBuilder SetCity(string city) => this;
        public Person Build() => new Person();
    }
}