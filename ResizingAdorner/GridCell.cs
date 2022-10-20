using Avalonia;

namespace ResizingAdorner;

public class GridCell
{
    public int Column { get; set; }
    public int Row { get; set; }
    public double ActualWidth { get; set; }
    public double ActualHeight { get; set; }
    public Rect Bounds { get; set; }
}
