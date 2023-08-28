using System.Numerics;
using System.Reactive;
using ReactiveUI;
using Splat;

namespace VibeOne.ViewModels;

public class TankDetailsViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment { get; }
    public IScreen HostScreen { get; }

    private readonly RoutingState _router = Locator.Current.GetService<RoutingState>()!;

    public ReactiveCommand<Unit, IRoutableViewModel?> NavigateBack { get; }

    public TankDetailsViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        NavigateBack = ReactiveCommand.CreateFromObservable(() => _router.NavigateBack.Execute());
    }
}
