using Avalonia;
using Avalonia.Controls;
using ResizingAdorner.Controls.Model;

namespace ResizingAdorner.Controls.Resizers;

public class GridControlResizer : IControlResizer
{
    public bool EnableSnap { get; set; }

    public double SnapX { get; set; }

    public double SnapY { get; set; }

    public void Start(Control control)
    {
        // TODO:
    }

    public void Move(Control control, Vector vector)
    {
        // TODO:
    }

    public void Left(Control control, Vector vector)
    {
        // TODO:
    }

    public void Right(Control control, Vector vector)
    {
        // TODO:
    }

    public void Top(Control control, Vector vector)
    {
        // TODO:
    }

    public void Bottom(Control control, Vector vector)
    {
        // TODO:
    }
}
