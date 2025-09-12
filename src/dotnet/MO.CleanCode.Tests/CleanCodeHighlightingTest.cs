using CleanCode.Features.ChainedReferences;
using CleanCode.Features.ClassTooBig;
using CleanCode.Features.ComplexExpression;
using CleanCode.Features.ExcessiveIndentation;
using CleanCode.Features.FlagArguments;
using CleanCode.Features.HollowNames;
using CleanCode.Features.MethodNameNotMeaningful;
using CleanCode.Features.MethodTooLong;
using CleanCode.Features.TooManyDependencies;
using CleanCode.Features.TooManyMethodArguments;
using CleanCode.Features.TooManyPublicMethods;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.FeaturesTestFramework.Daemon;
using JetBrains.ReSharper.Psi;
using NUnit.Framework;

namespace CleanCode.Tests
{
    public class CleanCodeHighlightingTest : CSharpHighlightingTestBase
    {
        protected override string RelativeTestDataPath => "CSharp";

        protected override bool HighlightingPredicate(
            IHighlighting highlighting,
            IPsiSourceFile sourceFile,
            IContextBoundSettingsStore settingsStore
        )
        {
            return highlighting is MethodTooLongHighlighting
                || highlighting is MethodTooManyDeclarationsHighlighting
                || highlighting is MaximumChainedReferencesHighlighting
                || highlighting is ClassTooBigHighlighting
                || highlighting is ComplexConditionExpressionHighlighting
                || highlighting is ExcessiveIndentHighlighting
                || highlighting is FlagArgumentsHighlighting
                || highlighting is HollowTypeNameHighlighting
                || highlighting is MethodNameNotMeaningfulHighlighting
                || highlighting is TooManyDependenciesHighlighting
                || highlighting is TooManyArgumentsHighlighting
                || highlighting is TooManyPublicMethodsHighlighting;
        }

        [Test]
        public void TestMethodTooLongTest()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestChainedReferencesTest()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestClassTooBigTest()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestComplexExpressionTest()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestExcessiveIndentationTest()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestFlagArgumentsTest()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestHollowNamesTest()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestMethodNameNotMeaningfulTest()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestTooManyDependenciesTest()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestTooManyMethodArgumentsTest()
        {
            DoNamedTest2();
        }

        [Test]
        public void TestTooManyPublicMethodsTest()
        {
            DoNamedTest2();
        }
    }
}
