using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ReactiveUI.Fody.Helpers;

namespace VibeOne.Models;

public class TankModel : INotifyPropertyChanged
{
    public int Id { get; init; }
    public string? Name { get; init; }
    [Reactive] public double Temperature { get; set; }
    public IEnumerable<TemperatureHistory>? TemperatureHistory { get; init; }
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}

public class TemperatureHistory
{
    public double Temperature { get; set; }
    public DateTime Time { get; set; }
}
