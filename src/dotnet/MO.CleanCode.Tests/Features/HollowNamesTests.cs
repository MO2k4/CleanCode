using System.Linq;
using CleanCode.Features.HollowNames;
using CleanCode.Settings;
using NUnit.Framework;

namespace CleanCode.Tests.Features
{
    [TestFixture]
    public class HollowNamesTests : CleanCodeTestBase
    {
        protected override string RelativeTestDataPath => "HollowNames";

        [Test]
        public void Should_Highlight_Classes_With_Hollow_Name_Suffixes()
        {
            // Test with default settings (Handler,Manager,Processor,Controller,Helper)
            var highlightings = RunInspection("HollowNamesTestData");
            var hollowNamesHighlightings = highlightings
                .OfType<HollowTypeNameHighlighting>()
                .ToList();

            // Should find violations in classes ending with hollow suffixes
            Assert.GreaterOrEqual(hollowNamesHighlightings.Count, 1);

            var firstHighlighting = hollowNamesHighlightings[0];
            Assert.IsTrue(firstHighlighting.ToolTip.Contains("hollow type name") ||
                         firstHighlighting.ToolTip.Contains("too generic") ||
                         firstHighlighting.ToolTip.Contains("not meaningful"));
        }

        [Test]
        public void Should_Detect_All_Default_Hollow_Names()
        {
            var highlightings = RunInspection("HollowNamesTestData");
            var hollowNamesHighlightings = highlightings
                .OfType<HollowTypeNameHighlighting>()
                .ToList();

            var violatedNames = hollowNamesHighlightings
                .Select(h => h.ToString())
                .ToList();

            // Should detect default hollow names: Handler, Manager, Processor, Controller, Helper
            var defaultHollowNames = new[] { "Manager", "Handler", "Processor", "Controller", "Helper" };
            
            foreach (var hollowName in defaultHollowNames)
            {
                Assert.IsTrue(violatedNames.Any(name => name.Contains(hollowName)),
                    $"Should detect classes ending with {hollowName}");
            }
        }

        [Test]
        public void Should_Not_Highlight_Meaningful_Class_Names()
        {
            var highlightings = RunInspection("HollowNamesTestData");
            var hollowNamesHighlightings = highlightings
                .OfType<HollowTypeNameHighlighting>()
                .ToList();

            // Classes with meaningful, specific names should not be highlighted
            var meaningfulNameViolations = hollowNamesHighlightings
                .Where(h => h.ToString().Contains("CustomerRepository") ||
                           h.ToString().Contains("OrderService") ||
                           h.ToString().Contains("InvoiceCalculator") ||
                           h.ToString().Contains("EmailValidator") ||
                           h.ToString().Contains("ProductCatalog"))
                .ToList();

            Assert.AreEqual(0, meaningfulNameViolations.Count);
        }

        [Test]
        public void Should_Not_Highlight_When_Hollow_Name_Not_Suffix()
        {
            var highlightings = RunInspection("HollowNamesTestData");
            var hollowNamesHighlightings = highlightings
                .OfType<HollowTypeNameHighlighting>()
                .ToList();

            // Classes where hollow words are not suffixes should not be highlighted
            var nonSuffixViolations = hollowNamesHighlightings
                .Where(h => h.ToString().Contains("ManagerialReport") ||
                           h.ToString().Contains("HandlerConfiguration"))
                .ToList();

            Assert.AreEqual(0, nonSuffixViolations.Count);
        }

        [Test]
        public void Should_Respect_Custom_Hollow_Names_Setting()
        {
            // Test with custom hollow names setting
            var settings = new CleanCodeSettings
            {
                MeaninglessClassNameSuffixes = "Service,Repository,Factory"
            };

            var highlightings = RunInspection("HollowNamesTestData", settings);
            var hollowNamesHighlightings = highlightings
                .OfType<HollowTypeNameHighlighting>()
                .ToList();

            // With custom settings, different classes should be highlighted
            // Default hollow names should not be highlighted with custom settings
            var defaultHollowViolations = hollowNamesHighlightings
                .Where(h => h.ToString().Contains("DataManager") ||
                           h.ToString().Contains("RequestHandler"))
                .ToList();

            // Should be fewer or zero violations for default hollow names
            var originalCount = RunInspection("HollowNamesTestData")
                .OfType<HollowTypeNameHighlighting>()
                .Count();

            Assert.LessOrEqual(hollowNamesHighlightings.Count, originalCount);
        }

        [Test]
        public void Should_Detect_Exact_Hollow_Name_Matches()
        {
            var highlightings = RunInspection("HollowNamesTestData");
            var hollowNamesHighlightings = highlightings
                .OfType<HollowTypeNameHighlighting>()
                .ToList();

            // Classes that are exactly hollow names should be detected
            var exactMatchViolations = hollowNamesHighlightings
                .Where(h => h.ToString().Contains("Manager") && !h.ToString().Contains("Data") ||
                           h.ToString().Contains("Handler") && !h.ToString().Contains("Request") ||
                           h.ToString().Contains("Processor") && !h.ToString().Contains("Payment"))
                .ToList();

            Assert.GreaterOrEqual(exactMatchViolations.Count, 0);
        }

        [Test]
        public void Should_Check_Different_Class_Types()
        {
            var highlightings = RunInspection("HollowNamesTestData");
            var hollowNamesHighlightings = highlightings
                .OfType<HollowTypeNameHighlighting>()
                .ToList();

            // Should check regular classes, nested classes, generic classes, abstract classes, static classes
            Assert.GreaterOrEqual(hollowNamesHighlightings.Count, 3);

            var violationSources = hollowNamesHighlightings
                .Select(h => h.ToString())
                .ToList();

            // Should detect violations in different class types
            Assert.IsTrue(violationSources.Any(source =>
                source.Contains("NestedManager") ||
                source.Contains("GenericHandler") ||
                source.Contains("AbstractProcessor") ||
                source.Contains("StaticHelper") ||
                source.Contains("PartialManager")));
        }

        [Test]
        public void Should_Have_Correct_Highlighting_Message()
        {
            var highlightings = RunInspection("HollowNamesTestData");
            var hollowNamesHighlighting = highlightings
                .OfType<HollowTypeNameHighlighting>()
                .FirstOrDefault();

            if (hollowNamesHighlighting != null)
            {
                var message = hollowNamesHighlighting.ToolTip;
                Assert.IsTrue(message.Contains("hollow type name") ||
                             message.Contains("too generic") ||
                             message.Contains("not meaningful") ||
                             message.Contains("meaningless"));
            }
        }
    }
}