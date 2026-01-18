using System;
using System.Collections.Generic;
using System.Linq;
using DocsUnoTesting.Models;

namespace DocsUnoTesting.Repositories;

public class TestStageResultRepository
{
    private readonly List<TestStageResult> _testStageResults = new();
    private readonly Random _random = new();

    public TestStageResultRepository()
    {
    }

    public IEnumerable<TestStageResult> GetAll() => _testStageResults;

    public TestStageResult? GetById(Guid id) => _testStageResults.FirstOrDefault(r => r.Id == id);

    public void Add(TestStageResult testStageResult) => _testStageResults.Add(testStageResult);

    public void Update(TestStageResult testStageResult)
    {
        var existingResult = GetById(testStageResult.Id);
        if (existingResult != null)
        {
            _testStageResults.Remove(existingResult);
            _testStageResults.Add(testStageResult);
        }
    }

    public void Delete(Guid id)
    {
        var result = GetById(id);
        if (result != null)
        {
            _testStageResults.Remove(result);
        }
    }

    // This method will generate results for stages of a specific TestResult.
    public List<TestStageResult> GenerateStageResultsForTestResult(TestResult testResult, IEnumerable<TestStage> stages)
    {
        var stageResults = new List<TestStageResult>();

        foreach (var stage in stages)
        {
            float score = (float)(_random.NextDouble() * (stage.MaxScore - stage.MinScore) + stage.MinScore);
            stageResults.Add(new TestStageResult(testResult, stage, score));

            // Recursively generate results for child stages
            if (stage.ChildStages != null && stage.ChildStages.Any())
            {
                stageResults.AddRange(GenerateStageResultsForTestResult(testResult, stage.ChildStages));
            }
        }

        return stageResults;
    }
}
