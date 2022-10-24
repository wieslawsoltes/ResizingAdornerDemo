using System;
using Avalonia.Controls;

namespace ResizingAdorner.Utilities;

public static class TypeFactory
{
    public static Control? CreateControl(Type type)
    {
        return Activator.CreateInstance(type) as Control;
    }
}
