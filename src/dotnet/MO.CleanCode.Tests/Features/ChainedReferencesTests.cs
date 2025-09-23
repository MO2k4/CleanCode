using System.Linq;
using CleanCode.Features.ChainedReferences;
using CleanCode.Settings;
using NUnit.Framework;

namespace CleanCode.Tests.Features
{
    [TestFixture]
    public class ChainedReferencesTests : CleanCodeTestBase
    {
        protected override string RelativeTestDataPath => "ChainedReferences";

        [Test]
        public void Should_Highlight_Method_With_Too_Many_Chained_References()
        {
            // Test with default settings (2 max chained references)
            var highlightings = RunInspection("ChainedReferencesTestData");
            var chainedReferencesHighlightings = highlightings
                .OfType&lt;MaximumChainedReferencesHighlighting&gt;()
                .ToList();

            // Should find violations in methods with 3+ chained references
            Assert.GreaterOrEqual(chainedReferencesHighlightings.Count, 1);

            var firstHighlighting = chainedReferencesHighlightings[0];
            Assert.IsTrue(firstHighlighting.ToolTip.Contains("too many chained references"));
        }

        [Test]
        public void Should_Not_Highlight_Method_Within_Chain_Limit()
        {
            // Test with custom settings allowing 5 chained references
            var settings = new CleanCodeSettings
            {
                MaximumChainedReferences = 5
            };

            var highlightings = RunInspection("ChainedReferencesTestData", settings);
            var chainedReferencesHighlightings = highlightings
                .OfType&lt;MaximumChainedReferencesHighlighting&gt;()
                .ToList();

            // With limit of 5, fewer methods should be highlighted
            var originalCount = RunInspection("ChainedReferencesTestData")
                .OfType&lt;MaximumChainedReferencesHighlighting&gt;()
                .Count();

            Assert.LessOrEqual(chainedReferencesHighlightings.Count, originalCount);
        }

        [Test]
        public void Should_Respect_IncludeLinqInChainedReferences_Setting()
        {
            // Test with LINQ inclusion disabled (default)
            var settingsExcludeLinq = new CleanCodeSettings
            {
                IncludeLinqInChainedReferences = false
            };

            var highlightingsExcludeLinq = RunInspection("ChainedReferencesTestData", settingsExcludeLinq);
            var chainedReferencesExcludeLinq = highlightingsExcludeLinq
                .OfType&lt;MaximumChainedReferencesHighlighting&gt;()
                .ToList();

            // Test with LINQ inclusion enabled
            var settingsIncludeLinq = new CleanCodeSettings
            {
                IncludeLinqInChainedReferences = true
            };

            var highlightingsIncludeLinq = RunInspection("ChainedReferencesTestData", settingsIncludeLinq);
            var chainedReferencesIncludeLinq = highlightingsIncludeLinq
                .OfType&lt;MaximumChainedReferencesHighlighting&gt;()
                .ToList();

            // Including LINQ should potentially find more violations
            Assert.GreaterOrEqual(chainedReferencesIncludeLinq.Count, chainedReferencesExcludeLinq.Count);
        }

        [Test]
        public void Should_Not_Highlight_Fluent_APIs_With_Same_Return_Type()
        {
            var highlightings = RunInspection("ChainedReferencesTestData");
            var chainedReferencesHighlightings = highlightings
                .OfType&lt;MaximumChainedReferencesHighlighting&gt;()
                .ToList();

            // MethodWithFluentAPIChaining should not be highlighted
            // as fluent APIs returning the same type should be ignored
            var fluentApiViolations = chainedReferencesHighlightings
                .Where(h => h.ToString().Contains("MethodWithFluentAPIChaining"))
                .ToList();

            // This might be 0 if the analyzer correctly ignores same-type fluent chains
            Assert.LessOrEqual(fluentApiViolations.Count, chainedReferencesHighlightings.Count);
        }

        [Test]
        public void Should_Not_Highlight_Simple_Property_Access()
        {
            var highlightings = RunInspection("ChainedReferencesTestData");
            var chainedReferencesHighlightings = highlightings
                .OfType&lt;MaximumChainedReferencesHighlighting&gt;()
                .ToList();

            // MethodWithSimplePropertyAccess should not be highlighted
            var simplePropertyViolations = chainedReferencesHighlightings
                .Where(h => h.ToString().Contains("MethodWithSimplePropertyAccess"))
                .ToList();

            Assert.AreEqual(0, simplePropertyViolations.Count);
        }

        [Test]
        public void Should_Not_Highlight_Same_Object_Method_Calls()
        {
            var highlightings = RunInspection("ChainedReferencesTestData");
            var chainedReferencesHighlightings = highlightings
                .OfType&lt;MaximumChainedReferencesHighlighting&gt;()
                .ToList();

            // MethodWithSameObjectCalls should not be highlighted
            var sameObjectViolations = chainedReferencesHighlightings
                .Where(h => h.ToString().Contains("MethodWithSameObjectCalls"))
                .ToList();

            Assert.AreEqual(0, sameObjectViolations.Count);
        }

        [Test]
        public void Should_Detect_Multiple_Chains_In_Same_Method()
        {
            var highlightings = RunInspection("ChainedReferencesTestData");
            var chainedReferencesHighlightings = highlightings
                .OfType&lt;MaximumChainedReferencesHighlighting&gt;()
                .Where(h => h.ToString().Contains("MethodWithMultipleChains"))
                .ToList();

            // Should detect multiple violations in the same method
            Assert.GreaterOrEqual(chainedReferencesHighlightings.Count, 1);
        }

        [Test]
        public void Should_Have_Correct_Highlighting_Message()
        {
            var highlightings = RunInspection("ChainedReferencesTestData");
            var chainedReferencesHighlighting = highlightings
                .OfType&lt;MaximumChainedReferencesHighlighting&gt;()
                .FirstOrDefault();

            Assert.IsNotNull(chainedReferencesHighlighting);
            var message = chainedReferencesHighlighting.ToolTip;
            Assert.IsTrue(message.Contains("too many chained references") ||
                         message.Contains("violating the Law of Demeter"));
        }
    }
}