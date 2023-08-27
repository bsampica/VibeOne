﻿using System.Reactive;
using System.Windows.Input;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using DynamicData;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Splat;
using VibeOne.Views;

namespace VibeOne.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    [Reactive] public ReactiveObject CurrentPage { get; set; }

    public ICommand ChangeModelCommand { get; set; }

    public MainWindowViewModel()
    {
        ChangeModelCommand = ReactiveCommand.Create(ChangeModel);
        CurrentPage = new HomePageViewModel();
    }

    private void ChangeModel()
    {
        Console.WriteLine($"{CurrentPage.ToString()}");
        switch (CurrentPage)
        {
            case OperationsViewModel:
                CurrentPage = Locator.Current.GetService<TankDetailsViewModel>();
                Console.WriteLine("case Operations");
                break;
            case HomePageViewModel:
                CurrentPage = Locator.Current.GetService<OperationsViewModel>();
                Console.WriteLine("case HomePage");
                break;
            case TankDetailsViewModel:
                CurrentPage = Locator.Current.GetService<HomePageViewModel>();
                Console.WriteLine("case tank details");
                break;
            case null:
                Console.WriteLine("case null");
                CurrentPage = new HomePageViewModel();
                break;
            default:
                CurrentPage = Locator.Current.GetService<HomePageViewModel>();
                Console.WriteLine("case default");
                break;
        }
    }
}
