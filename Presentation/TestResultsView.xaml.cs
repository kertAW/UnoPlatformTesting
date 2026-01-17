using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Controls;

namespace DocsUnoTesting.Presentation;

public sealed partial class TestResultsView : UserControl
{
    public TestResultsView()
    {
        this.InitializeComponent();
    }

    private void ListView_ItemClick(object sender, ItemClickEventArgs e)
    {
        if (e.ClickedItem is TestResult testResult)
        {
            if (DataContext is TestResultsViewModel viewModel)
            {
                viewModel.NavigateToDetailsCommand.Execute(testResult);
            }
        }
    }
}
