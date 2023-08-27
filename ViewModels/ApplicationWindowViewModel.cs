// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Windows.Input;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;

namespace VibeOne.ViewModels;

public class ApplicationWindowViewModel : ViewModelBase
{

    [Reactive] public ReactiveObject CurrentPage { get; set; }

    public ICommand ChangeModelCommand { get; set; }

    public ApplicationWindowViewModel()
    {
        ChangeModelCommand = ReactiveCommand.Create(ChangeModel);
        CurrentPage = new HomePageViewModel();
    }

    private void ChangeModel()
    {
        switch (CurrentPage)
        {
            case OperationsViewModel:
                CurrentPage = Locator.Current.GetService<TankDetailsViewModel>();
                break;
            case HomePageViewModel:
                CurrentPage = Locator.Current.GetService<OperationsViewModel>();
                break;
            case TankDetailsViewModel:
                CurrentPage = Locator.Current.GetService<HomePageViewModel>();
                break;
            default:
                CurrentPage = Locator.Current.GetService<HomePageViewModel>();
                break;
        }

    }
}
