using System;

public class TestClass
{
    // This method should trigger excessive indentation highlighting
    public void MethodWithExcessiveIndentation()
    {
        if (true)
        {
            if (true)
            {
                if (true)
                {
                    if (true)
                    {
                        if (true)
                        {
                            if (true)
                            {
                                Console.WriteLine("Too deeply nested");
                            }
                        }
                    }
                }
            }
        }
    }

    // This should be fine
    public void MethodWithNormalIndentation()
    {
        if (true)
        {
            Console.WriteLine("Normal indentation");
        }
    }
}
