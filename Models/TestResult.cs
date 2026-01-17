namespace DocsUnoTesting.Models;

internal class TestResult : IHasId
{
    private readonly Guid _id = Guid.NewGuid();
    private readonly Test test;
    private readonly Student student;

    public TestResult(Test test, Student student, float score)
    {
        this.test = test;
        this.student = student;

        if (!CheckScoreIsValid(test, score))
        {
            throw new ArgumentException("Оценка не может быть больше или меньше порогов");
        }
    }

    public Guid Id => _id;

    public Test PassedTest => test;
    public Student Student => student;

    private bool CheckScoreIsValid(Test test, float score)
    {
        return score >= test.MinScore && score <= test.MaxScore;
    }
}
