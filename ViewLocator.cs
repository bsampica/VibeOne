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
    // public Control? Build(object? data)
    // {
    //     if (data is null) return new TextBlock { Text = "Data was null" };
    //     var name = data.GetType().FullName!.Replace("ViewModel", "View");
    //     var type = Type.GetType(name);
    //     
    //     return (type != null)
    //         ? (Control)Activator.CreateInstance(type)!
    //         : new TextBlock() { Text = "View was not found: " + name };
    // }
    //
    // public bool Match(object? data)
    // {
    //     return data is ViewModelBase;
    // }

    public IViewFor ResolveView<T>(T viewModel, string contract = null)
    {
        switch (viewModel)
        {
            case HomePageViewModel context:
                return new HomePageView() { DataContext = context };
            case OperationsViewModel context:
                return new OperationsView() { DataContext = context };
            case TankDetailsViewModel context:
                return new TankDetailsView() { DataContext = context };
            default:
                throw new ArgumentOutOfRangeException(nameof(viewModel));
        }
    }
}
