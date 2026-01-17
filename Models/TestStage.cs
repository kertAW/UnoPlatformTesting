namespace DocsUnoTesting.Models;

internal class TestStage(
    Test test,
    float minScore,
    float maxScore,
    TestStage? parentStage = null,
    IEnumerable<TestStage>? childStages = null
) : IHasId
{
    private readonly Guid _id = Guid.NewGuid();

    public Guid Id => _id;

    public Test Test => test;

    public TestStage? ParentStage => parentStage;

    public float MinScore => minScore;
    public float MaxScore => maxScore;

    public IEnumerable<TestStage> ChildStages => childStages ?? [];
}
