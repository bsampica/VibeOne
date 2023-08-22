using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using VibeNine.ViewModels;

namespace VibeNine.Views;

public partial class OperationsView : ReactiveUserControl<OperationsViewModel>
{
    public OperationsView()
    {
        InitializeComponent();
    }
}
