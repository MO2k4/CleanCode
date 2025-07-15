using System;
using System.Linq;
using CleanCode.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.Tree;

namespace CleanCode.Features.TooManyPublicMethods
{
    public abstract class TooManyPublicMethodsCheck<T> : ElementProblemAnalyzer<T>
    {
        protected static void CheckIfClassHasTooManyPublicMethods<TMethodDeclaration>(
            ITreeNode declaration,
            ITreeNode element,
            ElementProblemAnalyzerData data,
            IHighlightingConsumer consumer,
            Func<TMethodDeclaration, bool> isPublicMethod
        )
            where TMethodDeclaration : ITreeNode
        {
            var maxPublicMethods = data.SettingsStore.GetValue(
                (CleanCodeSettings s) => s.MaximumPublicMethodsInClass
            );

            var publicMethodCount = element
                .Children()
                .OfType<TMethodDeclaration>()
                .Count(isPublicMethod);

            if (publicMethodCount <= maxPublicMethods)
                return;

            var documentRange = declaration.GetDocumentRange();
            var highlighting = new TooManyPublicMethodsHighlighting(
                documentRange,
                maxPublicMethods,
                publicMethodCount
            );
            consumer.AddHighlighting(highlighting);
        }
    }
}
