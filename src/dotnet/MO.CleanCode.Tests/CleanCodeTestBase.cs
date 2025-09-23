using System.Collections.Generic;
using JetBrains.Application.Settings;
using JetBrains.ReSharper.Feature.Services.Daemon;
using JetBrains.ReSharper.TestFramework;
using JetBrains.ReSharper.Psi;
using NUnit.Framework;
using CleanCode.Settings;

namespace CleanCode.Tests
{
    [TestFixture]
    public abstract class CleanCodeTestBase : BaseTestFixture
    {
        protected override string RelativeTestDataPath => "CSharp";

        protected void DoNamedTest(ICleanCodeSettings settings = null)
        {
            if (settings != null)
            {
                using (TestSettingsStoreFree.CreateSettingsTransaction())
                {
                    var settingsStore = Shell.Instance.GetComponent<ISettingsStore>();
                    settingsStore.BindToContextTransient(ContextRange.ApplicationWide)
                        .SetValue((CleanCodeSettings s) => s, settings as CleanCodeSettings);

                    ExecuteWithGold(TestMethodName);
                }
            }
            else
            {
                ExecuteWithGold(TestMethodName);
            }
        }

        protected IEnumerable<IHighlighting> RunInspection(string testName, ICleanCodeSettings settings = null)
        {
            if (settings != null)
            {
                using (TestSettingsStoreFree.CreateSettingsTransaction())
                {
                    var settingsStore = Shell.Instance.GetComponent<ISettingsStore>();
                    settingsStore.BindToContextTransient(ContextRange.ApplicationWide)
                        .SetValue((CleanCodeSettings s) => s, settings as CleanCodeSettings);

                    return DoTestSolution(testName);
                }
            }

            return DoTestSolution(testName);
        }

        protected virtual IEnumerable<IHighlighting> DoTestSolution(string testName)
        {
            var testFile = GetTestDataFilePath2(testName + ".cs");
            var solution = GetSolution(testFile);

            foreach (var file in solution.GetAllProjectFiles())
            {
                var sourceFile = file.ToSourceFile();
                if (sourceFile?.IsValid() == true)
                {
                    var daemon = Solution.GetComponent<IDaemon>();
                    var highlighting = daemon.GetHighlighting(sourceFile);
                    foreach (var h in highlighting)
                    {
                        yield return h;
                    }
                }
            }
        }
    }
}