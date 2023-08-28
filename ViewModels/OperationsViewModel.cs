using ReactiveUI;
namespace VibeOne.ViewModels;

public class OperationsViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment { get; } = "/operations";
    public IScreen HostScreen { get; }

    public OperationsViewModel(IScreen screen) => HostScreen = screen;

}
