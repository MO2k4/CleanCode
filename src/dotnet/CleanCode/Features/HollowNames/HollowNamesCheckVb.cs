using System;
using System.Collections.Generic;
using System.Linq;
using CleanCode.Resources;
using CleanCode.Settings;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.VB.Tree;
using JetBrains.ReSharper.Psi.Tree;
using System.Globalization;

namespace CleanCode.Features.HollowNames
{
    [ElementProblemAnalyzer(typeof(IClassDeclaration),
        HighlightingTypes = new[]
        {
            typeof(HollowTypeNameHighlighting)
        })]
    public class HollowNamesCheckVb : ElementProblemAnalyzer<IClassDeclaration>
    {
        protected override void Run(IClassDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        {
            var suffixes = GetSuffixes(data.SettingsStore);

            var match = GetFirstMatchOrDefault(element.DeclaredName, suffixes);
            if (match != null)
                AddHighlighting(match, consumer, element);
        }

        private IEnumerable<string> GetSuffixes(IContextBoundSettingsStore dataSettingsStore)
        {
            var suffixes = dataSettingsStore.GetValue((CleanCodeSettings s) => s.MeaninglessClassNameSuffixes);
            return suffixes.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries);
        }

        private static string GetFirstMatchOrDefault(string declaredName, IEnumerable<string> suffixes)
        {
            return suffixes.FirstOrDefault(declaredName.EndsWith);
        }

        private static void AddHighlighting(string bannedSuffix, IHighlightingConsumer consumer, IClassDeclaration typeExpression)
        {
            var identifier = typeExpression.Name;
            var documentRange = identifier.GetDocumentRange();
            var toolTip = string.Format(CultureInfo.CurrentCulture, Warnings.HollowTypeName, bannedSuffix);
            var highlighting = new HollowTypeNameHighlighting(toolTip, documentRange);
            consumer.AddHighlighting(highlighting);
        }
    }
}