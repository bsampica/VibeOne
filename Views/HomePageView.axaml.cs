using Avalonia.ReactiveUI;
using VibeOne.ViewModels;

namespace VibeOne.Views;

public partial class HomePageView : ReactiveUserControl<HomePageViewModel>
{
    public HomePageView()
    {
        InitializeComponent();
    }
}
