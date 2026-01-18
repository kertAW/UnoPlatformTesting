namespace DocsUnoTesting.Models;

public class TestStage(
    string name,
    Test test,
    float minScore,
    float maxScore,
    TestStage? parentStage = null,
    IEnumerable<TestStage>? childStages = null
) : IHasId
{
    private readonly Guid _id = Guid.NewGuid();

    public Guid Id => _id;
    public string Name { get; } = name;

    public Test Test => test;

    public TestStage? ParentStage => parentStage;

    public float MinScore => minScore;
    public float MaxScore => maxScore;

    public IEnumerable<TestStage> ChildStages => childStages ?? [];
}
