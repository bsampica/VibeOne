using System;
using ReactiveUI;

namespace VibeNine.ViewModels;

public class TankDetailsViewModel : PageViewModelBase
{
    public string Title
    {
        get => "Welcome to the Tank Details page.";
    }


    private bool _value1 = false;
    private bool _value2 = false;

// public override bool CanNavigateNext
// {
//     get => _value1;
//     protected set => this.RaiseAndSetIfChanged(ref _value1, value);
// }
// public override bool CanNavigatePrevious
// {
//     get => _value2;
//     protected set => this.RaiseAndSetIfChanged(ref _value2, value);
// }

    public TankDetailsViewModel()
    {
        // Listen to changes on on the page to perform some type of validation if necessary for navigation
        // this.WhenAnyValue(x => x.MailAddress, x => x.Password)
        // .Subscribe(_ => UpdateCanNavigateNext());
    }
}