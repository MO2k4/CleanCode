using System.Globalization;
using CleanCode.Resources;
using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.VB;

namespace CleanCode.Features.ClassTooBig
{
    [RegisterConfigurableSeverity(SeverityID,
        null,
        CleanCodeHighlightingGroupIds.CleanCode,
        "Class too big",
        "This class contains too many methods",
        Severity.SUGGESTION)]
    [ConfigurableSeverityHighlighting(SeverityID, CSharpLanguage.Name + "," + VBLanguage.Name)]
    public class ClassTooBigHighlighting : IHighlighting
    {
        internal const string SeverityID = "ClassTooBig";

        private readonly DocumentRange _documentRange;

        public ClassTooBigHighlighting(DocumentRange documentRange, int threshold, int currentValue)
        {
            ToolTip = string.Format(CultureInfo.CurrentCulture, Warnings.ClassTooBig, currentValue, threshold);
            _documentRange = documentRange;
        }

        public DocumentRange CalculateRange() => _documentRange;

        public string ToolTip { get; }

        public string ErrorStripeToolTip => ToolTip;

        public bool IsValid() => true;
    }
}