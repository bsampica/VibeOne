// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Reactive;
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
    private RoutingState? Router { get; }
    private TankService TankService { get; } = Locator.Current.GetService<TankService>()!;
    [Reactive] public IEnumerable<TankModel>? TankModels { get; set; }
    public ReactiveCommand<Unit, IRoutableViewModel>? NavigateTankDetails { get; }

    public HomePageViewModel(IScreen screen, RoutingState? router)
    {
        HostScreen = screen;
        Router = router;
        TankModels = TankService.Tanks;

        NavigateTankDetails = ReactiveCommand.CreateFromObservable(() =>
            Router?.Navigate.Execute(new TankDetailsViewModel(screen))!);
    }
}
