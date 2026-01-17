namespace DocsUnoTesting.Models;

internal class TestStageResult(TestResult testResult, TestStage testStage, float score) : IHasId
{
    private readonly Guid _id = Guid.NewGuid();

    public Guid Id => _id;

    public TestResult TestResult => testResult;
    public TestStage Stage => testStage;
    public float Score => score;
}
