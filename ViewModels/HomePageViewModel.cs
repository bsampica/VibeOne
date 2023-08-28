using System.Reactive;
using System.Windows.Input;
using ReactiveUI;
using Splat;

namespace VibeOne.ViewModels;

public class HomePageViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment { get; } = "/home";
    public IScreen HostScreen { get; }

    private RoutingState Router { get; }

    public ReactiveCommand<Unit, IRoutableViewModel> NavigateTankDetails { get; }

    public HomePageViewModel(IScreen screen, RoutingState router)
    {
        HostScreen = screen;
        Router = router;

        NavigateTankDetails = ReactiveCommand.CreateFromObservable(() =>
            Router.Navigate.Execute(new TankDetailsViewModel(HostScreen)));
    }
}
