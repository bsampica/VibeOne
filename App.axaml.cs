// REACTIVE UI SPLAT LOCATOR
// https://github.com/AvaloniaUI/Avalonia/blob/master/samples/ReactiveUIDemo/App.axaml.cs

using System.Reflection;
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
    public static bool IsRPI { get; set; } = false;
}

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        // Locator.CurrentMutable.RegisterLazySingleton(() => new RoutingState());
        // Locator.CurrentMutable.RegisterLazySingleton(() => new TankService());
        // Locator.CurrentMutable.RegisterLazySingleton(() => new SensorService());
        // Locator.CurrentMutable.RegisterLazySingleton(() => new RelayService());
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
