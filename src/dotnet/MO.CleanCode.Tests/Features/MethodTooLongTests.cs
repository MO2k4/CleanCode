using System.Linq;
using CleanCode.Features.MethodTooLong;
using CleanCode.Settings;
using NUnit.Framework;

namespace CleanCode.Tests.Features
{
    [TestFixture]
    public class MethodTooLongTests : CleanCodeTestBase
    {
        protected override string RelativeTestDataPath => "MethodTooLong";

        [Test]
        public void Should_Highlight_Method_With_Too_Many_Statements()
        {
            // Test with default settings (15 max statements)
            var highlightings = RunInspection("MethodTooLongTestData");
            var methodTooLongHighlightings = highlightings
                .OfType<MethodTooLongHighlighting>()
                .ToList();

            // Should find violation in MethodWithTooManyStatements (16 statements > 15 default)
            Assert.AreEqual(1, methodTooLongHighlightings.Count);

            var highlighting = methodTooLongHighlightings[0];
            Assert.IsTrue(highlighting.ToolTip.Contains("(16 / 15)"));
            Assert.IsTrue(highlighting.ToolTip.Contains("too many statements"));
        }

        [Test]
        public void Should_Highlight_Method_With_Too_Many_Declarations()
        {
            // Test with default settings (6 max declarations)
            var highlightings = RunInspection("MethodTooLongTestData");
            var methodTooManyDeclarationsHighlightings = highlightings
                .OfType<MethodTooManyDeclarationsHighlighting>()
                .ToList();

            // Should find violation in MethodWithTooManyDeclarations (7 declarations > 6 default)
            Assert.AreEqual(1, methodTooManyDeclarationsHighlightings.Count);

            var highlighting = methodTooManyDeclarationsHighlightings[0];
            Assert.IsTrue(highlighting.ToolTip.Contains("(7 / 6)"));
            Assert.IsTrue(highlighting.ToolTip.Contains("too many declarations"));
        }

        [Test]
        public void Should_Not_Highlight_Methods_Within_Statement_Limit()
        {
            // Test with custom settings allowing 20 statements
            var settings = new CleanCodeSettings
            {
                MaximumMethodStatements = 20
            };

            var highlightings = RunInspection("MethodTooLongTestData", settings);
            var methodTooLongHighlightings = highlightings
                .OfType<MethodTooLongHighlighting>()
                .ToList();

            // With limit of 20, no methods should be highlighted for statement count
            Assert.AreEqual(0, methodTooLongHighlightings.Count);
        }

        [Test]
        public void Should_Not_Highlight_Methods_Within_Declaration_Limit()
        {
            // Test with custom settings allowing 10 declarations
            var settings = new CleanCodeSettings
            {
                MaximumDeclarationsInMethod = 10
            };

            var highlightings = RunInspection("MethodTooLongTestData", settings);
            var methodTooManyDeclarationsHighlightings = highlightings
                .OfType<MethodTooManyDeclarationsHighlighting>()
                .ToList();

            // With limit of 10, no methods should be highlighted for declaration count
            Assert.AreEqual(0, methodTooManyDeclarationsHighlightings.Count);
        }

        [Test]
        public void Should_Not_Highlight_Small_Methods()
        {
            var highlightings = RunInspection("MethodTooLongTestData");
            var allHighlightings = highlightings
                .Where(h => h is MethodTooLongHighlighting || h is MethodTooManyDeclarationsHighlighting)
                .ToList();

            // SmallMethod, EmptyMethod, SimpleExpression should not be highlighted
            var smallMethodViolations = allHighlightings
                .Where(h => h.ToString().Contains("SmallMethod") ||
                           h.ToString().Contains("EmptyMethod") ||
                           h.ToString().Contains("SimpleExpression"))
                .ToList();

            Assert.AreEqual(0, smallMethodViolations.Count);
        }

        [Test]
        public void Should_Not_Highlight_Methods_At_Exact_Limit()
        {
            var highlightings = RunInspection("MethodTooLongTestData");

            // MethodWithAcceptableStatementCount and MethodWithAcceptableDeclarationCount
            // should not be highlighted as they are exactly at the limit
            var acceptableMethodViolations = highlightings
                .Where(h => (h is MethodTooLongHighlighting || h is MethodTooManyDeclarationsHighlighting) &&
                           (h.ToString().Contains("MethodWithAcceptableStatementCount") ||
                            h.ToString().Contains("MethodWithAcceptableDeclarationCount")))
                .ToList();

            Assert.AreEqual(0, acceptableMethodViolations.Count);
        }

        [Test]
        public void Should_Have_Correct_Statement_Highlighting_Message()
        {
            var highlightings = RunInspection("MethodTooLongTestData");
            var methodTooLongHighlighting = highlightings
                .OfType<MethodTooLongHighlighting>()
                .FirstOrDefault();

            Assert.IsNotNull(methodTooLongHighlighting);
            var message = methodTooLongHighlighting.ToolTip;
            Assert.IsTrue(message.Contains("method is too long"));
        }

        [Test]
        public void Should_Have_Correct_Declaration_Highlighting_Message()
        {
            var highlightings = RunInspection("MethodTooLongTestData");
            var methodTooManyDeclarationsHighlighting = highlightings
                .OfType<MethodTooManyDeclarationsHighlighting>()
                .FirstOrDefault();

            Assert.IsNotNull(methodTooManyDeclarationsHighlighting);
            var message = methodTooManyDeclarationsHighlighting.ToolTip;
            Assert.IsTrue(message.Contains("too many declarations"));
        }
    }
}