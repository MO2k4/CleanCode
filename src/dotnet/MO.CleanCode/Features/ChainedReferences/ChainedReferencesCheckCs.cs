using CleanCode.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp.Tree;

namespace CleanCode.Features.ChainedReferences
{
    [ElementProblemAnalyzer(typeof(ICSharpStatement), HighlightingTypes = new[]
    {
        typeof(MaximumChainedReferencesHighlighting)
    })]
    public class ChainedReferencesCheckCs : ChainedReferencesCheck<ICSharpStatement>
    {
        protected override void Run(ICSharpStatement element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            if (element.CanBeEmbedded) return;
            
            var threshold = data.SettingsStore.GetValue((CleanCodeSettings s) => s.MaximumChainedReferences);
            HighlightMethodChainsThatAreTooLong(element, consumer, threshold);
        }
    }
}