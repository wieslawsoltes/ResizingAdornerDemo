using Avalonia;
using Avalonia.Controls;

namespace ResizingAdorner.Controls.Model;

public interface IControlResizer
{
    bool EnableSnap { get; set; }
    double SnapX { get; set; }
    double SnapY { get; set; }
    void Start(Control control);
    void Move(Control control, Vector vector);
    void Left(Control control, Vector vector);
    void Right(Control control, Vector vector);
    void Top(Control control, Vector vector);
    void Bottom(Control control, Vector vector);
}
