using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Iot.Device.Display;
using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Drawing.Geometries;
using LiveChartsCore.SkiaSharpView.Painting;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using SkiaSharp;
using Splat;
using VibeOne.Models;
using VibeOne.Operations;
using VibeOne.Services;

namespace VibeOne.ViewModels;

public class TankDetailsViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment { get; } = "/tankdetails";
    public IScreen HostScreen { get; }
    private readonly RoutingState _router = Locator.Current.GetService<RoutingState>()!;
    public ReactiveCommand<Unit, IRoutableViewModel?> NavigateBack { get; }

    public IObservable<DateTime> CurrentTime { get; set; }
        = Observable.Create<DateTime>(observer =>
        {
            var timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += (_, _) =>
            {
                observer.OnNext(DateTime.Now);
            };
            timer.Start();
            return Disposable.Empty;
        });

    private readonly SensorService _sensorService;
    private readonly IAutoOperation _co2Service;

    [Reactive] public TankModel SelectedTankModel { get; set; }
    [Reactive] public List<ISeries>? Series { get; set; }
    [Reactive] public double MainTankTemperature { get; set; }
    [Reactive] public double Tank1Temperature { get; set; }
    [Reactive] public double Tank2Temperature { get; set; }
    [Reactive] public double Tank3Temperature { get; set; }

    public TankDetailsViewModel(IScreen? hostScreen = null)
    {
        HostScreen = hostScreen;
        NavigateBack = ReactiveCommand.CreateFromObservable(() => _router.NavigateBack.Execute());
        var tankService = Locator.Current.GetService<TankService>();
        tankService?.MockData();
        SelectedTankModel = tankService?.Tanks!.First()!;

        _co2Service = Locator.Current.GetService<IAutoOperation>();
        Task.Run(async () =>
        {
            await _co2Service?.BeginOperation(() => { });
        });


        _sensorService =
            Locator.Current.GetService<SensorService>() ??
            new SensorService(); // TODO: Figure out how to handle null locator calls.

        _sensorService.PropertyChanged += HandleTemperatureChange;
        BuildChartSeriesData();
    }

    private void HandleTemperatureChange(object? sender, PropertyChangedEventArgs args)
    {
        Tank1Temperature = _sensorService.TemperatureOne - 10.7d;
        Tank2Temperature = _sensorService.TemperatureTwo;
        Tank3Temperature = _sensorService.TemperatureTwo + 10.2d;
        MainTankTemperature = _sensorService.TemperatureOne;
    }

    private void BuildChartSeriesData()
    {
        var values1 = new List<float>();
        var x = -0.05f;
        var fx = EasingFunctions.BounceInOut; // this is the function we are going to plot

        while (x <= 1f)
        {
            values1.Add(fx(x));
            x += .009f;
        }

        var valuesCopy = values1
            .OrderByDescending(ob => ob)
            .Select(s => float.Parse(s.ToString().Substring(2, 2)));


        var columnSeries1 = new ColumnSeries<float>
        {
            Values = valuesCopy, Stroke = null, Padding = 2, MaxBarWidth = double.PositiveInfinity,
        };

        columnSeries1.PointMeasured += OnPointMeasured;
        Series = new List<ISeries> { columnSeries1 };
    }

    private void OnPointMeasured(ChartPoint<float, RoundedRectangleGeometry, LabelGeometry> point)
    {
        var perPointDelay = 65; // milliseconds
        var delay = point.Context.Entity.MetaData!.EntityIndex * perPointDelay;
        var speed = (float)point.Context.Chart.AnimationsSpeed.TotalMilliseconds + delay;

        point.Visual?.SetTransition(
            new Animation(progress =>
                {
                    var d = delay / speed;

                    return progress <= d
                        ? 0
                        : EasingFunctions.BuildCustomElasticOut(1.5f, 0.60f)((progress - d) / (1 - d));
                },
                TimeSpan.FromMilliseconds(speed)));
    }
}
