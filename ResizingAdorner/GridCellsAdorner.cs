using System;
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
            var bounds = Grid.Bounds;
            var cells = GridHelper.GetCells(Grid);
            var pen = new Pen(new SolidColorBrush(Colors.Cyan));

            var column = -1;
            var row = -1;
            foreach (var cell in cells)
            {
                if (cell.Column > column && cell.Column > 0)
                {
                    column = cell.Column;
                    context.DrawLine(
                        pen, 
                        new Point(cell.ColumnOffset, 0.0), 
                        new Point(cell.ColumnOffset, bounds.Height));
                }

                if (cell.Row > row && cell.Row > 0)
                {
                    row = cell.Row;
                    context.DrawLine(
                        pen, 
                        new Point(0.0, cell.RowOffset), 
                        new Point(bounds.Width, cell.RowOffset));
                }

                // context.DrawRectangle(null, pen, cell.Bounds);
            }
        }
    }
}
