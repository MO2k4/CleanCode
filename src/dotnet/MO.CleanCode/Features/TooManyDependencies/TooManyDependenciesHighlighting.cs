using System.Globalization;
using CleanCode.Resources;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.VB;

namespace CleanCode.Features.TooManyDependencies
{
    [RegisterConfigurableSeverity(SeverityID,
        null,
        CleanCodeHighlightingGroupIds.CleanCode,
        "Too many dependencies",
        "Too many dependencies passed into constructor.",
        Severity.WARNING)]
    [ConfigurableSeverityHighlighting(SeverityID, CSharpLanguage.Name + "," + VBLanguage.Name)]
    public class TooManyDependenciesHighlighting : IHighlighting
    {
        internal const string SeverityID = "TooManyDependencies";
        private readonly DocumentRange _documentRange;

        public TooManyDependenciesHighlighting(DocumentRange documentRange, int threshold, int currentValue)
        {
            ToolTip = string.Format(CultureInfo.CurrentCulture, Warnings.TooManyDependencies, currentValue, threshold);
            _documentRange = documentRange;
        }

        public DocumentRange CalculateRange() => _documentRange;

        public string ToolTip { get; }

        public string ErrorStripeToolTip => ToolTip;

        public bool IsValid() => true;
    }
}