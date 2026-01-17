using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DocsUnoTesting.Models;
using DocsUnoTesting.Repositories;
using Microsoft.UI.Xaml;
using Uno.Extensions.Navigation;

namespace DocsUnoTesting.Presentation;

public partial class TestResultsViewModel : ObservableObject
{
    private readonly StudentRepository _studentRepository;
    private readonly TestRepository _testRepository;
    private readonly TestResultRepository _testResultRepository;
    private readonly INavigator _navigator;

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
    public ICommand DeleteTestResultCommand { get; }
    public ICommand EditTestResultCommand { get; }
    public ICommand NavigateToDetailsCommand { get; }

    public TestResultsViewModel(
        StudentRepository studentRepository, 
        TestRepository testRepository, 
        TestResultRepository testResultRepository,
        INavigator navigator)
    {
        _studentRepository = studentRepository;
        _testRepository = testRepository;
        _testResultRepository = testResultRepository;
        _navigator = navigator;

        CreateTestResultCommand = new AsyncRelayCommand<XamlRoot>(CreateTestResult);
        DeleteTestResultCommand = new AsyncRelayCommand<TestResult>(DeleteTestResult);
        EditTestResultCommand = new AsyncRelayCommand<TestResult>(EditTestResult);
        NavigateToDetailsCommand = new AsyncRelayCommand<TestResult>(NavigateToDetails);
        LoadInitialData();
    }

    private async Task NavigateToDetails(TestResult? testResult)
    {
        await _navigator.NavigateViewModelAsync<TestResultDetailsViewModel>(this, data: testResult);
    }

    private void LoadInitialData()
    {
        _studentRepository.GetAll().ToList().ForEach(Students.Add);
        _testRepository.GetAll().ToList().ForEach(Tests.Add);
        _testResultRepository.GetAll().ToList().ForEach(TestResults.Add);
    }

    private async Task CreateTestResult(XamlRoot? xamlRoot)
    {
        SelectedStudent = null;
        SelectedTest = null;
        Score = 0;

        var dialog = new CreateTestResultDialog
        {
            DataContext = this,
            XamlRoot = xamlRoot,
            Title = "Create Test Result"
        };

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            try
            {
                var newTestResult = new TestResult(SelectedTest!, SelectedStudent!, Score);
                _testResultRepository.Add(newTestResult);
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

    private XamlRoot? GetXamlRoot()
    {
        if (App.Current is App app && app.MainWindow is not null && app.MainWindow.Content is not null)
        {
            return app.MainWindow.Content.XamlRoot;
        }
        return null;
    }

    private async Task DeleteTestResult(TestResult? testResult)
    {
        if (testResult is null) return;

        var xamlRoot = GetXamlRoot();
        if (xamlRoot is null) return;

        var dialog = new ContentDialog
        {
            Title = "Delete Test Result",
            Content = "Are you sure you want to delete this test result?",
            PrimaryButtonText = "Delete",
            SecondaryButtonText = "Cancel",
            XamlRoot = xamlRoot
        };

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            _testResultRepository.Delete(testResult.Id);
            TestResults.Remove(testResult);
        }
    }

    private async Task EditTestResult(TestResult? testResult)
    {
        if (testResult is null) return;
        
        var xamlRoot = GetXamlRoot();
        if (xamlRoot is null) return;

        SelectedStudent = testResult.Student;
        SelectedTest = testResult.PassedTest;
        Score = testResult.Score;

        var dialog = new CreateTestResultDialog
        {
            DataContext = this,
            XamlRoot = xamlRoot,
            Title = "Edit Test Result"
        };

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            try
            {
                var updatedTestResult = new TestResult(SelectedTest!, SelectedStudent!, Score);
                _testResultRepository.Update(updatedTestResult);
                
                // To update the UI, we remove the old one and add a new one.
                TestResults.Remove(testResult);
                TestResults.Add(updatedTestResult);

                await new ContentDialog
                {
                    Title = "Success",
                    Content = "Test result updated successfully.",
                    CloseButtonText = "OK",
                    XamlRoot = xamlRoot
                }.ShowAsync();
            }
            catch (Exception e)
            {
                await new ContentDialog
                {
                    Title = "Error",
                    Content = $"Error updating test result: {e.Message}",
                    CloseButtonText = "OK",
                    XamlRoot = xamlRoot
                }.ShowAsync();
            }
        }
    }
}
