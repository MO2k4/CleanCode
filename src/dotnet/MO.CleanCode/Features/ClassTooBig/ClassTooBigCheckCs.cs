using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace CleanCode.Features.ClassTooBig
{
    [ElementProblemAnalyzer(typeof(IClassDeclaration), HighlightingTypes = new[]
    {
        typeof(ClassTooBigHighlighting)
    })]
    public class ClassTooBigCheckCs : ClassTooBigCheck<IClassDeclaration>
    {
        protected override void Run(IClassDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            CheckIfClassIsTooBig<IMethodDeclaration>(element.NameIdentifier, element, data, consumer);
        }
    }
}