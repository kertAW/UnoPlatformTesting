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

    public TestResultRepository(StudentRepository studentRepository, TestRepository testRepository)
    {
        _studentRepository = studentRepository;
        _testRepository = testRepository;
        Seed();
    }

    public IEnumerable<TestResult> GetAll() => _testResults;

    public TestResult? GetById(Guid id) => _testResults.FirstOrDefault(tr => tr.Id == id);

    public void Add(TestResult testResult) => _testResults.Add(testResult);

    public void Update(TestResult testResult)
    {
        var existingTestResult = GetById(testResult.Id);
        if (existingTestResult != null)
        {
            _testResults.Remove(existingTestResult);
            _testResults.Add(testResult);
        }
    }

    public void Delete(Guid id)
    {
        var testResult = GetById(id);
        if (testResult != null)
        {
            _testResults.Remove(testResult);
        }
    }

    private void Seed()
    {
        var students = _studentRepository.GetAll().ToList();
        var tests = _testRepository.GetAll().ToList();
        var random = new Random();

        foreach (var student in students)
        {
            foreach (var test in tests)
            {
                if (random.Next(0, 2) == 0)
                {
                    _testResults.Add(new TestResult(test, student, random.Next((int)test.MinScore, (int)test.MaxScore)));
                }
            }
        }
    }
}
