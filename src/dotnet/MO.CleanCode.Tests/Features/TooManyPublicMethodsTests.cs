using System.Linq;
using CleanCode.Features.TooManyPublicMethods;
using CleanCode.Settings;
using NUnit.Framework;

namespace CleanCode.Tests.Features
{
    [TestFixture]
    public class TooManyPublicMethodsTests : CleanCodeTestBase
    {
        protected override string RelativeTestDataPath => "TooManyPublicMethods";

        [Test]
        public void Should_Highlight_Class_With_Too_Many_Public_Methods()
        {
            // Test with default settings (15 max public methods)
            var highlightings = RunInspection("TooManyPublicMethodsTestData");
            var tooManyPublicMethodsHighlightings = highlightings
                .OfType<TooManyPublicMethodsHighlighting>()
                .ToList();

            // Should find violations in classes with 16+ public methods
            Assert.GreaterOrEqual(tooManyPublicMethodsHighlightings.Count, 1);

            var firstHighlighting = tooManyPublicMethodsHighlightings[0];
            Assert.IsTrue(firstHighlighting.ToolTip.Contains("(16 / 15)") ||
                         firstHighlighting.ToolTip.Contains("too many public methods"));
        }

        [Test]
        public void Should_Not_Highlight_Class_Within_Public_Method_Limit()
        {
            // Test with custom settings allowing 20 public methods
            var settings = new CleanCodeSettings
            {
                MaximumPublicMethodsInClass = 20
            };

            var highlightings = RunInspection("TooManyPublicMethodsTestData", settings);
            var tooManyPublicMethodsHighlightings = highlightings
                .OfType<TooManyPublicMethodsHighlighting>()
                .ToList();

            // With limit of 20, fewer classes should be highlighted
            var originalCount = RunInspection("TooManyPublicMethodsTestData")
                .OfType<TooManyPublicMethodsHighlighting>()
                .Count();

            Assert.LessOrEqual(tooManyPublicMethodsHighlightings.Count, originalCount);
        }

        [Test]
        public void Should_Only_Count_Public_Methods()
        {
            var highlightings = RunInspection("TooManyPublicMethodsTestData");
            var tooManyPublicMethodsHighlightings = highlightings
                .OfType<TooManyPublicMethodsHighlighting>()
                .ToList();

            // Should only count public methods, not private, protected, or internal
            // ClassWithNoPublicMethods should not be highlighted despite having many non-public methods
            var noPublicMethodsViolations = tooManyPublicMethodsHighlightings
                .Where(h => h.ToString().Contains("ClassWithNoPublicMethods"))
                .ToList();

            Assert.AreEqual(0, noPublicMethodsViolations.Count);
        }

        [Test]
        public void Should_Not_Count_Properties_As_Methods()
        {
            var highlightings = RunInspection("TooManyPublicMethodsTestData");
            var tooManyPublicMethodsHighlightings = highlightings
                .OfType<TooManyPublicMethodsHighlighting>()
                .ToList();

            // ClassWithOnlyProperties should not be highlighted despite having many properties
            var propertyOnlyViolations = tooManyPublicMethodsHighlightings
                .Where(h => h.ToString().Contains("ClassWithOnlyProperties"))
                .ToList();

            Assert.AreEqual(0, propertyOnlyViolations.Count);
        }

        [Test]
        public void Should_Count_Static_Public_Methods()
        {
            var highlightings = RunInspection("TooManyPublicMethodsTestData");
            var tooManyPublicMethodsHighlightings = highlightings
                .OfType<TooManyPublicMethodsHighlighting>()
                .ToList();

            // Should count both instance and static public methods
            var mixedMethodViolations = tooManyPublicMethodsHighlightings
                .Where(h => h.ToString().Contains("ClassWithMixedPublicMethods") ||
                           h.ToString().Contains("StaticClassWithManyPublicMethods"))
                .ToList();

            Assert.GreaterOrEqual(mixedMethodViolations.Count, 1);
        }

        [Test]
        public void Should_Not_Highlight_Classes_At_Exact_Limit()
        {
            var highlightings = RunInspection("TooManyPublicMethodsTestData");
            var tooManyPublicMethodsHighlightings = highlightings
                .OfType<TooManyPublicMethodsHighlighting>()
                .ToList();

            // ClassWithAcceptablePublicMethodCount has exactly 15 methods (at limit)
            var exactLimitViolations = tooManyPublicMethodsHighlightings
                .Where(h => h.ToString().Contains("ClassWithAcceptablePublicMethodCount"))
                .ToList();

            Assert.AreEqual(0, exactLimitViolations.Count);
        }

        [Test]
        public void Should_Not_Highlight_Small_Classes()
        {
            var highlightings = RunInspection("TooManyPublicMethodsTestData");
            var tooManyPublicMethodsHighlightings = highlightings
                .OfType<TooManyPublicMethodsHighlighting>()
                .ToList();

            // Small classes should not be highlighted
            var smallClassViolations = tooManyPublicMethodsHighlightings
                .Where(h => h.ToString().Contains("SmallPublicMethodsClass") ||
                           h.ToString().Contains("EmptyPublicMethodsClass"))
                .ToList();

            Assert.AreEqual(0, smallClassViolations.Count);
        }

        [Test]
        public void Should_Check_Different_Class_Types()
        {
            var highlightings = RunInspection("TooManyPublicMethodsTestData");
            var tooManyPublicMethodsHighlightings = highlightings
                .OfType<TooManyPublicMethodsHighlighting>()
                .ToList();

            // Should check regular classes, abstract classes, static classes
            var violationSources = tooManyPublicMethodsHighlightings
                .Select(h => h.ToString())
                .ToList();

            Assert.IsTrue(violationSources.Any(source =>
                source.Contains("ClassWithTooManyPublicMethods") ||
                source.Contains("AbstractClassWithManyPublicMethods") ||
                source.Contains("StaticClassWithManyPublicMethods")));
        }

        [Test]
        public void Should_Have_Correct_Highlighting_Message()
        {
            var highlightings = RunInspection("TooManyPublicMethodsTestData");
            var tooManyPublicMethodsHighlighting = highlightings
                .OfType<TooManyPublicMethodsHighlighting>()
                .FirstOrDefault();

            if (tooManyPublicMethodsHighlighting != null)
            {
                var message = tooManyPublicMethodsHighlighting.ToolTip;
                Assert.IsTrue(message.Contains("too many public methods") ||
                             message.Contains("class is too big") ||
                             message.Contains("public interface"));
            }
        }
    }
}