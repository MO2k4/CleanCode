using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.VB.Tree;

namespace CleanCode.Features.TooManyPublicMethods
{
    [ElementProblemAnalyzer(
        typeof(IClassDeclaration),
        HighlightingTypes = new[] { typeof(TooManyPublicMethodsHighlighting) }
    )]
    public class TooManyPublicMethodsCheckVb : TooManyPublicMethodsCheck<IClassDeclaration>
    {
        protected override void Run(
            IClassDeclaration element,
            ElementProblemAnalyzerData data,
            IHighlightingConsumer consumer
        )
        {
            CheckIfClassHasTooManyPublicMethods<IMethodDeclaration>(
                element.Name,
                element,
                data,
                consumer,
                method => method.GetAccessRights() == AccessRights.PUBLIC
            );
        }
    }
}
