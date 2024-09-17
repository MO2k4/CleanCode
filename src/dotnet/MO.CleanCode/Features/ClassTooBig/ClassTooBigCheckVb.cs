using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.VB.Tree;

namespace CleanCode.Features.ClassTooBig
{
    [ElementProblemAnalyzer(typeof(IClassDeclaration), HighlightingTypes = new[]
    {
        typeof(ClassTooBigHighlighting)
    })]
    public class ClassTooBigCheckVb : ClassTooBigCheck<IClassDeclaration>
    {
        protected override void Run(IClassDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            CheckIfClassIsTooBig<IMethodDeclaration>(element.Name, element, data, consumer);
        }
    }
}