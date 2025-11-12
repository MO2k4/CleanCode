using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using CleanCode.Settings;

namespace CleanCode.Tests
{
    [TestFixture]
    public abstract class CleanCodeTestBase
    {
        protected virtual string RelativeTestDataPath => "CSharp";

        // Simplified test base for now - will be enhanced when we have proper ReSharper test setup
        protected IEnumerable<object> RunInspection(string testName, CleanCodeSettings settings = null)
        {
            // TODO: Implement proper ReSharper test framework integration
            // For now, return empty to allow compilation
            return Enumerable.Empty<object>();
        }
    }
}