namespace DocsUnoTesting.Models;

public class Test(string name, float minScore, float maxScore) : IHasId
{
    private readonly Guid _id = Guid.NewGuid();

    public Guid Id => _id;

    public string Name => name;

    public float MinScore => minScore;
    public float MaxScore => maxScore;
}
