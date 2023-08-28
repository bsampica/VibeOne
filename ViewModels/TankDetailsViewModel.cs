using ReactiveUI;


namespace VibeOne.ViewModels;

public class TankDetailsViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }

    public TankDetailsViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
    }
}
