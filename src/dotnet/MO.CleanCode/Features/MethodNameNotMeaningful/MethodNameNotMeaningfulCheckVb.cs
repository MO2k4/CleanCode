using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.VB.Tree;

namespace CleanCode.Features.MethodNameNotMeaningful
{
    [ElementProblemAnalyzer(typeof(IMethodDeclaration), HighlightingTypes = new[]
    {
        typeof(MethodNameNotMeaningfulHighlighting)
    })]
    public class MethodNameNotMeaningfulCheckVb : MethodNameNotMeaningfulCheck<IMethodDeclaration>
    {
        protected override void Run(IMethodDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            CheckAndAddHighlighting(element, data, consumer);
        }
    }
}