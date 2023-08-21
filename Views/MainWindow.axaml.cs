using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Material.Icons;
using Material.Icons.Avalonia;

namespace VibeNine.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
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
    }}