using System;
using Avalonia;
using Avalonia.Controls;
using ResizingAdorner.Defaults;

namespace ResizingAdorner.Editors;

public class CanvasEditor
{
    public static void Insert(Type type, Point point, Canvas canvas)
    {
        var obj = Activator.CreateInstance(type);
        if (obj is not Control control)
        {
            return;
        }

        DefaultsProvider.FixedPositionAndSize(control);

        Canvas.SetLeft(control, point.X);
        Canvas.SetTop(control, point.Y);

        canvas.Children.Add(control);
    }
}
