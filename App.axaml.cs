// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Splat;
using VibeOne.Operations;
using VibeOne.Services;
using VibeOne.ViewModels;
using VibeOne.Views;


namespace VibeOne;

public static class DeviceInstance
{
    public static bool IsRPI { get; set; }
}

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        // Locator.CurrentMutable.RegisterLazySingleton(() => new RoutingState());
        // Locator.CurrentMutable.RegisterLazySingleton(() => new TankService());
        // Locator.CurrentMutable.RegisterLazySingleton(() => new SensorService());
        // Locator.CurrentMutable.RegisterLazySingleton(() => new RelayService());
        Locator.CurrentMutable.RegisterLazySingleton(() => new CancellationTokenSource());
        SplatRegistrations.RegisterLazySingleton<RoutingState>();
        SplatRegistrations.RegisterLazySingleton<TankService>();
        SplatRegistrations.RegisterLazySingleton<SensorService>();
        SplatRegistrations.RegisterLazySingleton<RelayService>();
        SplatRegistrations.RegisterLazySingleton<IAutoOperation, Co2TankOperation>();
        SplatRegistrations.SetupIOC();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            DeviceInstance.IsRPI = false;
            Console.WriteLine($"Detected Classic Destkop Style Application Lifetime");
            desktop.MainWindow = new MainWindow() { DataContext = new MainWindowViewModel() };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleView)
        {
            DeviceInstance.IsRPI = true;
            Console.WriteLine($"Detected Single View Style Application Lifetime");
            singleView.MainView = new MainView() { DataContext = new MainWindowViewModel() };
        }

        StartupRelayTest();
        StartupSensorTest();
        base.OnFrameworkInitializationCompleted();
    }

    private void StartupSensorTest()
    {
        var sensorService = Locator.Current.GetService<SensorService>();
        Task.Run(() =>
        {
            Console.WriteLine("Starting the Sensor Service");
            sensorService?.StartTemperatureMonitorAsync();
        });
    }

    private void StartupRelayTest()
    {
        var relayService = Locator.Current.GetService<RelayService>();
        relayService?.TriggerRelayAsync();
    }
}
