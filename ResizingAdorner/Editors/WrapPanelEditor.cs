using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using ResizingAdorner.Defaults;

namespace ResizingAdorner.Editors;

public class WrapPanelEditor
{
    public static void Insert(Type type, Point point, WrapPanel wrapPanel)
    {
        var obj = Activator.CreateInstance(type);
        if (obj is not Control control)
        {
            return;
        }

        DefaultsProvider.FixedPositionAndSize(control);

        wrapPanel.Children.Add(control);
    }
}
