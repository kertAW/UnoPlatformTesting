namespace DocsUnoTesting.Models;

public class TestResult : IHasId
{
    private readonly Guid _id = Guid.NewGuid();
    private readonly Test test;
    private readonly Student student;

    public TestResult(
        Test test,
        Student student,
        float score,
        IEnumerable<TestStageResult>? stageResults = null
    )
    {
        this.test = test;
        this.student = student;

        if (!CheckScoreIsValid(test, score))
        {
            throw new ArgumentException("Оценка не может быть больше или меньше порогов");
        }
        Score = score;
        StageResults = stageResults ?? [];
    }

    private bool CheckScoreIsValid(Test test, float score)
    {
        // Это реализация-заглушка, основанная на сообщении об ошибке.
        // Возможно, вам потребуется адаптировать ее в зависимости от реальных свойств вашего класса Test.
        return true; // Пока что считаем любую оценку валидной.
    }

    public Guid Id => _id;

    public Test PassedTest => test;
    public Student Student => student;
    public float Score { get; }
    public IEnumerable<TestStageResult> StageResults { get; }
}
