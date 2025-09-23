using System.Linq;
using CleanCode.Features.ExcessiveIndentation;
using CleanCode.Settings;
using NUnit.Framework;

namespace CleanCode.Tests.Features
{
    [TestFixture]
    public class ExcessiveIndentationTests : CleanCodeTestBase
    {
        protected override string RelativeTestDataPath => "ExcessiveIndentation";

        [Test]
        public void Should_Highlight_Method_With_Excessive_Indentation()
        {
            // Test with default settings (3 max indentation levels)
            var highlightings = RunInspection("ExcessiveIndentationTestData");
            var excessiveIndentHighlightings = highlightings
                .OfType&lt;ExcessiveIndentHighlighting&gt;()
                .ToList();

            // Should find violations in methods with 4+ levels of nesting
            Assert.GreaterOrEqual(excessiveIndentHighlightings.Count, 4);

            var firstHighlighting = excessiveIndentHighlightings[0];
            Assert.IsTrue(firstHighlighting.ToolTip.Contains("(4 / 3)"));
            Assert.IsTrue(firstHighlighting.ToolTip.Contains("too deeply indented"));
        }

        [Test]
        public void Should_Not_Highlight_Method_Within_Indentation_Limit()
        {
            // Test with custom settings allowing 5 indentation levels
            var settings = new CleanCodeSettings
            {
                MaximumIndentationDepth = 5
            };

            var highlightings = RunInspection("ExcessiveIndentationTestData", settings);
            var excessiveIndentHighlightings = highlightings
                .OfType&lt;ExcessiveIndentHighlighting&gt;()
                .ToList();

            // With limit of 5, no methods should be highlighted
            Assert.AreEqual(0, excessiveIndentHighlightings.Count);
        }

        [Test]
        public void Should_Detect_Indentation_In_Different_Constructs()
        {
            var highlightings = RunInspection("ExcessiveIndentationTestData");
            var excessiveIndentHighlightings = highlightings
                .OfType&lt;ExcessiveIndentHighlighting&gt;()
                .ToList();

            // Should detect excessive indentation in if statements, loops, try-catch, switch
            var methodNames = excessiveIndentHighlightings
                .Select(h => h.ToString())
                .ToList();

            // Verify we catch different types of nesting
            Assert.IsTrue(methodNames.Any(name =>
                name.Contains("MethodWithExcessiveIndentation") ||
                name.Contains("MethodWithNestedLoops") ||
                name.Contains("MethodWithNestedTryCatch") ||
                name.Contains("MethodWithSwitchNesting")));
        }

        [Test]
        public void Should_Not_Highlight_Shallow_Methods()
        {
            var highlightings = RunInspection("ExcessiveIndentationTestData");
            var excessiveIndentHighlightings = highlightings
                .OfType&lt;ExcessiveIndentHighlighting&gt;()
                .ToList();

            // MethodWithShallowNesting, EmptyMethod, SimpleMethod should not be highlighted
            var shallowMethodViolations = excessiveIndentHighlightings
                .Where(h => h.ToString().Contains("MethodWithShallowNesting") ||
                           h.ToString().Contains("EmptyMethod") ||
                           h.ToString().Contains("SimpleMethod"))
                .ToList();

            Assert.AreEqual(0, shallowMethodViolations.Count);
        }

        [Test]
        public void Should_Not_Highlight_Method_At_Exact_Limit()
        {
            var highlightings = RunInspection("ExcessiveIndentationTestData");
            var excessiveIndentHighlightings = highlightings
                .OfType&lt;ExcessiveIndentHighlighting&gt;()
                .ToList();

            // MethodWithAcceptableIndentation should not be highlighted (exactly at limit)
            var acceptableMethodViolations = excessiveIndentHighlightings
                .Where(h => h.ToString().Contains("MethodWithAcceptableIndentation"))
                .ToList();

            Assert.AreEqual(0, acceptableMethodViolations.Count);
        }

        [Test]
        public void Should_Have_Correct_Highlighting_Message()
        {
            var highlightings = RunInspection("ExcessiveIndentationTestData");
            var excessiveIndentHighlighting = highlightings
                .OfType&lt;ExcessiveIndentHighlighting&gt;()
                .FirstOrDefault();

            Assert.IsNotNull(excessiveIndentHighlighting);
            var message = excessiveIndentHighlighting.ToolTip;
            Assert.IsTrue(message.Contains("method is too deeply indented"));
        }
    }
}