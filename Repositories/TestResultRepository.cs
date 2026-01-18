using System;
using System.Collections.Generic;
using System.Linq;
using DocsUnoTesting.Models;

namespace DocsUnoTesting.Repositories;

public class TestResultRepository
{
    private readonly List<TestResult> _testResults = new();
    private readonly StudentRepository _studentRepository;
    private readonly TestRepository _testRepository;
    private readonly TestStageResultRepository _testStageResultRepository;
    private readonly Random _random = new();

    public TestResultRepository(
        StudentRepository studentRepository,
        TestRepository testRepository,
        TestStageResultRepository testStageResultRepository)
    {
        _studentRepository = studentRepository;
        _testRepository = testRepository;
        _testStageResultRepository = testStageResultRepository;
        Seed();
    }

    public IEnumerable<TestResult> GetAll() => _testResults;

    public TestResult? GetById(Guid id) => _testResults.FirstOrDefault(r => r.Id == id);

    public void Add(TestResult testResult) => _testResults.Add(testResult);

    public void Update(TestResult testResult)
    {
        var existingResult = GetById(testResult.Id);
        if (existingResult != null)
        {
            _testResults.Remove(existingResult);
            _testResults.Add(testResult);
        }
    }

    public void Delete(Guid id)
    {
        var result = GetById(id);
        if (result != null)
        {
            _testResults.Remove(result);
        }
    }

    private void Seed()
    {
        var students = _studentRepository.GetAll().ToList();
        var tests = _testRepository.GetAll().ToList();

        if (students.Any() && tests.Any())
        {
            foreach (var student in students)
            {
                foreach (var test in tests)
                {
                    float score = (float)(_random.NextDouble() * (test.MaxScore - test.MinScore) + test.MinScore);
                    var stageResults = _testStageResultRepository.GenerateStageResultsForTestResult(
                        new TestResult(test, student, score), // Pass a dummy TestResult for initial generation
                        test.Stages
                    );
                    _testResults.Add(new TestResult(test, student, score, stageResults));
                }
            }
        }
    }
}