using System.Collections.Generic;
using CleanCode.Features;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.ReSharper.Psi.VB.Tree;

namespace CleanCode.Features.ChainedReferences;

public abstract class ChainedReferencesCheck<T> : ElementProblemAnalyzer<T>
{
    protected static void HighlightMethodChainsThatAreTooLong(
        ITreeNode statement,
        IHighlightingConsumer consumer,
        int threshold,
        bool includeLinq = false
    )
    {
        var children = statement.Children();

        foreach (var treeNode in children)
        {
            if (treeNode is IReferenceExpression referenceExpression)
            {
                HighlightReferenceExpressionIfNeeded(
                    referenceExpression,
                    consumer,
                    threshold,
                    includeLinq
                );
            }
            else
            {
                HighlightMethodChainsThatAreTooLong(treeNode, consumer, threshold, includeLinq);
            }
        }
    }

    private static void HighlightReferenceExpressionIfNeeded(
        IReferenceExpression referenceExpression,
        IHighlightingConsumer consumer,
        int threshold,
        bool includeLinq
    )
    {
        var types = new HashSet<IType>();

        var nextReferenceExpression = referenceExpression;
        var chainLength = 0;

        while (nextReferenceExpression != null)
        {
            // Skip LINQ methods if not including them in the count
            if (!includeLinq && IsLinqMethod(nextReferenceExpression))
            {
                nextReferenceExpression = ExtensionMethodsVb.TryGetFirstReferenceExpression(
                    nextReferenceExpression
                );
                continue;
            }

            var childReturnType = ExtensionMethodsCsharp.TryGetClosedReturnTypeFrom(
                nextReferenceExpression
            );

            if (childReturnType != null)
            {
                types.Add(childReturnType);
                chainLength++;
            }

            nextReferenceExpression = ExtensionMethodsVb.TryGetFirstReferenceExpression(
                nextReferenceExpression
            );
            nextReferenceExpression = ExtensionMethodsVb.TryGetFirstReferenceExpression(
                nextReferenceExpression
            );
        }

        var isFluentChain = types.Count == 1;
        if (!isFluentChain && chainLength > threshold)
        {
            AddHighlighting(referenceExpression, consumer, threshold, chainLength);
        }
    }

    private static bool IsLinqMethod(IReferenceExpression expression)
    {
        if (expression == null)
            return false;

        var reference = expression.Reference;
        var resolveResult = reference.Resolve();
        var declaredElement = resolveResult.DeclaredElement;

        // Check if it's a method in the System.Linq namespace
        if (declaredElement is IMethod method)
        {
            var containingType = method.GetContainingType();
            if (containingType == null)
                return false;

            var ns = containingType.GetContainingNamespace();
            return ns != null && ns.QualifiedName.Contains("System.Linq");
        }

        return false;
    }

    private static void AddHighlighting(
        IReferenceExpression reference,
        IHighlightingConsumer consumer,
        int threshold,
        int currentValue
    )
    {
        var nameIdentifier = reference.NameIdentifier;
        var documentRange = nameIdentifier.GetDocumentRange();
        var highlighting = new MaximumChainedReferencesHighlighting(
            documentRange,
            threshold,
            currentValue
        );
        consumer.AddHighlighting(highlighting);
    }
}
