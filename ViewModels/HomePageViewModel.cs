using System;
using System.Windows.Input;


namespace VibeNine.ViewModels;

public class HomePageViewModel : PageViewModelBase
{
    public string Title => "Welcome to the Home Page View: ";

    public ICommand NavigateTankDetailsCommand { get; }

    public HomePageViewModel()
    {
        
    }

    public void NavigateTankDetails()
    {
        
    }

    // public override bool CanNavigateNext
    // {
    //     get => true;a
    //     protected set => throw new NotSupportedException();
    // }
    //
    // public override bool CanNavigatePrevious
    // {
    //     get => true;
    //     protected set => throw new NotSupportedException();
    // }
    //
}