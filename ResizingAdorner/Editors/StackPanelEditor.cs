using System;
using Avalonia;
using Avalonia.Controls;
using ResizingAdorner.Defaults;

namespace ResizingAdorner.Editors;

public class StackPanelEditor
{
    public static void Insert(Type type, Point point, StackPanel stackPanel)
    {
        var obj = Activator.CreateInstance(type);
        if (obj is not Control control)
        {
            return;
        }

        DefaultsProvider.AutoPositionAndStretch(control);

        stackPanel.Children.Add(control);
    }
}
