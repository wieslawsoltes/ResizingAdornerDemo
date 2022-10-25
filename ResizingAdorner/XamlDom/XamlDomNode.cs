using System;
using System.Collections.Generic;
using System.Text;
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

            if (Child is { })
            {
                if (!Child.CreateControl())
                {
                    return false;
                }
            }

            if (Children is { })
            {
                foreach (var child in Children)
                {
                    if (!child.CreateControl())
                    {
                        return false;
                    }
                }
            }

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

    public void Write(StringBuilder sb, int indentLevel)
    {
        if (ControlType is null)
        {
            return;
        }

        var hasContent = false;

        if (indentLevel > 0)
        {
            sb.Append(new string(' ', indentLevel * 2));
        }
        
        sb.Append('<');
        sb.Append(ControlType.Name);

        if (Values is { })
        {
            foreach (var kvp in Values)
            {
                if (kvp.Value is null)
                {
                    continue;
                }
                sb.Append(' ');
                sb.Append(kvp.Key);
                sb.Append('=');
                sb.Append('"');
                sb.Append(kvp.Value);
                sb.Append('"');
            }
        }

        if (Child is { })
        {
            sb.Append('>');
            sb.AppendLine();

            var childLevel = indentLevel + 1;

            Child.Write(sb, childLevel);

            hasContent = true;
        }

        if (Children is {Count: > 0})
        {
            sb.Append('>');
            sb.AppendLine();

            var childrenLevel = indentLevel + 1;

            foreach (var child in Children)
            {
                child.Write(sb, childrenLevel);
            }

            hasContent = true;
        }

        if (hasContent)
        {
            sb.Append(new string(' ', indentLevel * 2));
            sb.Append('<');
            sb.Append('/');
            sb.Append(ControlType.Name);
            sb.Append('>');
            sb.AppendLine();
        }
        else
        {
            sb.Append(' ');
            sb.Append('/');
            sb.Append('>');
            sb.AppendLine();
        }
    }
}
