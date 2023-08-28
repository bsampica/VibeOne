using Avalonia.ReactiveUI;
using VibeOne.ViewModels;


namespace VibeOne.Views;

public partial class TankDetailsView : ReactiveUserControl<TankDetailsViewModel>
{
    public TankDetailsView()
    {
        InitializeComponent();
    }
}
