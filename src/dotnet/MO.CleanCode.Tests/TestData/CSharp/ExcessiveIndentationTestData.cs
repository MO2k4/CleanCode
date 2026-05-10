using System;

namespace CleanCode.Tests.TestData.CSharp
{
    public class ExcessiveIndentationTestData
    {
        // This method should trigger ExcessiveIndentation warning (4 levels > default 3)
        public void MethodWithExcessiveIndentation()
        {
            if (true)              // Level 1
            {
                if (true)          // Level 2
                {
                    if (true)      // Level 3
                    {
                        if (true)  // Level 4 - over the limit
                        {
                            Console.WriteLine("Too deep!");
                        }
                    }
                }
            }
        }

        // This method should NOT trigger warning (3 levels = default limit)
        public void MethodWithAcceptableIndentation()
        {
            if (true)              // Level 1
            {
                if (true)          // Level 2
                {
                    if (true)      // Level 3
                    {
                        Console.WriteLine("Just right!");
                    }
                }
            }
        }

        // This method should trigger warning with loop nesting
        public void MethodWithNestedLoops()
        {
            for (int i = 0; i < 5; i++)        // Level 1
            {
                for (int j = 0; j < 5; j++)    // Level 2
                {
                    for (int k = 0; k < 5; k++) // Level 3
                    {
                        for (int l = 0; l < 5; l++) // Level 4 - over the limit
                        {
                            Console.WriteLine($"{i},{j},{k},{l}");
                        }
                    }
                }
            }
        }

        // This method should trigger warning with try-catch nesting
        public void MethodWithNestedTryCatch()
        {
            try                    // Level 1
            {
                try                // Level 2
                {
                    try            // Level 3
                    {
                        try        // Level 4 - over the limit
                        {
                            Console.WriteLine("Deep try");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Deep catch");
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Try catch");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Try catch");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Try catch");
            }
        }

        // This method should NOT trigger warning (shallow nesting)
        public void MethodWithShallowNesting()
        {
            if (true)
            {
                Console.WriteLine("Level 1");
            }

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"Level 1: {i}");
            }
        }

        // Empty method should not trigger warning
        public void EmptyMethod()
        {
        }

        // Simple method should not trigger warning
        public void SimpleMethod()
        {
            Console.WriteLine("Simple");
        }

        // Method with switch statement nesting
        public void MethodWithSwitchNesting(int value)
        {
            switch (value)         // Level 1
            {
                case 1:
                    if (true)      // Level 2
                    {
                        if (true)  // Level 3
                        {
                            switch (value) // Level 4 - over the limit
                            {
                                case 1:
                                    Console.WriteLine("Deep switch");
                                    break;
                            }
                        }
                    }
                    break;
            }
        }
    }
}