using System.Reactive;
using ReactiveUI;
using Splat;

namespace VibeOne.ViewModels;

public class MainWindowViewModel : ViewModelBase, IScreen
{
    public RoutingState Router { get; } = Locator.Current.GetService<RoutingState>()!;
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateHome { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateOps { get; }

    public MainWindowViewModel()
    {
        // NAVIGATE to the default page when the app opens.
        Router.Navigate.Execute(new TankDetailsViewModel(this));

        NavigateHome =
            ReactiveCommand.CreateFromObservable(() =>
                Router.Navigate.Execute(new TankDetailsViewModel(this)));

        NavigateOps =
            ReactiveCommand.CreateFromObservable(() =>
                Router.Navigate.Execute(new OperationsViewModel(this)));
    }
}
