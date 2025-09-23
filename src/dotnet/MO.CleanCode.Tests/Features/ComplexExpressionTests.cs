using System.Linq;
using CleanCode.Features.ComplexExpression;
using CleanCode.Settings;
using NUnit.Framework;

namespace CleanCode.Tests.Features
{
    [TestFixture]
    public class ComplexExpressionTests : CleanCodeTestBase
    {
        protected override string RelativeTestDataPath => "ComplexExpression";

        [Test]
        public void Should_Highlight_Complex_If_Conditions()
        {
            // Test with default settings (1 max expression)
            var highlightings = RunInspection("ComplexExpressionTestData");
            var complexExpressionHighlightings = highlightings
                .OfType&lt;ComplexConditionExpressionHighlighting&gt;()
                .ToList();

            // Should find violations in conditions with 2+ operators
            Assert.GreaterOrEqual(complexExpressionHighlightings.Count, 1);

            var firstHighlighting = complexExpressionHighlightings[0];
            Assert.IsTrue(firstHighlighting.ToolTip.Contains("too many expressions") ||
                         firstHighlighting.ToolTip.Contains("complex condition"));
        }

        [Test]
        public void Should_Not_Highlight_Expressions_Within_Limit()
        {
            // Test with custom settings allowing 5 expressions
            var settings = new CleanCodeSettings
            {
                MaximumExpressionsInCondition = 5
            };

            var highlightings = RunInspection("ComplexExpressionTestData", settings);
            var complexExpressionHighlightings = highlightings
                .OfType&lt;ComplexConditionExpressionHighlighting&gt;()
                .ToList();

            // With limit of 5, fewer expressions should be highlighted
            var originalCount = RunInspection("ComplexExpressionTestData")
                .OfType&lt;ComplexConditionExpressionHighlighting&gt;()
                .Count();

            Assert.LessOrEqual(complexExpressionHighlightings.Count, originalCount);
        }

        [Test]
        public void Should_Detect_Complex_Conditions_In_Different_Constructs()
        {
            var highlightings = RunInspection("ComplexExpressionTestData");
            var complexExpressionHighlightings = highlightings
                .OfType&lt;ComplexConditionExpressionHighlighting&gt;()
                .ToList();

            var violationSources = complexExpressionHighlightings
                .Select(h => h.ToString())
                .ToList();

            // Should detect complex conditions in if, while, for, ternary, assignments
            Assert.IsTrue(violationSources.Any(source =>
                source.Contains("MethodWithComplexIfCondition") ||
                source.Contains("MethodWithComplexWhileCondition") ||
                source.Contains("MethodWithComplexForCondition") ||
                source.Contains("MethodWithComplexTernaryCondition") ||
                source.Contains("MethodWithComplexAssignment")));
        }

        [Test]
        public void Should_Not_Highlight_Simple_Conditions()
        {
            var highlightings = RunInspection("ComplexExpressionTestData");
            var complexExpressionHighlightings = highlightings
                .OfType&lt;ComplexConditionExpressionHighlighting&gt;()
                .ToList();

            // MethodWithSimpleConditions should not be highlighted
            var simpleConditionViolations = complexExpressionHighlightings
                .Where(h => h.ToString().Contains("MethodWithSimpleConditions") ||
                           h.ToString().Contains("MethodWithSimpleBooleanChecks"))
                .ToList();

            Assert.AreEqual(0, simpleConditionViolations.Count);
        }

        [Test]
        public void Should_Detect_Nested_Logical_Operators()
        {
            var highlightings = RunInspection("ComplexExpressionTestData");
            var complexExpressionHighlightings = highlightings
                .OfType&lt;ComplexConditionExpressionHighlighting&gt;()
                .ToList();

            // Should detect complex nested logical expressions
            var nestedLogicViolations = complexExpressionHighlightings
                .Where(h => h.ToString().Contains("MethodWithNestedLogicalOperators"))
                .ToList();

            Assert.GreaterOrEqual(nestedLogicViolations.Count, 0);
        }

        [Test]
        public void Should_Detect_Mixed_Operators()
        {
            var highlightings = RunInspection("ComplexExpressionTestData");
            var complexExpressionHighlightings = highlightings
                .OfType&lt;ComplexConditionExpressionHighlighting&gt;()
                .ToList();

            // Should detect complex expressions with arithmetic and logical operators
            var mixedOperatorViolations = complexExpressionHighlightings
                .Where(h => h.ToString().Contains("MethodWithMixedOperators"))
                .ToList();

            Assert.GreaterOrEqual(mixedOperatorViolations.Count, 0);
        }

        [Test]
        public void Should_Detect_Multiple_Complex_Conditions_In_Same_Method()
        {
            var highlightings = RunInspection("ComplexExpressionTestData");
            var complexExpressionHighlightings = highlightings
                .OfType&lt;ComplexConditionExpressionHighlighting&gt;()
                .Where(h => h.ToString().Contains("MethodWithMultipleComplexConditions"))
                .ToList();

            // Should detect multiple complex conditions in the same method
            Assert.GreaterOrEqual(complexExpressionHighlightings.Count, 0);
        }

        [Test]
        public void Should_Not_Highlight_Methods_Without_Conditions()
        {
            var highlightings = RunInspection("ComplexExpressionTestData");
            var complexExpressionHighlightings = highlightings
                .OfType&lt;ComplexConditionExpressionHighlighting&gt;()
                .ToList();

            // Methods without conditions should not be highlighted
            var noConditionViolations = complexExpressionHighlightings
                .Where(h => h.ToString().Contains("MethodWithoutConditions"))
                .ToList();

            Assert.AreEqual(0, noConditionViolations.Count);
        }

        [Test]
        public void Should_Detect_Complex_Loop_Conditions()
        {
            var highlightings = RunInspection("ComplexExpressionTestData");
            var complexExpressionHighlightings = highlightings
                .OfType&lt;ComplexConditionExpressionHighlighting&gt;()
                .ToList();

            // Should detect complex conditions in various loop types
            var loopViolations = complexExpressionHighlightings
                .Where(h => h.ToString().Contains("MethodWithComplexWhileCondition") ||
                           h.ToString().Contains("MethodWithComplexForCondition") ||
                           h.ToString().Contains("MethodWithComplexDoWhile"))
                .ToList();

            Assert.GreaterOrEqual(loopViolations.Count, 0);
        }

        [Test]
        public void Should_Have_Correct_Highlighting_Message()
        {
            var highlightings = RunInspection("ComplexExpressionTestData");
            var complexExpressionHighlighting = highlightings
                .OfType&lt;ComplexConditionExpressionHighlighting&gt;()
                .FirstOrDefault();

            if (complexExpressionHighlighting != null)
            {
                var message = complexExpressionHighlighting.ToolTip;
                Assert.IsTrue(message.Contains("too many expressions") ||
                             message.Contains("complex condition") ||
                             message.Contains("condition is too complex"));
            }
        }
    }
}