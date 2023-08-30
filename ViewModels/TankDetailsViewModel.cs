using System.Collections.Generic;
using System.Reactive;
using Avalonia.Media;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.VisualElements;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SkiaSharp;
using Splat;
using VibeOne.Services;

namespace VibeOne.ViewModels;

public class TankDetailsViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment { get; } = "/tankdetails";
    public IScreen HostScreen { get; }
    private readonly RoutingState _router = Locator.Current.GetService<RoutingState>()!;
    public ReactiveCommand<Unit, IRoutableViewModel?> NavigateBack { get; }
    public ReactiveCommand<Unit, Unit> DoRandomChange { get; }

    [Reactive] public IEnumerable<ISeries> Series { get; set; }

    [Reactive] public NeedleVisual Needle { get; set; }

    private readonly Random _random = new();

    [Reactive] public IEnumerable<VisualElement<SkiaSharpDrawingContext>> VisualElements { get; set; }

    public Task DoRandomChangeExecute()
    {
        Needle.Value = _random.Next(-100, 20);
        return Task.CompletedTask;
    }

    public TankDetailsViewModel(IScreen hostScreen)
    {
        HostScreen = hostScreen;
        NavigateBack = ReactiveCommand.CreateFromObservable(() => _router.NavigateBack.Execute());
        DoRandomChange = ReactiveCommand.CreateFromTask(DoRandomChangeExecute);

        var sectionsOuter = 130;
        var sectionsWidth = 20;

        Needle = new NeedleVisual() { Value = 45, ScalesXAt = 145, ScalesYAt = 145 };


        Series = GaugeGenerator.BuildAngularGaugeSections(
            new GaugeItem(60, s => SetStyle(sectionsOuter, sectionsWidth, s)),
            new GaugeItem(30, s => SetStyle(sectionsOuter, sectionsWidth, s)),
            new GaugeItem(10, s => SetStyle(sectionsOuter, sectionsWidth, s)));

        VisualElements = new VisualElement<SkiaSharpDrawingContext>[]
        {
            new AngularTicksVisual()
            {
                LabelsSize = 16,
                LabelsOuterOffset = 15,
                OuterOffset = 65,
                TicksLength = 20,
                Stroke = new SolidColorPaint(SKColors.Brown),
            },
            Needle,
        };

        static void SetStyle(double sectionsOuter, double sectionsWidth, PieSeries<ObservableValue> series)
        {
            series.OuterRadiusOffset = sectionsOuter;
            series.MaxRadialColumnWidth = sectionsWidth;
            series.AnimationsSpeed = new TimeSpan(0, 0, 0, 0, 300);
        }
    }
}
