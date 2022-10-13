using Avalonia;
using Avalonia.Controls;

namespace ResizingAdorner;

public class CanvasControlResizer : IControlResizer
{
    private double _left;
    private double _top;
    private double _width;
    private double _height;

    public void Start(Control control)
    {
        _left = Canvas.GetLeft(control);
        _top = Canvas.GetTop(control);
        _width = control.Bounds.Width;
        _height = control.Bounds.Height;
    }

    public void Move(Control control, Vector vector)
    {
        var left = _left + vector.X;
        Canvas.SetLeft(control, left);

        var top = _top + vector.Y;
        Canvas.SetTop(control, top);
    }

    public void Left(Control control, Vector vector)
    {
        var left = _left + vector.X;
        var width = _width - vector.X;
        if (width >= 0)
        {
            Canvas.SetLeft(control, left);
            // TODO: Check for MinWidth
            control.Width = width;
        }
    }

    public void Right(Control control, Vector vector)
    {
        _width = control.Bounds.Width;
        var width = _width + vector.X;
        if (width >= 0)
        {
            // TODO: Check for MinWidth
            control.Width = width;
        }
    }

    public void Top(Control control, Vector vector)
    {
        var top = _top + vector.Y;
        var height = _height - vector.Y;
        if (height >= 0)
        {
            Canvas.SetTop(control, top);
            // TODO: Check for MinHeight
            control.Height = height;
        }
    }

    public void Bottom(Control control, Vector vector)
    {
        _height = control.Bounds.Height;
        var height = _height + vector.Y;
        if (height >= 0)
        {
            // TODO: Check for MinHeight
            control.Height = height;
        }
    }
}
