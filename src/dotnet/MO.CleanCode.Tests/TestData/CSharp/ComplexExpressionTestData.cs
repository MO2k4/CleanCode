using System;

namespace CleanCode.Tests.TestData.CSharp
{
    public class ComplexExpressionTestData
    {
        public int Value { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }

        // This method should trigger ComplexConditionExpression warning (2 operators > default 1)
        public void MethodWithComplexIfCondition()
        {
            if (Value > 0 && IsActive && Name != null)  // 3 operators: >, &&, &&, !=
            {
                Console.WriteLine("Complex condition");
            }
        }

        // This method should NOT trigger warning (1 operator = default limit)
        public void MethodWithSimpleIfCondition()
        {
            if (Value > 0)  // 1 operator: >
            {
                Console.WriteLine("Simple condition");
            }
        }

        // This method should trigger warning in while loop
        public void MethodWithComplexWhileCondition()
        {
            while (Value > 0 && IsActive && Name != null)  // 3 operators
            {
                Value--;
            }
        }

        // This method should trigger warning in for loop
        public void MethodWithComplexForCondition()
        {
            for (int i = 0; i < 10 && IsActive && Value > 0; i++)  // 3 operators: <, &&, &&, >
            {
                Console.WriteLine(i);
            }
        }

        // This method should trigger warning in ternary expression
        public string MethodWithComplexTernaryCondition()
        {
            return (Value > 0 && IsActive && Name != null) ? "valid" : "invalid";  // 3 operators
        }

        // This method should NOT trigger warning (simple conditions)
        public void MethodWithSimpleConditions()
        {
            if (IsActive)  // 0 operators (just a boolean)
            {
                Console.WriteLine("Active");
            }

            while (Value > 0)  // 1 operator
            {
                Value--;
            }

            for (int i = 0; i < 10; i++)  // 1 operator
            {
                Console.WriteLine(i);
            }
        }

        // This method should trigger warning with nested logical operators
        public void MethodWithNestedLogicalOperators()
        {
            if ((Value > 0 || Value < -10) && (IsActive || Name == "test"))  // 4 operators: >, ||, <, &&, ||, ==
            {
                Console.WriteLine("Nested logic");
            }
        }

        // This method should trigger warning in assignment with complex expression
        public void MethodWithComplexAssignment()
        {
            bool result = Value > 0 && IsActive && Name != null;  // 3 operators
            Console.WriteLine(result);
        }

        // This method should trigger warning with arithmetic and logical operators
        public void MethodWithMixedOperators()
        {
            if (Value + 10 > 20 && IsActive)  // 3 operators: +, >, &&
            {
                Console.WriteLine("Mixed operators");
            }
        }

        // This method should NOT trigger warning (no conditions)
        public void MethodWithoutConditions()
        {
            Console.WriteLine("No conditions here");
            Value = 42;
            Name = "test";
        }

        // This method should trigger warning with multiple complex conditions
        public void MethodWithMultipleComplexConditions()
        {
            if (Value > 0 && IsActive && Name != null)  // 3 operators - should trigger
            {
                Console.WriteLine("First complex condition");
            }

            if (Value < 100 && !IsActive && Name == "test")  // 4 operators - should trigger
            {
                Console.WriteLine("Second complex condition");
            }
        }

        // This method should trigger warning with complex do-while
        public void MethodWithComplexDoWhile()
        {
            do
            {
                Value++;
            }
            while (Value < 100 && IsActive && Name != null);  // 3 operators
        }

        // This method should NOT trigger warning with simple boolean checks
        public void MethodWithSimpleBooleanChecks()
        {
            if (IsActive)
            {
                Console.WriteLine("Simple boolean");
            }

            if (!IsActive)
            {
                Console.WriteLine("Negated boolean");
            }
        }

        // Array initialization with complex expression
        public void MethodWithComplexArrayInitialization()
        {
            bool[] results = { Value > 0 && IsActive && Name != null };  // 3 operators in initializer
        }
    }
}