using System.Linq;
using CleanCode.Features.ClassTooBig;
using CleanCode.Settings;
using NUnit.Framework;

namespace CleanCode.Tests.Features
{
    [TestFixture]
    public class ClassTooBigTests : CleanCodeTestBase
    {
        protected override string RelativeTestDataPath => "ClassTooBig";

        [Test]
        public void Should_Highlight_Class_With_Too_Many_Methods()
        {
            // Test with default settings (20 max methods)
            var highlightings = RunInspection("ClassTooBigTestData");
            var classTooBigHighlightings = highlightings
                .OfType<ClassTooBigHighlighting>()
                .ToList();

            // Should find violation in ClassWithTooManyMethods (21 methods > 20 default)
            Assert.AreEqual(1, classTooBigHighlightings.Count);

            var highlighting = classTooBigHighlightings[0];
            Assert.IsTrue(highlighting.ToolTip.Contains("(21 / 20)"));
            Assert.IsTrue(highlighting.ToolTip.Contains("too many methods"));
        }

        [Test]
        public void Should_Not_Highlight_Class_Within_Method_Limit()
        {
            // Test with custom settings allowing 25 methods
            var settings = new CleanCodeSettings
            {
                MaximumMethodsInClass = 25
            };

            var highlightings = RunInspection("ClassTooBigTestData", settings);
            var classTooBigHighlightings = highlightings
                .OfType<ClassTooBigHighlighting>()
                .ToList();

            // With limit of 25, no classes should be highlighted
            Assert.AreEqual(0, classTooBigHighlightings.Count);
        }

        [Test]
        public void Should_Not_Count_Properties_And_Fields()
        {
            var highlightings = RunInspection("ClassTooBigTestData");
            var classTooBigHighlightings = highlightings
                .OfType<ClassTooBigHighlighting>()
                .ToList();

            // ClassWithMixedMembers has properties and fields but only 3 methods
            // It should not be highlighted
            var mixedMembersViolations = classTooBigHighlightings
                .Where(h => h.ToString().Contains("ClassWithMixedMembers"))
                .ToList();

            Assert.AreEqual(0, mixedMembersViolations.Count);
        }

        [Test]
        public void Should_Not_Highlight_Empty_Class()
        {
            var highlightings = RunInspection("ClassTooBigTestData");
            var classTooBigHighlightings = highlightings
                .OfType<ClassTooBigHighlighting>()
                .ToList();

            // EmptyClass should not have any violations
            var emptyClassViolations = classTooBigHighlightings
                .Where(h => h.ToString().Contains("EmptyClass"))
                .ToList();

            Assert.AreEqual(0, emptyClassViolations.Count);
        }

        [Test]
        public void Should_Not_Highlight_Small_Classes()
        {
            var highlightings = RunInspection("ClassTooBigTestData");
            var classTooBigHighlightings = highlightings
                .OfType<ClassTooBigHighlighting>()
                .ToList();

            // SmallClass and ClassWithAcceptableMethodCount should not be highlighted
            var smallClassViolations = classTooBigHighlightings
                .Where(h => h.ToString().Contains("SmallClass") || h.ToString().Contains("ClassWithAcceptableMethodCount"))
                .ToList();

            Assert.AreEqual(0, smallClassViolations.Count);
        }

        [Test]
        public void Should_Have_Correct_Highlighting_Message()
        {
            var highlightings = RunInspection("ClassTooBigTestData");
            var classTooBigHighlighting = highlightings
                .OfType<ClassTooBigHighlighting>()
                .FirstOrDefault();

            Assert.IsNotNull(classTooBigHighlighting);
            var message = classTooBigHighlighting.ToolTip;
            Assert.IsTrue(message.Contains("class is too big"));
        }
    }
}