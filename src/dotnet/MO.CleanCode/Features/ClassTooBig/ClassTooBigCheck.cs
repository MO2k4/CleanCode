using CleanCode.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.Tree;

namespace CleanCode.Features.ClassTooBig;

public abstract class ClassTooBigCheck<T> : ElementProblemAnalyzer<T>
{
    protected static void CheckIfClassIsTooBig<TMethodDeclaration>(ITreeNode declaration, 
        ITreeNode element,
        ElementProblemAnalyzerData data, 
        IHighlightingConsumer consumer) 
        where TMethodDeclaration : ITreeNode
    {
        var maxLength = data.SettingsStore.GetValue((CleanCodeSettings s) => s.MaximumMethodsInClass);
        var statementCount = element.CountChildren<TMethodDeclaration>();

        if (statementCount <= maxLength) return;
            
        var documentRange = declaration.GetDocumentRange();
        var highlighting = new ClassTooBigHighlighting(documentRange, maxLength, statementCount);
        consumer.AddHighlighting(highlighting);
    }
}