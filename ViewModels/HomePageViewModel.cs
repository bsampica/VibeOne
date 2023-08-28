using ReactiveUI;
namespace VibeOne.ViewModels;

public class HomePageViewModel : ViewModelBase, IRoutableViewModel
{
    public string? UrlPathSegment { get; } = "/home";
    public IScreen HostScreen { get; }

    public HomePageViewModel(IScreen screen)
    { 
        Console.WriteLine("Home Page View Model Constructor()!");
        HostScreen = screen;
    }
}
