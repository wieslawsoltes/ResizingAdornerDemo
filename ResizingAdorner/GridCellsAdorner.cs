using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using ResizingAdorner.Controls.Utilities;

namespace ResizingAdorner;

public class GridCellsAdorner : Control
{
    public static readonly StyledProperty<Grid?> GridProperty = 
        AvaloniaProperty.Register<GridCellsAdorner, Grid?>(nameof(Grid));

    public Grid? Grid
    {
        get => GetValue(GridProperty);
        set => SetValue(GridProperty, value);
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);

        if (Grid is { })
        {
            var cells = GridHelper.GetCells(Grid);
            var pen = new Pen(new SolidColorBrush(Colors.Cyan));

            foreach (var cell in cells)
            {
                context.DrawRectangle(null, pen, cell.Bounds);
            }
        }
    }
}
