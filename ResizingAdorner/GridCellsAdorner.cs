using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

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
            var columnsCount = Grid.ColumnDefinitions.Count;
            var rowsCount = Grid.RowDefinitions.Count;
            var cells = new GridCell[columnsCount, rowsCount];

            var columnOffset = 0d;

            for (var column = 0; column < Grid.ColumnDefinitions.Count; column++)
            {
                var columnDefinition = Grid.ColumnDefinitions[column];
                var rowOffset = 0d;

                for (var row = 0; row < Grid.RowDefinitions.Count; row++)
                {
                    var rowDefinition = Grid.RowDefinitions[row];

                    cells[column, row] = new GridCell()
                    {
                        Column = column,
                        Row = row,
                        ActualWidth = columnDefinition.ActualWidth,
                        ActualHeight = rowDefinition.ActualHeight,
                        Bounds = new Rect(
                            columnOffset, 
                            rowOffset,
                            columnDefinition.ActualWidth,
                            rowDefinition.ActualHeight)
                    };

                    rowOffset += rowDefinition.ActualHeight;
                }

                columnOffset += columnDefinition.ActualWidth;
            }

            for (var column = 0; column < Grid.ColumnDefinitions.Count; column++)
            {
                for (var row = 0; row < Grid.RowDefinitions.Count; row++)
                {
                    context.DrawRectangle(
                        null,
                        new Pen(new SolidColorBrush(Colors.Cyan)), 
                        cells[column, row].Bounds);
                }
            }
        }
    }
}
