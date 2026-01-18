using CommunityToolkit.Mvvm.ComponentModel;
using DocsUnoTesting.Models;
using System.Collections.ObjectModel;

namespace DocsUnoTesting.Presentation;

public partial class TestStageResultTreeItemViewModel : ObservableObject
{
    public TestStageResult Model { get; }

    public ObservableCollection<TestStageResultTreeItemViewModel> Children { get; } = new();

    public string DisplayText => $"{Model.Stage.Name} - Score: {Model.Score:F2} ({Model.Comment})";

    public TestStageResultTreeItemViewModel(TestStageResult model)
    {
        Model = model;
        
        // Recursively create the hierarchy of view models
        foreach (var childModel in Model.Children)
        {
            Children.Add(new TestStageResultTreeItemViewModel(childModel));
        }
    }
}
