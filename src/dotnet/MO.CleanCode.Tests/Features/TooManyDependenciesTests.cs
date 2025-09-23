using System.Linq;
using CleanCode.Features.TooManyDependencies;
using CleanCode.Settings;
using NUnit.Framework;

namespace CleanCode.Tests.Features
{
    [TestFixture]
    public class TooManyDependenciesTests : CleanCodeTestBase
    {
        protected override string RelativeTestDataPath => "TooManyDependencies";

        [Test]
        public void Should_Highlight_Constructor_With_Too_Many_Interface_Dependencies()
        {
            // Test with default settings (3 max dependencies)
            var highlightings = RunInspection("TooManyDependenciesTestData");
            var tooManyDependenciesHighlightings = highlightings
                .OfType&lt;TooManyDependenciesHighlighting&gt;()
                .ToList();

            // Should find violations in:
            // - ClassWithTooManyDependencies (5 interfaces > 3 default)
            // - ClassWithMixedDependencies (5 interfaces > 3 default)
            Assert.AreEqual(2, tooManyDependenciesHighlightings.Count);

            var firstHighlighting = tooManyDependenciesHighlightings[0];
            Assert.IsTrue(firstHighlighting.ToolTip.Contains("(5 / 3)"));
            Assert.IsTrue(firstHighlighting.ToolTip.Contains("too many interfaces"));
        }

        [Test]
        public void Should_Not_Highlight_Constructor_Within_Dependency_Limit()
        {
            // Test with custom settings allowing 5 dependencies
            var settings = new CleanCodeSettings
            {
                MaximumConstructorDependencies = 5
            };

            var highlightings = RunInspection("TooManyDependenciesTestData", settings);
            var tooManyDependenciesHighlightings = highlightings
                .OfType&lt;TooManyDependenciesHighlighting&gt;()
                .ToList();

            // With limit of 5, no constructors should be highlighted
            Assert.AreEqual(0, tooManyDependenciesHighlightings.Count);
        }

        [Test]
        public void Should_Only_Count_Interface_Dependencies()
        {
            // Test that concrete types don't count towards dependency limit
            var highlightings = RunInspection("TooManyDependenciesTestData");
            var tooManyDependenciesHighlightings = highlightings
                .OfType&lt;TooManyDependenciesHighlighting&gt;()
                .ToList();

            // ClassWithConcreteTypes has 6 concrete dependencies but should not be highlighted
            // as only interfaces count
            var concreteTypeViolations = tooManyDependenciesHighlightings
                .Where(h => h.ToString().Contains("ClassWithConcreteTypes"))
                .ToList();

            Assert.AreEqual(0, concreteTypeViolations.Count);
        }

        [Test]
        public void Should_Not_Highlight_Empty_Constructor()
        {
            var highlightings = RunInspection("TooManyDependenciesTestData");
            var tooManyDependenciesHighlightings = highlightings
                .OfType&lt;TooManyDependenciesHighlighting&gt;()
                .ToList();

            // ClassWithNoConstructor should not have any violations
            var emptyConstructorViolations = tooManyDependenciesHighlightings
                .Where(h => h.ToString().Contains("ClassWithNoConstructor"))
                .ToList();

            Assert.AreEqual(0, emptyConstructorViolations.Count);
        }

        [Test]
        public void Should_Have_Correct_Highlighting_Message()
        {
            var highlightings = RunInspection("TooManyDependenciesTestData");
            var tooManyDependenciesHighlighting = highlightings
                .OfType&lt;TooManyDependenciesHighlighting&gt;()
                .FirstOrDefault();

            Assert.IsNotNull(tooManyDependenciesHighlighting);
            var message = tooManyDependenciesHighlighting.ToolTip;
            Assert.IsTrue(message.Contains("constructor has too many dependencies"));
        }
    }
}