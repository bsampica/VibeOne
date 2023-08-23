using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using VibeOne.ViewModels;
using VibeOne.Views;

namespace VibeOne;

public partial class App : Application
{
    // public static PageViewModelBase HomePageViewModel = new HomePageViewModel();
    // public static PageViewModelBase TankDetailsViewModel = new TankDetailsViewModel();
    // public static PageViewModelBase OperationsViewModel = new OperationsViewModel();
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}