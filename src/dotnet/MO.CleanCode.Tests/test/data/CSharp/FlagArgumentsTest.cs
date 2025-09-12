using System;

public class TestClass
{
    // This method should trigger flag arguments highlighting
    public void ProcessData(string data, bool shouldValidate, bool shouldLog, bool shouldCache)
    {
        if (shouldValidate)
        {
            // validate data
        }

        if (shouldLog)
        {
            Console.WriteLine("Logging: " + data);
        }

        if (shouldCache)
        {
            // cache data
        }
    }

    // This should be fine
    public void ProcessDataCorrectly(string data, ProcessingOptions options)
    {
        Console.WriteLine("Processing: " + data);
    }
}

public class ProcessingOptions
{
    public bool ShouldValidate { get; set; }
    public bool ShouldLog { get; set; }
    public bool ShouldCache { get; set; }
}
