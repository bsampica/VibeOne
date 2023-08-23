using System;
using System.Reactive;
using ReactiveUI;
using Splat;

namespace VibeOne.ViewModels;

public class TankDetailsViewModel : PageViewModelBase
{
    public override string? UrlPathSegment { get => "TankDetailsView"; }
    private RoutingState Router { get => Locator.Current.GetService<RoutingState>()!; }

    public ReactiveCommand<Unit, IRoutableViewModel?> NavigateBackwardsFromTankDetails
    {
        get =>
            ReactiveCommand.CreateFromObservable(() => Router.NavigateBack.Execute());
    }
}
