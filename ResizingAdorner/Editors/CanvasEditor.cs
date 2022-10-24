using System;
using Avalonia;
using Avalonia.Controls;
using ResizingAdorner.Controls.Model;
using ResizingAdorner.Controls.Utilities;

namespace ResizingAdorner.Editors;

public class CanvasEditor : IControlEditor
{
    public void Insert(Type type, Point point, object control, IControlDefaults? controlDefaults)
    {
        if (control is not Canvas canvas)
        {
            return;
        }
        
        if (TypeFactory.CreateControl(type) is not { } child)
        {
            return;
        }

        controlDefaults?.Fixed(child);

        Canvas.SetLeft(child, point.X);
        Canvas.SetTop(child, point.Y);

        canvas.Children.Add(child);
    }
}
