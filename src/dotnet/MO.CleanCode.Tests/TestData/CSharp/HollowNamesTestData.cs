using System;

namespace CleanCode.Tests.TestData.CSharp
{
    // These classes should trigger HollowNames warning (generic suffixes)
    public class DataManager  // "Manager" is in default hollow names list
    {
        public void ProcessData() { }
    }

    public class RequestHandler  // "Handler" is in default hollow names list
    {
        public void HandleRequest() { }
    }

    public class PaymentProcessor  // "Processor" is in default hollow names list
    {
        public void ProcessPayment() { }
    }

    public class UserController  // "Controller" is in default hollow names list
    {
        public void ControlUser() { }
    }

    public class DatabaseHelper  // "Helper" is in default hollow names list
    {
        public void HelpWithDatabase() { }
    }

    // These classes should NOT trigger warning (specific, meaningful names)
    public class CustomerRepository
    {
        public void SaveCustomer() { }
    }

    public class OrderService
    {
        public void CreateOrder() { }
    }

    public class InvoiceCalculator
    {
        public void CalculateTotal() { }
    }

    public class EmailValidator
    {
        public bool IsValidEmail(string email) => true;
    }

    public class ProductCatalog
    {
        public void AddProduct() { }
    }

    // These classes should NOT trigger warning (hollow names not at the end)
    public class ManagerialReport  // "Manager" is not a suffix
    {
        public void GenerateReport() { }
    }

    public class HandlerConfiguration  // "Handler" is not a suffix
    {
        public void Configure() { }
    }

    // Edge cases
    public class Manager  // Just "Manager" - should trigger
    {
        public void Manage() { }
    }

    public class Handler  // Just "Handler" - should trigger
    {
        public void Handle() { }
    }

    public class Processor  // Just "Processor" - should trigger
    {
        public void Process() { }
    }

    // These should NOT trigger (case sensitivity test)
    public class DatamanagER  // Different casing - might not trigger depending on implementation
    {
        public void ManageData() { }
    }

    // Nested classes should also be checked
    public class OuterClass
    {
        public class NestedManager  // Should trigger
        {
            public void DoWork() { }
        }

        public class NestedService  // Should not trigger
        {
            public void Serve() { }
        }
    }

    // Generic classes should be checked
    public class GenericHandler<T>  // Should trigger
    {
        public void Handle(T item) { }
    }

    public class GenericRepository<T>  // Should not trigger
    {
        public void Save(T item) { }
    }

    // Abstract classes should be checked
    public abstract class AbstractProcessor  // Should trigger
    {
        public abstract void Process();
    }

    public abstract class AbstractValidator  // Should not trigger
    {
        public abstract bool Validate();
    }

    // Interface names should be checked if the analyzer covers them
    public interface IDataManager  // Should trigger if interfaces are checked
    {
        void ManageData();
    }

    public interface IUserService  // Should not trigger
    {
        void ServeUser();
    }

    // Static classes should be checked
    public static class StaticHelper  // Should trigger
    {
        public static void Help() { }
    }

    public static class MathUtilities  // Should not trigger
    {
        public static int Add(int a, int b) => a + b;
    }

    // Partial classes should be checked
    public partial class PartialManager  // Should trigger
    {
        public void ManagePartially() { }
    }

    public partial class PartialCalculator  // Should not trigger
    {
        public void Calculate() { }
    }
}