using CommunityToolkit.Mvvm.Input;
using NowPlus.Cross.Models;

namespace NowPlus.Cross.PageModels
{
    public interface IProjectTaskPageModel
    {
        IAsyncRelayCommand<ProjectTask> NavigateToTaskCommand { get; }
        bool IsBusy { get; }
    }
}