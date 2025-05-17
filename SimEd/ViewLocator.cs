using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using CommunityToolkit.Mvvm.ComponentModel;
using Dock.Model.Core;
using SimEd.ViewModels;
using StaticViewLocator;

namespace SimEd;

[StaticViewLocator]
public partial class ViewLocator : IDataTemplate
{
    private readonly IServiceProvider _serviceProvider;

    public ViewLocator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Control? Build(object? data)
    {
        if (data is null)
        {
            return null;
        }

        var type = data.GetType();

        if (!s_views.TryGetValue(type, out var func))
        {
            throw new Exception($"Unable to create view for type: {type}");
        }

        Control resultControl = func.Invoke();
        resultControl.DataContext = data;
        if (data is IViewAware viewAware)
        {
            viewAware.SetControl(resultControl);
        }

        return resultControl;
    }

    public bool Match(object? data)
    {
        return data is ObservableObject || data is IDockable;
    }
}