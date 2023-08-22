using Avalonia.ReactiveUI;
using VibeNine.ViewModels;

namespace VibeNine.Views;

public partial class HomePageView : ReactiveUserControl<HomePageViewModel>
{
    public HomePageView()
    {
        InitializeComponent();
    }
}
