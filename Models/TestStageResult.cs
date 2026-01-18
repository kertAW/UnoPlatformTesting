using System.Collections.ObjectModel;

namespace DocsUnoTesting.Models;

public class TestStageResult : IHasId
{
    private readonly Guid _id = Guid.NewGuid();

    public Guid Id => _id;

    public TestResult TestResult { get; }
    public TestStage Stage { get; }
    public float Score { get; }

    public bool IsPassed { get; set; }
    public string Comment { get; set; } = string.Empty;

    public ObservableCollection<TestStageResult> Children { get; } = new ObservableCollection<TestStageResult>();

    public TestStageResult(TestResult testResult, TestStage testStage, float score)
    {
        TestResult = testResult;
        Stage = testStage;
        Score = score;
    }
}
