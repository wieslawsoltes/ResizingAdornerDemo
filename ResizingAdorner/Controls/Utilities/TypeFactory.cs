using System;
using Avalonia.Controls;

namespace ResizingAdorner.Controls.Utilities;

public class TypeFactory
{
    public static Control? CreateControl(Type type)
    {
        return Activator.CreateInstance(type) as Control;
    }
}
