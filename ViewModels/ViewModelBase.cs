using ReactiveUI;
using Splat;

namespace VibeNine.ViewModels;

public abstract class ViewModelBase : ReactiveObject, IRoutableViewModel
{
    public abstract string? UrlPathSegment { get; }
    public virtual IScreen HostScreen { get => Locator.Current.GetService<IScreen>()!; }
}
