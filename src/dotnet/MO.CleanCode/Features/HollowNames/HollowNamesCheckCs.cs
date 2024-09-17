using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace CleanCode.Features.HollowNames
{
    [ElementProblemAnalyzer(typeof(IClassDeclaration),
        HighlightingTypes = new[]
        {
            typeof(HollowTypeNameHighlighting)
        })]
    public class HollowNamesCheckCs : HollowNamesCheck<IClassDeclaration>
    {
        protected override void Run(IClassDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            CheckAndAddHighlighting(element, data, consumer);
        }
    }
}