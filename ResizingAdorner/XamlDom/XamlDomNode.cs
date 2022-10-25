using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using ResizingAdorner.Utilities;

namespace ResizingAdorner.XamlDom;

public class XamlDomNode
{
    private Control? _control;

    public Type? ControlType { get; set; }

    public Dictionary<string, object?>? Values { get; set; }

    public XamlDomNode? Child { get; set; }

    public List<XamlDomNode>? Children { get; set; }

    public bool CreateControl()
    {
        if (ControlType is null)
        {
            return false;
        }
        
        if (TypeHelper.CreateControl(ControlType) is { } control)
        {
            _control = control;
            return true;
        }

        return false;
    }

    /*
    public void SetValue(AvaloniaProperty property, object? value)
    {
        if (Values is null)
        {
            Values = new Dictionary<AvaloniaProperty, object?>();
        }

        Values[property] = value;

        if (_control is { })
        {
            _control.SetValue(property, value);
        }
    }

    public object? GetValue(AvaloniaProperty property)
    {
        if (Values is { })
        {
            if (Values.TryGetValue(property, out var value))
            {
                return value;
            }
        }

        return AvaloniaProperty.UnsetValue;
    }
    */
}
