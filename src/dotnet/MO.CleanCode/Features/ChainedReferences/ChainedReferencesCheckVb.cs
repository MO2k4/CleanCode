using CleanCode.Features;
using CleanCode.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.VB.Tree;

namespace CleanCode.Features.ChainedReferences
{
    [ElementProblemAnalyzer(
        typeof(IVBStatement),
        HighlightingTypes = new[] { typeof(MaximumChainedReferencesHighlighting) }
    )]
    public class ChainedReferencesCheckVb : ChainedReferencesCheck<IVBStatement>
    {
        protected override void Run(
            IVBStatement element,
            ElementProblemAnalyzerData data,
            IHighlightingConsumer consumer
        )
        {
            var threshold = data.SettingsStore.GetValue(
                (CleanCodeSettings s) => s.MaximumChainedReferences
            );

            var includeLinq = data.SettingsStore.GetValue(
                (CleanCodeSettings s) => s.IncludeLinqInChainedReferences
            );

            HighlightMethodChainsThatAreTooLong(element, consumer, threshold, includeLinq);
        }
    }
}
