using System.Reactive;
using ReactiveUI;
using Splat;


namespace VibeNine.ViewModels;

public class HomePageViewModel : PageViewModelBase
{
    private RoutingState Router { get; } = Locator.Current.GetService<RoutingState>()!;
    public sealed override string UrlPathSegment { get => "HomePageView"; }

    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToTankDetails
    {
        get =>
            ReactiveCommand.CreateFromObservable(() =>
                Router.Navigate.Execute(HandleNavigation()));
    }

    public IRoutableViewModel HandleNavigation()
    {
        Console.WriteLine(Router.GetCurrentViewModel());
        Console.WriteLine(Router);
        return new TankDetailsViewModel();
    }
}
