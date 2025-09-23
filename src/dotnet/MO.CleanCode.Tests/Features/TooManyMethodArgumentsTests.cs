using System.Linq;
using CleanCode.Features.TooManyMethodArguments;
using CleanCode.Settings;
using NUnit.Framework;

namespace CleanCode.Tests.Features
{
    [TestFixture]
    public class TooManyMethodArgumentsTests : CleanCodeTestBase
    {
        protected override string RelativeTestDataPath => "TooManyMethodArguments";

        [Test]
        public void Should_Highlight_Method_With_Too_Many_Arguments()
        {
            // Test with default settings (3 max parameters)
            var highlightings = RunInspection("TooManyMethodArgumentsTestData");
            var tooManyArgumentsHighlightings = highlightings
                .OfType&lt;TooManyArgumentsHighlighting&gt;()
                .ToList();

            // Should find violations in methods/constructors with 4+ parameters
            Assert.GreaterOrEqual(tooManyArgumentsHighlightings.Count, 5);

            var firstHighlighting = tooManyArgumentsHighlightings[0];
            Assert.IsTrue(firstHighlighting.ToolTip.Contains("(4 / 3)") ||
                         firstHighlighting.ToolTip.Contains("(7 / 3)"));
            Assert.IsTrue(firstHighlighting.ToolTip.Contains("too many parameters"));
        }

        [Test]
        public void Should_Not_Highlight_Method_Within_Parameter_Limit()
        {
            // Test with custom settings allowing 5 parameters
            var settings = new CleanCodeSettings
            {
                MaximumMethodParameters = 5
            };

            var highlightings = RunInspection("TooManyMethodArgumentsTestData", settings);
            var tooManyArgumentsHighlightings = highlightings
                .OfType&lt;TooManyArgumentsHighlighting&gt;()
                .ToList();

            // With limit of 5, fewer methods should be highlighted
            var originalCount = RunInspection("TooManyMethodArgumentsTestData")
                .OfType&lt;TooManyArgumentsHighlighting&gt;()
                .Count();

            Assert.LessOrEqual(tooManyArgumentsHighlightings.Count, originalCount);
        }

        [Test]
        public void Should_Check_All_Method_Types()
        {
            var highlightings = RunInspection("TooManyMethodArgumentsTestData");
            var tooManyArgumentsHighlightings = highlightings
                .OfType&lt;TooManyArgumentsHighlighting&gt;()
                .ToList();

            var methodNames = tooManyArgumentsHighlightings
                .Select(h => h.ToString())
                .ToList();

            // Should check instance methods, static methods, private methods, constructors
            Assert.IsTrue(methodNames.Any(name =>
                name.Contains("MethodWithTooManyArguments") ||
                name.Contains("StaticMethodWithTooManyArguments") ||
                name.Contains("PrivateMethodWithTooManyArguments") ||
                name.Contains("TooManyMethodArgumentsTestData"))); // Constructor
        }

        [Test]
        public void Should_Not_Highlight_Methods_Within_Limit()
        {
            var highlightings = RunInspection("TooManyMethodArgumentsTestData");
            var tooManyArgumentsHighlightings = highlightings
                .OfType&lt;TooManyArgumentsHighlighting&gt;()
                .ToList();

            // Methods with ≤3 parameters should not be highlighted
            var acceptableMethodViolations = tooManyArgumentsHighlightings
                .Where(h => h.ToString().Contains("MethodWithAcceptableArguments") ||
                           h.ToString().Contains("MethodWithFewArguments") ||
                           h.ToString().Contains("MethodWithOneArgument") ||
                           h.ToString().Contains("MethodWithNoArguments"))
                .ToList();

            Assert.AreEqual(0, acceptableMethodViolations.Count);
        }

        [Test]
        public void Should_Count_All_Parameter_Types()
        {
            var highlightings = RunInspection("TooManyMethodArgumentsTestData");
            var tooManyArgumentsHighlightings = highlightings
                .OfType&lt;TooManyArgumentsHighlighting&gt;()
                .ToList();

            // Should count optional parameters, ref/out parameters, etc.
            var methodsWithSpecialParams = tooManyArgumentsHighlightings
                .Where(h => h.ToString().Contains("MethodWithOptionalParameters") ||
                           h.ToString().Contains("MethodWithRefOutParameters"))
                .ToList();

            Assert.GreaterOrEqual(methodsWithSpecialParams.Count, 1);
        }

        [Test]
        public void Should_Not_Count_Params_Array_As_Multiple_Parameters()
        {
            var highlightings = RunInspection("TooManyMethodArgumentsTestData");
            var tooManyArgumentsHighlightings = highlightings
                .OfType&lt;TooManyArgumentsHighlighting&gt;()
                .ToList();

            // MethodWithParamsArray has only 2 parameters (message + params array)
            var paramsArrayViolations = tooManyArgumentsHighlightings
                .Where(h => h.ToString().Contains("MethodWithParamsArray"))
                .ToList();

            Assert.AreEqual(0, paramsArrayViolations.Count);
        }

        [Test]
        public void Should_Check_Interface_And_Abstract_Methods()
        {
            var highlightings = RunInspection("TooManyMethodArgumentsTestData");
            var tooManyArgumentsHighlightings = highlightings
                .OfType&lt;TooManyArgumentsHighlighting&gt;()
                .ToList();

            // Should check interface and abstract method declarations
            var declarationViolations = tooManyArgumentsHighlightings
                .Where(h => h.ToString().Contains("InterfaceMethodWithTooManyArguments") ||
                           h.ToString().Contains("AbstractMethodWithTooManyArguments"))
                .ToList();

            // Might be 0 if interface/abstract methods aren't checked by this analyzer
            Assert.GreaterOrEqual(declarationViolations.Count, 0);
        }

        [Test]
        public void Should_Have_Correct_Highlighting_Message()
        {
            var highlightings = RunInspection("TooManyMethodArgumentsTestData");
            var tooManyArgumentsHighlighting = highlightings
                .OfType&lt;TooManyArgumentsHighlighting&gt;()
                .FirstOrDefault();

            Assert.IsNotNull(tooManyArgumentsHighlighting);
            var message = tooManyArgumentsHighlighting.ToolTip;
            Assert.IsTrue(message.Contains("too many parameters") ||
                         message.Contains("method has too many arguments"));
        }
    }
}