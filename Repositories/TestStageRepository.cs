using System;
using System.Collections.Generic;
using System.Linq;
using DocsUnoTesting.Models;

namespace DocsUnoTesting.Repositories;

public class TestStageRepository
{
    private readonly List<TestStage> _testStages = new();

    public TestStageRepository()
    {
        // Seed() will be called when TestRepository needs to get stages.
        // It's not called directly in the constructor of this repository
        // because stages are tied to specific Test objects.
    }

    public IEnumerable<TestStage> GetAll() => _testStages;

    public TestStage? GetById(Guid id) => _testStages.FirstOrDefault(s => s.Id == id);

    public void Add(TestStage testStage) => _testStages.Add(testStage);

    public void Update(TestStage testStage)
    {
        var existingStage = GetById(testStage.Id);
        if (existingStage != null)
        {
            _testStages.Remove(existingStage);
            _testStages.Add(testStage);
        }
    }

    public void Delete(Guid id)
    {
        var stage = GetById(id);
        if (stage != null)
        {
            _testStages.Remove(stage);
        }
    }

    // This method will generate stages for a specific Test.
    // It will be called by TestRepository during its Seed() method.
    public List<TestStage> GenerateStagesForTest(Test test)
    {
        var stages = new List<TestStage>();

        var stage1 = new TestStage("Stage 1", test, 0, 25);
        var stage2 = new TestStage("Stage 2", test, 25, 50);
        var stage3 = new TestStage("Stage 3", test, 50, 75);
        var stage4 = new TestStage("Stage 4", test, 75, 100);

        // Nested stages for demonstration
        var subStage1_1 = new TestStage("SubStage 1.1", test, 0, 10, stage1);
        var subStage1_2 = new TestStage("SubStage 1.2", test, 10, 25, stage1);
        stage1 = new TestStage("Stage 1", test, 0, 25, null, new List<TestStage> { subStage1_1, subStage1_2 });

        var subStage2_1 = new TestStage("SubStage 2.1", test, 25, 35, stage2);
        var subStage2_2 = new TestStage("SubStage 2.2", test, 35, 50, stage2);
        stage2 = new TestStage("Stage 2", test, 25, 50, null, new List<TestStage> { subStage2_1, subStage2_2 });

        stages.Add(stage1);
        stages.Add(stage2);
        stages.Add(stage3);
        stages.Add(stage4);

        return stages;
    }
}
