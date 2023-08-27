using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using ReactiveUI;
using VibeOne.ViewModels;
using VibeOne.Views;

namespace VibeOne;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        if (data is null) return new TextBlock { Text = "Data was null" };
        var name = data.GetType().FullName!.Replace("ViewModel", "View");
        var type = Type.GetType(name);
        
        return (type != null)
            ? (Control)Activator.CreateInstance(type)!
            : new TextBlock() { Text = "View was not found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}
