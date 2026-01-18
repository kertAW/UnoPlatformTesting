using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using DocsUnoTesting.Models;
using Microsoft.UI.Xaml.Controls;

namespace DocsUnoTesting.Presentation;

public partial class TreeViewModel : ObservableObject
{
    public ObservableCollection<TestResult> TestResults { get; } = new();
    public ObservableCollection<TreeViewNode> TreeNodes { get; } = new();

    public TreeViewModel()
    {
        GenerateMockData();
    }

    private TreeViewNode CreateNode(TestStageResultTreeItemViewModel vm)
    {
        var node = new TreeViewNode { Content = vm };
        foreach (var childVm in vm.Children)
        {
            node.Children.Add(CreateNode(childVm));
        }
        return node;
    }

    private void GenerateMockData()
    {
        // Mock data generation

        // Test 1
        var student1 = new Student("John Doe");
        var test1 = new Test("Math Test", 0, 100);

        // TestStages for Test 1
        var algebraStage = new TestStage("1. Algebra", test1, 0, 100);
        var equationsStage = new TestStage("1.1. Equations", test1, 0, 100, algebraStage);
        var linearEqStage = new TestStage("1.1.1. Linear", test1, 0, 100, equationsStage);
        var quadraticEqStage = new TestStage("1.1.2. Quadratic", test1, 0, 100, equationsStage);
        var geometryStage = new TestStage("1.2. Geometry", test1, 0, 100, algebraStage);
        var calculusStage = new TestStage("2. Calculus", test1, 0, 100);

        // TestStageResults for Test 1
        var rootStageResult1 = new TestStageResult(null!, algebraStage, 80) { IsPassed = true, Comment = "Good" };
        var subStageResult1_1 = new TestStageResult(null!, equationsStage, 90) { IsPassed = true, Comment = "Excellent" };
        var subStageResult1_1_1 = new TestStageResult(null!, linearEqStage, 95) { IsPassed = true, Comment = "Very solid." };
        var subStageResult1_1_2 = new TestStageResult(null!, quadraticEqStage, 85) { IsPassed = true, Comment = "Good effort." };
        subStageResult1_1.Children.Add(subStageResult1_1_1);
        subStageResult1_1.Children.Add(subStageResult1_1_2);
        var subStageResult1_2 = new TestStageResult(null!, geometryStage, 70) { IsPassed = false, Comment = "Needs improvement" };
        rootStageResult1.Children.Add(subStageResult1_1);
        rootStageResult1.Children.Add(subStageResult1_2);
        var rootStageResult2 = new TestStageResult(null!, calculusStage, 85) { IsPassed = true, Comment = "Very Good" };
        var testResult1 = new TestResult(test1, student1, 82.5f, new[] { rootStageResult1, rootStageResult2 });
        TestResults.Add(testResult1);


        // Test 2
        var student2 = new Student("Jane Smith");
        var test2 = new Test("Science Test", 0, 100);
        var physicsStage = new TestStage("Physics", test2, 0, 100);
        var mechanicsStage = new TestStage("Mechanics", test2, 0, 100, physicsStage);
        var rootStageResult3 = new TestStageResult(null!, physicsStage, 95) { IsPassed = true, Comment = "Outstanding" };
        var subStageResult3_1 = new TestStageResult(null!, mechanicsStage, 92) { IsPassed = true, Comment = "Great" };
        rootStageResult3.Children.Add(subStageResult3_1);
        var testResult2 = new TestResult(test2, student2, 95f, new[] { rootStageResult3 });
        TestResults.Add(testResult2);

        // Test 3 - Deep Hierarchy
        var student3 = new Student("Peter Jones");
        var test3 = new Test("Computer Science Fundamentals", 0, 100);
        
        // Level 1
        var l1_programming = new TestStage("L1 - Programming", test3, 0, 100);
        // Level 2
        var l2_datastructs = new TestStage("L2 - Data Structures", test3, 0, 100, l1_programming);
        // Level 3
        var l3_arrays = new TestStage("L3 - Arrays", test3, 0, 100, l2_datastructs);
        // Level 4
        var l4_2darrays = new TestStage("L4 - 2D Arrays", test3, 0, 100, l3_arrays);
        // Level 5
        var l5_matrix = new TestStage("L5 - Matrix Manipulation", test3, 0, 100, l4_2darrays);

        var l1_res = new TestStageResult(null!, l1_programming, 88);
        var l2_res = new TestStageResult(null!, l2_datastructs, 91);
        var l3_res = new TestStageResult(null!, l3_arrays, 95);
        var l4_res = new TestStageResult(null!, l4_2darrays, 85);
        var l5_res = new TestStageResult(null!, l5_matrix, 82) { Comment = "Slightly slow performance."};
        
        l4_res.Children.Add(l5_res);
        l3_res.Children.Add(l4_res);
        l2_res.Children.Add(l3_res);
        l1_res.Children.Add(l2_res);

        var testResult3 = new TestResult(test3, student3, 89f, new[] { l1_res });
        TestResults.Add(testResult3);
        
        TreeNodes.Clear();
        foreach (var testResult in TestResults)
        {
            foreach (var stageResult in testResult.StageResults)
            {
                var vm = new TestStageResultTreeItemViewModel(stageResult);
                var node = CreateNode(vm);
                TreeNodes.Add(node);
            }
        }
    }
}
