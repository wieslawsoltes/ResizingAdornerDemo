using Avalonia;
using Avalonia.Controls;
using ResizingAdorner.Controls.Model;

namespace ResizingAdorner.Controls.Resizers;

public class DockPanelControlResizer : IControlResizer
{
    private DockPanel? _dockPanel;

    public bool EnableSnap { get; set; }

    public double SnapX { get; set; }

    public double SnapY { get; set; }

    public void Start(Control control)
    {
        _dockPanel = control.Parent as DockPanel;
    }

    public void Move(Control control, Point origin, Vector vector)
    {
        // TODO:
    }

    public void Left(Control control, Point origin, Vector vector)
    {
        // TODO:
    }

    public void Right(Control control, Point origin, Vector vector)
    {
        // TODO:
    }

    public void Top(Control control, Point origin, Vector vector)
    {
        // TODO:
    }

    public void Bottom(Control control, Point origin, Vector vector)
    {
        // TODO:
    }
}
