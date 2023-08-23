using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using Material.Icons;
using Material.Icons.Avalonia;
using ReactiveUI;
using VibeOne.ViewModels;

namespace VibeOne.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        ViewModel = new MainWindowViewModel();
        this.WhenActivated(d =>
        {
            // this.OneWayBind(ViewModel,
            //     x => x.Router,
            //     x => x.RoutedViewHost.Router);
        });
    }

    private void ToggleMenu_OnClick(object? sender, RoutedEventArgs e)
    {
        this.SplitViewMenu.IsPaneOpen = !this.SplitViewMenu.IsPaneOpen;
        if (this.SplitViewMenu.IsPaneOpen)
        {
            if ((sender as Button)?.Content is MaterialIcon currentButtonContent)
                currentButtonContent.Kind = MaterialIconKind.HamburgerOpen;
        }
        else
        {
            if ((sender as Button)?.Content is MaterialIcon currentButtonContent)
                currentButtonContent.Kind = MaterialIconKind.HamburgerClose;
        }
    }
}
