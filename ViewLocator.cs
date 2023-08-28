using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using ReactiveUI;
using VibeOne.ViewModels;
using VibeOne.Views;

namespace VibeOne;

public class ViewLocator : IViewLocator
{
    public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
    {
        return viewModel switch
        {
            HomePageViewModel context => new HomePageView() { DataContext = context },
            OperationsViewModel context => new OperationsView() { DataContext = context },
            TankDetailsViewModel context => new TankDetailsView() { DataContext = context },
            _ => throw new ArgumentOutOfRangeException(nameof(viewModel)),
        };
    }
}
