using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;

namespace VibeNine.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private bool _progressVisible = true;
    private bool _contentVisible = false;
    public bool ProgressVisible
    {
        get => _progressVisible;
        set => this.RaiseAndSetIfChanged(ref _progressVisible, value);
    }
    public bool ContentVisible
    {
        get => _contentVisible;
        set => this.RaiseAndSetIfChanged(ref _contentVisible, value);
    }

    private readonly PageViewModelBase[] _pages =
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
    public ICommand NavigateHomeCommand { get; }
    public ICommand NavigateOperationsCommand { get; }

    private int _progressPercent = 0;

    public int ProgressPercent
    {
        get => _progressPercent;
        set => this.RaiseAndSetIfChanged(ref _progressPercent, value);
    }

    public MainWindowViewModel()
    {
        _currentPage = _pages[0];
        //var canNavNext = this.WhenAnyValue(x => x.CurrentPage.)

        NavigateHomeCommand = ReactiveCommand.Create(NavigateHomeView);
        NavigateOperationsCommand = ReactiveCommand.Create(NavigateOperationsView);

        // TODO:  Temporary delay enforced to show the loading animation. 
        Task.Run(async () =>
        {
            for (int i = 0; i <= 100; i++)
            {
                ProgressPercent = i;
                await Task.Delay(100);
            }

            await Task.Delay(500);
            this.ProgressVisible = false;
            this.ContentVisible = true;
        });
    }


    private void NavigateHomeView()
    {
        if (_currentPage is HomePageViewModel) return;
        CurrentPage = _pages.First(p => p is HomePageViewModel);
    }

    private void NavigateOperationsView()
    {
        throw new NotImplementedException();
    }
}