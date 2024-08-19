using System.Linq;
using CleanCode.Settings;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.Util;

namespace CleanCode.Features.TooManyDependencies
{
    [ElementProblemAnalyzer(typeof(IConstructorDeclaration), HighlightingTypes = new[]
    {
        typeof(TooManyDependenciesHighlighting)
    })]
    public class TooManyDependenciesCheckCs : ElementProblemAnalyzer<IConstructorDeclaration>
    {
        protected override void Run(IConstructorDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            var maxDependencies = data.SettingsStore.GetValue((CleanCodeSettings s) => s.MaximumConstructorDependencies);
            var dependencies = element.ParameterDeclarations.Select(declaration => (declaration.DeclaredElement?.Type).IsInterfaceType());

            var dependenciesCount = dependencies.Count();
            if (dependenciesCount > maxDependencies)
            {
                var highlighting = new TooManyDependenciesHighlighting(element.GetNameDocumentRange(), maxDependencies, dependenciesCount);
                consumer.AddHighlighting(highlighting);
            }
        }
    }
}