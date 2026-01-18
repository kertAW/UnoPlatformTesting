using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using DocsUnoTesting.Models;

namespace DocsUnoTesting.Presentation;

public partial class TestResultDetailsViewModel : ObservableObject
{
    [ObservableProperty]
    private TestResult? _testResult;

    [ObservableProperty]
    private ObservableCollection<TestStageResultDisplayViewModel> _rootStageResults = new(); // Changed type

    [ObservableProperty]
    private string _formattedScore = string.Empty;

    partial void OnTestResultChanged(TestResult? oldValue, TestResult? newValue)
    {
        if (newValue != null)
            Initialize(newValue);
    }

    /// <summary>
    /// Initializes the view model with the TestResult data.
    /// This method is typically called by the navigation service when navigating to this view model
    /// with a data parameter.
    /// </summary>
    /// <param name="testResult">The TestResult object to display.</param>
    public void Initialize(TestResult testResult)
    {
        TestResult = testResult;
        FormattedScore = $"Score: {TestResult.Score:F2}";

        // Populate RootStageResults with wrapper ViewModels
        RootStageResults.Clear();
        if (TestResult?.StageResults != null)
        {
            var allStageResults = TestResult.StageResults.ToList();
            foreach (var stageResult in allStageResults.Where(sr => sr.Stage.ParentStage == null))
            {
                RootStageResults.Add(
                    new TestStageResultDisplayViewModel(stageResult, allStageResults)
                );
            }
        }
    }
}
