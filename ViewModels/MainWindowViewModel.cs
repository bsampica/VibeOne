using System.Reactive;
using System.Reflection;
using ReactiveUI;
using Splat;

namespace VibeNine.ViewModels;

public class MainWindowViewModel : ViewModelBase, IScreen
{
    public override string UrlPathSegment { get => "MainWindow"; }
    public RoutingState Router { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToOperations { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToHomeScreen { get; }

    public MainWindowViewModel()
    {
        Router = new RoutingState();
        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
        Locator.CurrentMutable.Register(() =>  Router);

        NavigateToOperations = ReactiveCommand.CreateFromObservable(() =>
            Router.Navigate.Execute(new OperationsViewModel()));

        NavigateToHomeScreen = ReactiveCommand.CreateFromObservable(() =>
            Router.Navigate.Execute(new HomePageViewModel()));
    }
}
