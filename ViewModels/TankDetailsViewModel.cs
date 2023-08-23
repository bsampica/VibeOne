using System;
using System.Collections.Generic;
using System.Reactive;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Kernel;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Extensions;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace VibeOne.ViewModels;

public class TankDetailsViewModel : PageViewModelBase
{
    public IEnumerable<ISeries> Series { get; set; } = new GaugeBuilder()
        .WithOffsetRadius(5)
        .WithLabelsPosition(PolarLabelsPosition.Start)
        .WithMaxColumnWidth(15)
        .AddValue(29, "Actual")
        .AddValue(-4, "SetPoint")
        .BuildSeries();


    public override string? UrlPathSegment { get => "TankDetailsView"; }
    private RoutingState Router { get => Locator.Current.GetService<RoutingState>()!; }

    public ReactiveCommand<Unit, IRoutableViewModel?> NavigateBackwardsFromTankDetails
    {
        get =>
            ReactiveCommand.CreateFromObservable(() => Router.NavigateBack.Execute());
    }
}
