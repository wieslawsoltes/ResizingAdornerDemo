using System;
using Avalonia;
using Avalonia.Controls;
using ResizingAdorner.Controls.Model;
using ResizingAdorner.Controls.Utilities;
using ResizingAdorner.Defaults;

namespace ResizingAdorner.Editors;

public class CanvasEditor : IControlEditor
{
    public void Insert(Type type, Point point, object control)
    {
        if (control is not Canvas canvas)
        {
            return;
        }
        
        if (TypeFactory.CreateControl(type) is not { } child)
        {
            return;
        }

        DefaultsProvider.FixedPositionAndSize(child);

        Canvas.SetLeft(child, point.X);
        Canvas.SetTop(child, point.Y);

        canvas.Children.Add(child);
    }
}
