using System.Collections.Generic;
using System.Linq;
using DocsUnoTesting.Models;

namespace DocsUnoTesting.Repositories;

public class TestRepository
{
    private readonly List<Test> _tests = new();

    public TestRepository()
    {
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
        _tests.AddRange(new List<Test>
        {
            new("Math Test", 0, 100),
            new("Science Test", 0, 100),
            new("History Test", 0, 100),
            new("Geography Test", 0, 100),
            new("English Test", 0, 100)
        });
    }
}
