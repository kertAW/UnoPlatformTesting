using System.Collections.Generic;
using System.Linq;
using DocsUnoTesting.Models;

namespace DocsUnoTesting.Repositories;

public class TestRepository
{
    private readonly List<Test> _tests = new();
    private readonly TestStageRepository _testStageRepository;

    public TestRepository(TestStageRepository testStageRepository)
    {
        _testStageRepository = testStageRepository;
        Seed();
    }

    public IEnumerable<Test> GetAll() => _tests;

    public Test? GetById(Guid id) => _tests.FirstOrDefault(t => t.Id == id);

    public void Add(Test test) => _tests.Add(test);

    public void Update(Test test)
    {
        var existingTest = GetById(test.Id);
        if (existingTest != null)
        {
            _tests.Remove(existingTest);
            _tests.Add(test);
        }
    }

    public void Delete(Guid id)
    {
        var test = GetById(id);
        if (test != null)
        {
            _tests.Remove(test);
        }
    }

    private void Seed()
    {
        var mathTest = new Test("Math Test", 0, 100);
        var mathStages = _testStageRepository.GenerateStagesForTest(mathTest);
        mathTest = new Test(mathTest.Name, mathTest.MinScore, mathTest.MaxScore, mathStages);

        var scienceTest = new Test("Science Test", 0, 100);
        var scienceStages = _testStageRepository.GenerateStagesForTest(scienceTest);
        scienceTest = new Test(scienceTest.Name, scienceTest.MinScore, scienceTest.MaxScore, scienceStages);

        var historyTest = new Test("History Test", 0, 100);
        var historyStages = _testStageRepository.GenerateStagesForTest(historyTest);
        historyTest = new Test(historyTest.Name, historyTest.MinScore, historyTest.MaxScore, historyStages);

        var geographyTest = new Test("Geography Test", 0, 100);
        var geographyStages = _testStageRepository.GenerateStagesForTest(geographyTest);
        geographyTest = new Test(geographyTest.Name, geographyTest.MinScore, geographyTest.MaxScore, geographyStages);

        var englishTest = new Test("English Test", 0, 100);
        var englishStages = _testStageRepository.GenerateStagesForTest(englishTest);
        englishTest = new Test(englishTest.Name, englishTest.MinScore, englishTest.MaxScore, englishStages);


        _tests.AddRange(new List<Test>
        {
            mathTest,
            scienceTest,
            historyTest,
            geographyTest,
            englishTest
        });
    }
}
