using ReactiveUI;

namespace VibeNine.ViewModels;

public class OperationsViewModel : PageViewModelBase
{
    public string Title = "Welcome to the operations page";
    public override string? UrlPathSegment { get; }
    public override IScreen HostScreen { get; }
}
