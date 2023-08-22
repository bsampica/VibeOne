using System;
using System.Linq;
using System.Reactive;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using VibeNine.Views;

namespace VibeNine.ViewModels;

public class MainWindowViewModel : ViewModelBase, IScreen
{
    public override string? UrlPathSegment { get => "MainWindow"; }
    public override IScreen? HostScreen { get => throw new NotImplementedException(); }

    public RoutingState Router { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToOperations { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToHomeScreen { get; }

    public MainWindowViewModel()
    {
        Router = new RoutingState();
        Locator.CurrentMutable.RegisterViewsForViewModels(Assembly.GetCallingAssembly());

        // Manage the routing state. Use the Router.Navigate.Execute
        // command to navigate to different view models. 
        //
        // Note, that the Navigate.Execute method accepts an instance 
        // of a view model, this allows you to pass parameters to 
        // your view models, or to reuse existing view models.
        //
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
