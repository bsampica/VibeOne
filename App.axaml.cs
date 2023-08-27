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
        InitContainer();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new ApplicationWindow() { DataContext = new ApplicationWindowViewModel() };
        }


        base.OnFrameworkInitializationCompleted();
    }

    public void InitContainer()
    {
        Locator.CurrentMutable.RegisterLazySingleton(() => new OperationsViewModel());
        Locator.CurrentMutable.RegisterLazySingleton(() => new TankDetailsViewModel());
        Locator.CurrentMutable.RegisterLazySingleton(() => new MainWindowViewModel());
        Locator.CurrentMutable.RegisterLazySingleton(() => new HomePageViewModel());

        // Locator.CurrentMutable.Register(() => new HomePageView(), typeof(IViewFor<HomePageViewModel>));
        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetExecutingAssembly());
    }
}
