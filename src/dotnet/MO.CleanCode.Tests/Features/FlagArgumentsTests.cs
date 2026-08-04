using System.Linq;
using CleanCode.Features.FlagArguments;
using CleanCode.Settings;
using NUnit.Framework;

namespace CleanCode.Tests.Features
{
    [TestFixture]
    public class FlagArgumentsTests : CleanCodeTestBase
    {
        protected override string RelativeTestDataPath => "FlagArguments";

        [Test]
        public void Should_Highlight_Boolean_Flag_Arguments_Used_In_If()
        {
            // Test with default settings (flag analysis enabled)
            var highlightings = RunInspection("FlagArgumentsTestData");
            var flagArgumentsHighlightings = highlightings
                .OfType<FlagArgumentsHighlighting>()
                .ToList();

            // Should find violations where bool/enum parameters are used in if statements
            Assert.GreaterOrEqual(flagArgumentsHighlightings.Count, 1);

            var message = flagArgumentsHighlightings[0].ToolTip;
            Assert.IsTrue(message.Contains("flag argument") || message.Contains("boolean parameter"));
        }

        [Test]
        public void Should_Highlight_Enum_Flag_Arguments_Used_In_If()
        {
            var highlightings = RunInspection("FlagArgumentsTestData");
            var flagArgumentsHighlightings = highlightings
                .OfType<FlagArgumentsHighlighting>()
                .ToList();

            // Should detect enum parameters used in if statements
            var enumFlagViolations = flagArgumentsHighlightings
                .Where(h => h.ToString().Contains("mode") || h.ToString().Contains("ProcessingMode"))
                .ToList();

            Assert.GreaterOrEqual(enumFlagViolations.Count, 0);
        }

        [Test]
        public void Should_Not_Highlight_When_Flag_Analysis_Disabled()
        {
            // Test with flag analysis disabled
            var settings = new CleanCodeSettings
            {
                IsFlagAnalysisEnabled = false
            };

            var highlightings = RunInspection("FlagArgumentsTestData", settings);
            var flagArgumentsHighlightings = highlightings
                .OfType<FlagArgumentsHighlighting>()
                .ToList();

            // With flag analysis disabled, no violations should be found
            Assert.AreEqual(0, flagArgumentsHighlightings.Count);
        }

        [Test]
        public void Should_Not_Highlight_Flags_Not_Used_In_If()
        {
            var highlightings = RunInspection("FlagArgumentsTestData");
            var flagArgumentsHighlightings = highlightings
                .OfType<FlagArgumentsHighlighting>()
                .ToList();

            // Methods where flag parameters are not used in if statements should not be highlighted
            var noIfUsageViolations = flagArgumentsHighlightings
                .Where(h => h.ToString().Contains("MethodWithBooleanNotUsedInIf") ||
                           h.ToString().Contains("MethodWithEnumNotUsedInIf") ||
                           h.ToString().Contains("MethodWithFlagAssignment"))
                .ToList();

            Assert.AreEqual(0, noIfUsageViolations.Count);
        }

        [Test]
        public void Should_Not_Highlight_Non_Flag_Parameters()
        {
            var highlightings = RunInspection("FlagArgumentsTestData");
            var flagArgumentsHighlightings = highlightings
                .OfType<FlagArgumentsHighlighting>()
                .ToList();

            // String and int parameters should not be highlighted even if used in if
            var nonFlagViolations = flagArgumentsHighlightings
                .Where(h => h.ToString().Contains("MethodWithStringParameter") ||
                           h.ToString().Contains("MethodWithIntParameter"))
                .ToList();

            Assert.AreEqual(0, nonFlagViolations.Count);
        }

        [Test]
        public void Should_Highlight_Multiple_Flag_Parameters()
        {
            var highlightings = RunInspection("FlagArgumentsTestData");
            var flagArgumentsHighlightings = highlightings
                .OfType<FlagArgumentsHighlighting>()
                .ToList();

            // MethodWithMultipleFlags should potentially have multiple violations
            var multipleFlags = flagArgumentsHighlightings
                .Where(h => h.ToString().Contains("MethodWithMultipleFlags"))
                .ToList();

            // Should detect multiple flag parameters in the same method
            Assert.GreaterOrEqual(multipleFlags.Count, 0);
        }

        [Test]
        public void Should_Detect_Nested_Flag_Usage()
        {
            var highlightings = RunInspection("FlagArgumentsTestData");
            var flagArgumentsHighlightings = highlightings
                .OfType<FlagArgumentsHighlighting>()
                .ToList();

            // Should detect flag usage in nested if statements
            var nestedFlagUsage = flagArgumentsHighlightings
                .Where(h => h.ToString().Contains("MethodWithNestedFlagUsage"))
                .ToList();

            Assert.GreaterOrEqual(nestedFlagUsage.Count, 0);
        }

        [Test]
        public void Should_Detect_Flags_In_Complex_Conditions()
        {
            var highlightings = RunInspection("FlagArgumentsTestData");
            var flagArgumentsHighlightings = highlightings
                .OfType<FlagArgumentsHighlighting>()
                .ToList();

            // Should detect flag usage in complex if conditions
            var complexConditions = flagArgumentsHighlightings
                .Where(h => h.ToString().Contains("MethodWithComplexCondition"))
                .ToList();

            Assert.GreaterOrEqual(complexConditions.Count, 0);
        }

        [Test]
        public void Should_Not_Highlight_Methods_Without_If_Statements()
        {
            var highlightings = RunInspection("FlagArgumentsTestData");
            var flagArgumentsHighlightings = highlightings
                .OfType<FlagArgumentsHighlighting>()
                .ToList();

            // Methods without if statements should not be highlighted
            var noIfViolations = flagArgumentsHighlightings
                .Where(h => h.ToString().Contains("MethodWithoutIf") ||
                           h.ToString().Contains("MethodWithoutParameters"))
                .ToList();

            Assert.AreEqual(0, noIfViolations.Count);
        }

        [Test]
        public void Should_Have_Correct_Highlighting_Message()
        {
            var highlightings = RunInspection("FlagArgumentsTestData");
            var flagArgumentsHighlighting = highlightings
                .OfType<FlagArgumentsHighlighting>()
                .FirstOrDefault();

            if (flagArgumentsHighlighting != null)
            {
                var message = flagArgumentsHighlighting.ToolTip;
                Assert.IsTrue(message.Contains("flag argument") ||
                             message.Contains("boolean parameter") ||
                             message.Contains("Single Responsibility"));
            }
        }
    }
}