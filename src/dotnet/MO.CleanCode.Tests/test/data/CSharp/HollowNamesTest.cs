using System;

// This class should trigger hollow names highlighting
public class Data
{
    public string Info { get; set; }
    public object Object { get; set; }
    public int Number { get; set; }
}

// These should also trigger hollow names highlighting
public class Thing
{
    public void DoStuff() { }
}

public interface IManager
{
    void Handle();
}

// These should be fine
public class CustomerRepository
{
    public void SaveCustomer() { }
}

public class InvoiceCalculator
{
    public decimal CalculateTotal()
    {
        return 0;
    }
}
