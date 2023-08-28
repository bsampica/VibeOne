// REACTIVE UI SPLAT LOCATOR
// https://github.com/AvaloniaUI/Avalonia/blob/master/samples/ReactiveUIDemo/App.axaml.cs

using System.Reflection;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Splat;
using VibeOne.ViewModels;
using VibeOne.Views;


namespace VibeOne;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        Locator.CurrentMutable.RegisterLazySingleton(() => new RoutingState());
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow() { DataContext = new MainWindowViewModel() };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
