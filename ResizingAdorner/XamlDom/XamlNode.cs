using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Metadata;
using ResizingAdorner.Utilities;

namespace ResizingAdorner.XamlDom;

public class XamlNode
{
    private Control? _control;

    public Type? ControlType { get; set; }

    public Dictionary<XamlProperty, object?>? Values { get; set; }

    public XamlNode? Child { get; set; }

    public List<XamlNode>? Children { get; set; }

    public Control? Control => _control;

    public bool CreateControl()
    {
        if (ControlType is null)
        {
            return false;
        }
        
        if (TypeHelper.CreateControl(ControlType) is { } control)
        {
            _control = control;

            var contentProperty = _control
                .GetType()
                .GetProperties()
                .FirstOrDefault(x => x.IsDefined(typeof(ContentAttribute), false));

            var addMethod = contentProperty?.PropertyType.GetMethod("Add");

            if (Child is { })
            {
                if (!Child.CreateControl())
                {
                    return false;
                }

                if (contentProperty is { })
                {
                    contentProperty.SetValue(_control, Child.Control);
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

                    if (contentProperty is { } && addMethod is { } && child.Control is { })
                    {
                        var content = contentProperty.GetValue(_control);
                        if (content is { })
                        {
                            addMethod.Invoke(content, new object[] { child.Control });
                        }
                    }
                }
            }

            return true;
        }

        return false;
    }

    public void SetValue(XamlProperty property, object? value)
    {
        if (Values is null)
        {
            Values = new Dictionary<XamlProperty, object?>();
        }

        Values[property] = value;

        if (_control is { })
        {
            property.SetValue(_control, value);
        }
    }

    public object? GetValue(XamlProperty property)
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

                var name = kvp.Key.Name;
                if (kvp.Key.AvaloniaProperty is {IsAttached: true})
                {
                    name = kvp.Key.AvaloniaProperty.OwnerType.Name + "." + name;
                }
                
                sb.Append(' ');
                sb.Append(name);
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
