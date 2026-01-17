using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DocsUnoTesting.Models;
using Microsoft.UI.Xaml;

namespace DocsUnoTesting.Presentation;

public partial class TestResultsViewModel : ObservableObject
{
    public ObservableCollection<TestResult> TestResults { get; } = new();
    public ObservableCollection<Student> Students { get; } = new();
    public ObservableCollection<Test> Tests { get; } = new();

    [ObservableProperty]
    private Student? _selectedStudent;

    [ObservableProperty]
    private Test? _selectedTest;

    [ObservableProperty]
    private float _score;

    public ICommand CreateTestResultCommand { get; }

    public TestResultsViewModel()
    {
        CreateTestResultCommand = new AsyncRelayCommand<XamlRoot>(CreateTestResult);
        LoadInitialData();
    }

    private void LoadInitialData()
    {
        var students = new List<Student>
        {
            new("John Doe"),
            new("Jane Smith"),
            new("Peter Jones"),
            new("Mary Williams"),
            new("David Brown"),
            new("Susan Davis"),
            new("Michael Miller"),
            new("Linda Wilson"),
            new("Robert Moore"),
            new("Patricia Taylor")
        };
        students.ForEach(Students.Add);

        var tests = new List<Test>
        {
            new("Math Test", 0, 100),
            new("Science Test", 0, 100),
            new("History Test", 0, 100),
            new("Geography Test", 0, 100),
            new("English Test", 0, 100)
        };
        tests.ForEach(Tests.Add);

        var random = new Random();
        foreach (var student in students)
        {
            foreach (var test in tests)
            {
                if (random.Next(0, 2) == 0)
                {
                    TestResults.Add(new TestResult(test, student, random.Next((int)test.MinScore, (int)test.MaxScore)));
                }
            }
        }
    }

    private async Task CreateTestResult(XamlRoot? xamlRoot)
    {
        var dialog = new CreateTestResultDialog
        {
            DataContext = this,
            XamlRoot = xamlRoot
        };

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            try
            {
                var newTestResult = new TestResult(SelectedTest!, SelectedStudent!, Score);
                TestResults.Add(newTestResult);
                await new ContentDialog
                {
                    Title = "Success",
                    Content = "Test result created successfully.",
                    CloseButtonText = "OK",
                    XamlRoot = xamlRoot
                }.ShowAsync();
            }
            catch (Exception e)
            {
                await new ContentDialog
                {
                    Title = "Error",
                    Content = $"Error creating test result: {e.Message}",
                    CloseButtonText = "OK",
                    XamlRoot = xamlRoot
                }.ShowAsync();
            }
        }
    }
}
