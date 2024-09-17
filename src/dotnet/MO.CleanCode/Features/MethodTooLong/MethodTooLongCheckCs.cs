using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace CleanCode.Features.MethodTooLong
{
    [ElementProblemAnalyzer(typeof(IMethodDeclaration), HighlightingTypes = new[]
    {
        typeof(MethodTooLongHighlighting)
    })]
    public class MethodTooLongCheckCs : MethodTooLongCheck<IMethodDeclaration>
    {
        protected override void Run(IMethodDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            CheckAndAddHighlight(element, data, consumer);
        }
    }
}