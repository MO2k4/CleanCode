﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CleanCode.Resources;
using CleanCode.Settings;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.Tree;

namespace CleanCode.Features.HollowNames;

public abstract class HollowNamesCheck<T> : ElementProblemAnalyzer<T>
{
    private static readonly string[] Separator = { "," };

    protected static void CheckAndAddHighlighting(IDeclaration element, ElementProblemAnalyzerData data,  IHighlightingConsumer consumer)
    {
        var suffixes = GetSuffixes(data.SettingsStore);

        var match = GetFirstMatchOrDefault(element.DeclaredName, suffixes);
        if (match != null)
            AddHighlighting(match, consumer, element);
    }
        
    private static string[] GetSuffixes(IContextBoundSettingsStore dataSettingsStore)
    {
        var suffixes = dataSettingsStore.GetValue((CleanCodeSettings s) => s.MeaninglessClassNameSuffixes);
        return suffixes.Split(Separator, StringSplitOptions.RemoveEmptyEntries);
    }

    private static string GetFirstMatchOrDefault(string declaredName, IEnumerable<string> suffixes)
    {
        return suffixes.FirstOrDefault(declaredName.EndsWith);
    }

    private static void AddHighlighting(string bannedSuffix, IHighlightingConsumer consumer, ITreeNode treeNode)
    {
        var documentRange = treeNode.GetDocumentRange();
        var toolTip = string.Format(CultureInfo.CurrentCulture, Warnings.HollowTypeName, bannedSuffix);
        var highlighting = new HollowTypeNameHighlighting(toolTip, documentRange);
        consumer.AddHighlighting(highlighting);
    }
}