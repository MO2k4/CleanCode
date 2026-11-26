using System;

namespace CleanCode.Tests.TestData.CSharp
{
    public enum ProcessingMode
    {
        Fast,
        Slow,
        Detailed
    }

    public class FlagArgumentsTestData
    {
        // This method should trigger FlagArguments warning (bool parameter used in if)
        public void MethodWithBooleanFlag(bool isEnabled)
        {
            if (isEnabled)
            {
                Console.WriteLine("Feature is enabled");
            }
            else
            {
                Console.WriteLine("Feature is disabled");
            }
        }

        // This method should trigger FlagArguments warning (enum parameter used in if)
        public void MethodWithEnumFlag(ProcessingMode mode)
        {
            if (mode == ProcessingMode.Fast)
            {
                Console.WriteLine("Fast processing");
            }
            else if (mode == ProcessingMode.Slow)
            {
                Console.WriteLine("Slow processing");
            }
            else
            {
                Console.WriteLine("Detailed processing");
            }
        }

        // This method should NOT trigger warning (bool parameter not used in if)
        public void MethodWithBooleanNotUsedInIf(bool isEnabled)
        {
            Console.WriteLine($"Status: {isEnabled}");
            var result = isEnabled ? "on" : "off";
            DoSomething(result);
        }

        // This method should NOT trigger warning (enum parameter not used in if)
        public void MethodWithEnumNotUsedInIf(ProcessingMode mode)
        {
            Console.WriteLine($"Mode: {mode}");
            ProcessData(mode);
        }

        // This method should NOT trigger warning (string parameter, not a flag type)
        public void MethodWithStringParameter(string input)
        {
            if (input == "test")
            {
                Console.WriteLine("Input is test");
            }
        }

        // This method should NOT trigger warning (int parameter, not a flag type)
        public void MethodWithIntParameter(int value)
        {
            if (value > 0)
            {
                Console.WriteLine("Value is positive");
            }
        }

        // This method should trigger warning (multiple flag parameters)
        public void MethodWithMultipleFlags(bool flag1, bool flag2, ProcessingMode mode)
        {
            if (flag1)
            {
                Console.WriteLine("Flag1 is true");
            }

            if (flag2)
            {
                Console.WriteLine("Flag2 is true");
            }

            if (mode == ProcessingMode.Fast)
            {
                Console.WriteLine("Fast mode");
            }
        }

        // This method should trigger warning (nested if with flag)
        public void MethodWithNestedFlagUsage(bool outerFlag, bool innerFlag)
        {
            if (outerFlag)
            {
                if (innerFlag)
                {
                    Console.WriteLine("Both flags are true");
                }
            }
        }

        // This method should NOT trigger warning (no if statements)
        public void MethodWithoutIf(bool flag)
        {
            Console.WriteLine("No if statement here");
        }

        // This method should NOT trigger warning (no parameters)
        public void MethodWithoutParameters()
        {
            bool localFlag = true;
            if (localFlag)
            {
                Console.WriteLine("Local flag");
            }
        }

        // This method should trigger warning (complex condition with flag)
        public void MethodWithComplexCondition(bool isActive, int threshold)
        {
            if (isActive && threshold > 10)
            {
                Console.WriteLine("Active and above threshold");
            }
        }

        // This method should NOT trigger warning (flag used for assignment but not in if)
        public void MethodWithFlagAssignment(bool sourceFlag)
        {
            bool localFlag = sourceFlag;
            Console.WriteLine($"Local flag: {localFlag}");
        }

        // This method should trigger warning (switch statement with enum - if applicable)
        public void MethodWithSwitchOnEnum(ProcessingMode mode)
        {
            // This might not trigger if the analyzer only looks for if statements
            switch (mode)
            {
                case ProcessingMode.Fast:
                    Console.WriteLine("Fast");
                    break;
                case ProcessingMode.Slow:
                    Console.WriteLine("Slow");
                    break;
            }

            // But this should trigger
            if (mode == ProcessingMode.Detailed)
            {
                Console.WriteLine("Detailed mode");
            }
        }

        private void DoSomething(string input)
        {
            Console.WriteLine(input);
        }

        private void ProcessData(ProcessingMode mode)
        {
            Console.WriteLine($"Processing in {mode} mode");
        }
    }
}