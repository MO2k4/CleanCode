using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace CleanCode.Features.MethodNameNotMeaningful
{
    [ElementProblemAnalyzer(typeof(IMethodDeclaration), HighlightingTypes = new[]
    {
        typeof(MethodNameNotMeaningfulHighlighting)
    })]
    public class MethodNameNotMeaningfulCheckCs : MethodNameNotMeaningfulCheck<IMethodDeclaration>
    {
        protected override void Run(IMethodDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            CheckAndAddHighlighting(element, data, consumer);
        }
    }
}