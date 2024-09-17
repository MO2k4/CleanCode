using CleanCode.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.Tree;

namespace CleanCode.Features.MethodNameNotMeaningful;

public abstract class MethodNameNotMeaningfulCheck<T> : ElementProblemAnalyzer<T>
{
    protected static void CheckAndAddHighlighting(IDeclaration element, ElementProblemAnalyzerData data,
        IHighlightingConsumer consumer)
    {
        if (element == null)
            return;

        var minimumMethodNameLength = data.SettingsStore.GetValue((CleanCodeSettings s) => s.MinimumMeaningfulMethodNameLength);
        var name = element.GetText();

        if (name.Length >= minimumMethodNameLength) return;
        
        var documentRange = element.GetNameDocumentRange();
        var highlighting = new MethodNameNotMeaningfulHighlighting(documentRange);
        consumer.AddHighlighting(highlighting);
    }
}