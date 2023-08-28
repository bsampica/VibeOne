using System.Reactive;
using System.Windows.Input;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using VibeOne.Views;

namespace VibeOne.ViewModels;

public class MainWindowViewModel : ViewModelBase, IScreen
{
    public RoutingState Router { get; } = Locator.Current.GetService<RoutingState>()!;
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateHome { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateOps { get; }

    public MainWindowViewModel()
    {
        Console.WriteLine("Main Window View Model Constructor()!");
        // NAVIGATE to the default page when the app opens.
        Router.Navigate.Execute(new HomePageViewModel(this, Router));

        NavigateHome =
            ReactiveCommand.CreateFromObservable(() =>
                Router.Navigate.Execute(new HomePageViewModel(this, Router)));

        NavigateOps =
            ReactiveCommand.CreateFromObservable(() =>
                Router.Navigate.Execute(new OperationsViewModel(this)));
    }
}
