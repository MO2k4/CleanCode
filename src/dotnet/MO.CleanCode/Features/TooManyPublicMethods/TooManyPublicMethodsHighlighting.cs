using System.Globalization;
using CleanCode.Resources;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.VB;

namespace CleanCode.Features.TooManyPublicMethods
{
    [RegisterConfigurableSeverity(
        SeverityID,
        null,
        CleanCodeHighlightingGroupIds.CleanCode,
        "Too many public methods",
        "This class exposes too many public methods",
        Severity.SUGGESTION
    )]
    [ConfigurableSeverityHighlighting(SeverityID, CSharpLanguage.Name + "," + VBLanguage.Name)]
    public class TooManyPublicMethodsHighlighting : IHighlighting
    {
        internal const string SeverityID = "TooManyPublicMethods";

        private readonly DocumentRange _documentRange;

        public TooManyPublicMethodsHighlighting(
            DocumentRange documentRange,
            int threshold,
            int currentValue
        )
        {
            ToolTip = string.Format(
                CultureInfo.CurrentCulture,
                Warnings.TooManyPublicMethods,
                currentValue,
                threshold
            );
            _documentRange = documentRange;
        }

        public DocumentRange CalculateRange() => _documentRange;

        public string ToolTip { get; }

        public string ErrorStripeToolTip => ToolTip;

        public bool IsValid() => !string.IsNullOrWhiteSpace(ToolTip);
    }
}
