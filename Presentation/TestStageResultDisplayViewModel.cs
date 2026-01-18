using CommunityToolkit.Mvvm.ComponentModel;
using DocsUnoTesting.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace DocsUnoTesting.Presentation;

public partial class TestStageResultDisplayViewModel : ObservableObject
{
    public TestStageResult TestStageResult { get; }
    public ObservableCollection<TestStageResultDisplayViewModel> Children { get; } = new();

    public string DisplayText => $"Stage ({TestStageResult.Stage.MinScore:F0}-{TestStageResult.Stage.MaxScore:F0}): {TestStageResult.Score:F2}";

    public TestStageResultDisplayViewModel(TestStageResult testStageResult, IEnumerable<TestStageResult> allTestStageResults)
    {
        TestStageResult = testStageResult;

        if (TestStageResult.Stage.ChildStages != null && TestStageResult.Stage.ChildStages.Any())
        {
            foreach (var childStage in TestStageResult.Stage.ChildStages)
            {
                var childTestStageResult = allTestStageResults.FirstOrDefault(sr => sr.Stage.Id == childStage.Id);
                if (childTestStageResult != null)
                {
                    Children.Add(new TestStageResultDisplayViewModel(childTestStageResult, allTestStageResults));
                }
            }
        }
    }
}
