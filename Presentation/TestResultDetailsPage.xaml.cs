using DocsUnoTesting.Models;
using Microsoft.UI.Xaml.Navigation;

namespace DocsUnoTesting.Presentation;

public sealed partial class TestResultDetailsPage : Page
{
    private readonly TestResultDetailsViewModel _viewModel;

    public TestResultDetailsPage(TestResultDetailsViewModel viewModel)
    {
        _viewModel = viewModel;
        this.InitializeComponent();
        this.DataContext = viewModel;
    }

    public TestResultDetailsPage()
    {
        this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        if (e.Parameter is TestResult testResult)
        {
            _viewModel.Initialize(testResult);
        }
    }
}
