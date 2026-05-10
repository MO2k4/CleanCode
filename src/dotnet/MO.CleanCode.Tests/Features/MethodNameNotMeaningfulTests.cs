using System.Linq;
using CleanCode.Features.MethodNameNotMeaningful;
using CleanCode.Settings;
using NUnit.Framework;

namespace CleanCode.Tests.Features
{
    [TestFixture]
    public class MethodNameNotMeaningfulTests : CleanCodeTestBase
    {
        protected override string RelativeTestDataPath => "MethodNameNotMeaningful";

        [Test]
        public void Should_Highlight_Methods_With_Short_Names()
        {
            // Test with default settings (4 min characters)
            var highlightings = RunInspection("MethodNameNotMeaningfulTestData");
            var methodNameHighlightings = highlightings
                .OfType<MethodNameNotMeaningfulHighlighting>()
                .ToList();

            // Should find violations in methods with < 4 character names
            Assert.GreaterOrEqual(methodNameHighlightings.Count, 1);

            var firstHighlighting = methodNameHighlightings[0];
            Assert.IsTrue(firstHighlighting.ToolTip.Contains("method name is not meaningful") ||
                         firstHighlighting.ToolTip.Contains("too short"));
        }

        [Test]
        public void Should_Not_Highlight_Methods_With_Long_Enough_Names()
        {
            // Test with custom settings allowing 2 character names
            var settings = new CleanCodeSettings
            {
                MinimumMeaningfulMethodNameLength = 2
            };

            var highlightings = RunInspection("MethodNameNotMeaningfulTestData", settings);
            var methodNameHighlightings = highlightings
                .OfType<MethodNameNotMeaningfulHighlighting>()
                .ToList();

            // With limit of 2, fewer methods should be highlighted
            var originalCount = RunInspection("MethodNameNotMeaningfulTestData")
                .OfType<MethodNameNotMeaningfulHighlighting>()
                .Count();

            Assert.LessOrEqual(methodNameHighlightings.Count, originalCount);
        }

        [Test]
        public void Should_Check_All_Method_Types()
        {
            var highlightings = RunInspection("MethodNameNotMeaningfulTestData");
            var methodNameHighlightings = highlightings
                .OfType<MethodNameNotMeaningfulHighlighting>()
                .ToList();

            // Should check instance methods, static methods, private methods
            Assert.GreaterOrEqual(methodNameHighlightings.Count, 3);
        }

        [Test]
        public void Should_Not_Highlight_Methods_At_Exact_Limit()
        {
            var highlightings = RunInspection("MethodNameNotMeaningfulTestData");
            var methodNameHighlightings = highlightings
                .OfType<MethodNameNotMeaningfulHighlighting>()
                .ToList();

            // Methods with exactly 4 characters should not be highlighted
            var exactLimitViolations = methodNameHighlightings
                .Where(h => h.ToString().Contains("Save") || // 4 characters
                           h.ToString().Contains("Load"))   // 4 characters
                .ToList();

            Assert.AreEqual(0, exactLimitViolations.Count);
        }

        [Test]
        public void Should_Not_Highlight_Long_Method_Names()
        {
            var highlightings = RunInspection("MethodNameNotMeaningfulTestData");
            var methodNameHighlightings = highlightings
                .OfType<MethodNameNotMeaningfulHighlighting>()
                .ToList();

            // Long method names should not be highlighted
            var longNameViolations = methodNameHighlightings
                .Where(h => h.ToString().Contains("ExecuteOperation") ||
                           h.ToString().Contains("CalculateTotal") ||
                           h.ToString().Contains("UpdateConfiguration"))
                .ToList();

            Assert.AreEqual(0, longNameViolations.Count);
        }

        [Test]
        public void Should_Have_Correct_Highlighting_Message()
        {
            var highlightings = RunInspection("MethodNameNotMeaningfulTestData");
            var methodNameHighlighting = highlightings
                .OfType<MethodNameNotMeaningfulHighlighting>()
                .FirstOrDefault();

            if (methodNameHighlighting != null)
            {
                var message = methodNameHighlighting.ToolTip;
                Assert.IsTrue(message.Contains("method name is not meaningful") ||
                             message.Contains("too short") ||
                             message.Contains("not descriptive"));
            }
        }
    }
}