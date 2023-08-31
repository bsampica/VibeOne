using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using DynamicData.Aggregation;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using VibeOne.Models;
using VibeOne.Services;

namespace VibeOne.ViewModels;

public class HomePageViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment { get; } = "/home";
    public IScreen HostScreen { get; }
    private RoutingState Router { get; }
    private TankService TankService { get; } = Locator.Current.GetService<TankService>()!;
    [Reactive] public IEnumerable<TankModel> TankModels { get; set; }
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateTankDetails { get; }

    public HomePageViewModel()
    {
    }

    public HomePageViewModel(IScreen screen, RoutingState router)
    {
        HostScreen = screen;
        Router = router;
        TankModels = TankService.Tanks;

        NavigateTankDetails = ReactiveCommand.CreateFromObservable(() =>
            Router.Navigate.Execute(new TankDetailsViewModel(HostScreen)));
    }
}
