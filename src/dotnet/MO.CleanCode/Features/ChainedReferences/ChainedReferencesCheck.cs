using System.Collections.Generic;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.VB.Tree;

namespace CleanCode.Features.ChainedReferences;

public abstract class ChainedReferencesCheck<T> : ElementProblemAnalyzer<T>
{
    protected static void HighlightMethodChainsThatAreTooLong(ITreeNode statement, IHighlightingConsumer consumer, int threshold)
    {
        var children = statement.Children();

        foreach (var treeNode in children)
        {
            if (treeNode is IReferenceExpression referenceExpression)
            {
                HighlightReferenceExpressionIfNeeded(referenceExpression, consumer, threshold);
            }
            else
            {
                HighlightMethodChainsThatAreTooLong(treeNode, consumer, threshold);
            }
        }
    }

    private static void HighlightReferenceExpressionIfNeeded(IReferenceExpression referenceExpression, IHighlightingConsumer consumer, int threshold)
    {
        var types = new HashSet<IType>();

        var nextReferenceExpression = referenceExpression;
        var chainLength = 0;

        while (nextReferenceExpression != null)
        {
            var childReturnType = ExtensionMethodsCsharp.TryGetClosedReturnTypeFrom(nextReferenceExpression);

            if (childReturnType != null)
            {
                types.Add(childReturnType);
                chainLength++;
            }

            nextReferenceExpression = ExtensionMethodsVb.TryGetFirstReferenceExpression(nextReferenceExpression);
        }

        var isFluentChain = types.Count == 1;
        if (!isFluentChain && chainLength > threshold)
        {
            AddHighlighting(referenceExpression, consumer, threshold, chainLength);
        }
    }

    private static void AddHighlighting(IReferenceExpression reference, IHighlightingConsumer consumer, int threshold, int currentValue)
    {
        var nameIdentifier = reference.NameIdentifier;
        var documentRange = nameIdentifier.GetDocumentRange();
        var highlighting = new MaximumChainedReferencesHighlighting(documentRange, threshold, currentValue);
        consumer.AddHighlighting(highlighting);
    }
}