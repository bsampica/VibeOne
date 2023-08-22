using System.Reactive;
using System.Reflection;
using ReactiveUI;
using Splat;

namespace VibeNine.ViewModels;

public class MainWindowViewModel : ViewModelBase, IScreen
{
    public override string UrlPathSegment { get => "MainWindow"; }
    public override IScreen HostScreen { get; } = null!;
    public RoutingState Router { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToOperations { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToHomeScreen { get; }

    public MainWindowViewModel()
    {
        Router = new RoutingState();
        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());
      
        NavigateToOperations = ReactiveCommand.CreateFromObservable(() =>
            Router.Navigate.Execute(ExecuteNavOperation()));

        NavigateToHomeScreen = ReactiveCommand.CreateFromObservable(() =>
            Router.Navigate.Execute(ExecuteNavHomepage()));
    }

    private IRoutableViewModel ExecuteNavOperation()
    {
        Console.WriteLine("Executing the Operations Link");
        return new OperationsViewModel();
    }

    private IRoutableViewModel ExecuteNavHomepage()
    {
        Console.WriteLine("Executing the Homepage Link");
        return new HomePageViewModel();
    }
}
