using System;
using CleanCode.Settings;
using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.Tree;

namespace CleanCode.Features.MethodTooLong;

public abstract class MethodTooLongCheck<T> : ElementProblemAnalyzer<T>
{
    protected static void CheckAndAddHighlight<TMethodDeclaration>(TMethodDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
        where TMethodDeclaration : IDeclaration
    {
        var highlighting = GetHighlighting(element, data);
        if (highlighting != null)
            consumer.AddHighlighting(highlighting);
    }
    
    private static IHighlighting GetHighlighting<TMethodDeclaration>(TMethodDeclaration element, ElementProblemAnalyzerData data)
        where TMethodDeclaration : IDeclaration
    {
        var maxStatements = data.SettingsStore.GetValue((CleanCodeSettings s) => s.MaximumMethodStatements);
        var maxDeclarations = data.SettingsStore.GetValue((CleanCodeSettings s) => s.MaximumDeclarationsInMethod);

        var highlight = CheckStatementCount(element, maxStatements);
        if (highlight != null) return highlight;

        return element switch
        {
            JetBrains.ReSharper.Psi.CSharp.Tree.IMethodDeclaration declaration => CheckDeclarationCount(declaration,
                maxDeclarations),
            JetBrains.ReSharper.Psi.VB.Tree.IMethodDeclaration declaration => CheckDeclarationCount(declaration,
                maxDeclarations),
            _ => throw new ArgumentOutOfRangeException(nameof(element), element, null)
        };
    }

    [CanBeNull]
    private static MethodTooLongHighlighting CheckStatementCount(IDeclaration element, int maxStatements)
    {
        var statementCount = element.CountChildren<IStatement>();
        return statementCount > maxStatements 
            ? new MethodTooLongHighlighting(element.GetNameDocumentRange(), maxStatements, statementCount) 
            : null;
    }

    [CanBeNull]
    private static MethodTooManyDeclarationsHighlighting CheckDeclarationCount(JetBrains.ReSharper.Psi.CSharp.Tree.IMethodDeclaration element, int maxDeclarations)
    {
        // Only look in the method body for declarations, otherwise we see
        // parameters + type parameters. We can ignore arrow expressions, as
        // they must be a single expression and won't have declarations
        var declarationCount = element.Body?.CountChildren<IDeclaration>() ?? 0;
        return declarationCount > maxDeclarations 
            ? new MethodTooManyDeclarationsHighlighting(element.GetNameDocumentRange(), maxDeclarations, declarationCount) 
            : null;
    }
    
    [CanBeNull]
    private static MethodTooManyDeclarationsHighlighting CheckDeclarationCount(JetBrains.ReSharper.Psi.VB.Tree.IMethodDeclaration element, int maxDeclarations)
    {
        // Only look in the method body for declarations, otherwise we see
        // parameters + type parameters. We can ignore arrow expressions, as
        // they must be a single expression and won't have declarations
        var declarationCount = element.Block?.CountChildren<IDeclaration>() ?? 0;
        return declarationCount > maxDeclarations 
            ? new MethodTooManyDeclarationsHighlighting(element.GetNameDocumentRange(), maxDeclarations, declarationCount) 
            : null;
    }
}