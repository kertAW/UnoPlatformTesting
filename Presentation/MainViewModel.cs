using DocsUnoTesting.Repositories;

namespace DocsUnoTesting.Presentation;

public partial class MainViewModel : ObservableObject
{
    private INavigator _navigator;

    [ObservableProperty]
    private string? name;

    public MainViewModel(
        IStringLocalizer localizer,
        IOptions<AppConfig> appInfo,
        INavigator navigator,
        StudentRepository studentRepository,
        TestRepository testRepository,
        TestResultRepository testResultRepository
    )
    {
        _navigator = navigator;
        Title = "Main";
        Title += $" - {localizer["ApplicationName"]}";
        Title += $" - {appInfo?.Value?.Environment}";
        GoToSecond = new AsyncRelayCommand(GoToSecondView);
        ShowTreeViewCommand = new AsyncRelayCommand(ShowTreeView);
        TestResultsViewModel = new TestResultsViewModel(
            studentRepository,
            testRepository,
            testResultRepository,
            navigator
        );
    }

    public string? Title { get; }

    public TestResultsViewModel TestResultsViewModel { get; }

    public ICommand GoToSecond { get; }
    public ICommand ShowTreeViewCommand { get; }

    private async Task GoToSecondView()
    {
        await _navigator.NavigateViewModelAsync<SecondViewModel>(this, data: new Entity(Name!));
    }

    private async Task ShowTreeView()
    {
        await _navigator.NavigateViewModelAsync<TreeViewModel>(this);
    }
}
