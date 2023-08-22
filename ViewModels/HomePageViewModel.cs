using System;
using System.Windows.Input;
using ReactiveUI;
using Splat;


namespace VibeNine.ViewModels;

public class HomePageViewModel : PageViewModelBase
{
    public sealed override string? UrlPathSegment { get => "Home"; }
    public sealed override IScreen HostScreen { get; }
    public string Title => "Welcome to the Home Page View: ";

    public HomePageViewModel(IScreen? screen = null)
    {
        HostScreen = screen ?? Locator.Current.GetService<IScreen>();
    }
}
