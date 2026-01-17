using CommunityToolkit.Mvvm.ComponentModel;
using DocsUnoTesting.Models;
using Uno.Extensions.Navigation; // Add this using directive if needed for INavigable or similar

namespace DocsUnoTesting.Presentation;

[ObservableObject]
public partial class TestResultDetailsViewModel //: INavigable // Might need to implement this
{
    [ObservableProperty]
    private TestResult? _testResult;

    /// <summary>
    /// Initializes the view model with the TestResult data.
    /// This method is typically called by the navigation service when navigating to this view model
    /// with a data parameter.
    /// </summary>
    /// <param name="testResult">The TestResult object to display.</param>
    public void Initialize(TestResult testResult)
    {
        TestResult = testResult;
    }
}
