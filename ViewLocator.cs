using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using ReactiveUI;
using VibeNine.ViewModels;
using VibeNine.Views;

namespace VibeNine;

public class ViewLocator : IViewLocator
{
    public IViewFor ResolveView<T>(T? viewModel, string? contract = null)
    {
        if (viewModel is HomePageViewModel)
            return new HomePageView() { ViewModel = viewModel as HomePageViewModel };
        if (viewModel is OperationsViewModel)
            return new OperationsView () { ViewModel = viewModel as OperationsViewModel };
        throw new Exception($"Could not find the view for view model {typeof(T).Name}.");
    }
}
