using System.Windows.Input;
using DynamicData;
using ReactiveUI;

namespace VibeNine.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly PageViewModelBase[] Pages =
    {
        new HomePageViewModel(),
        new TankDetailsViewModel()
    };

    private PageViewModelBase _currentPage;

    public PageViewModelBase CurrentPage
    {
        get => _currentPage;
        private set => this.RaiseAndSetIfChanged(ref _currentPage, value);
    }

    public ICommand NavigateNextCommand { get; }
    public ICommand NavigatePreviousCommand { get; }

    public MainWindowViewModel()
    {
        _currentPage = Pages[0];
        //var canNavNext = this.WhenAnyValue(x => x.CurrentPage.)

        NavigateNextCommand = ReactiveCommand.Create(NavigateNext);
        NavigatePreviousCommand = ReactiveCommand.Create(NavigatePrevious);
    }

    private void NavigateNext()
    {
        // get the current index and add one.  This wont work for specific menu items, where we will get the name of the link and instanciate the viewmodel
        var index = Pages.IndexOf(CurrentPage) + 1;
        CurrentPage = Pages[index];
    }

    private void NavigatePrevious()
    {
        // get the current index and subtract 1
        var index = Pages.IndexOf(CurrentPage) - 1;

        //  /!\ Be aware that we have no check if the index is valid. You may want to add it on your own. /!\
        CurrentPage = Pages[index];
    }
}