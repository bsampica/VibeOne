using System.Reactive;
using System.Reflection;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace VibeOne.ViewModels;

public class MainWindowViewModel : ViewModelBase, IScreen
{
    [Reactive] public bool IsBackButtonVisible { get; set; } = false;
    public override string UrlPathSegment { get => "MainWindow"; }
    public RoutingState Router { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToOperations { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToHomeScreen { get; }

    public ReactiveCommand<Unit, IRoutableViewModel?> NavigateBackwards { get; }

    public MainWindowViewModel()
    {
        Router = new RoutingState();
        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
        Locator.CurrentMutable.Register(() => Router);

        NavigateToOperations = ReactiveCommand.CreateFromObservable(() =>
            Router.Navigate.Execute(new OperationsViewModel()));

        NavigateToHomeScreen = ReactiveCommand.CreateFromObservable(() =>
            Router.Navigate.Execute(new HomePageViewModel()));

        NavigateBackwards = ReactiveCommand.CreateFromObservable(() =>
            Router.NavigateBack.Execute());
    }
}
