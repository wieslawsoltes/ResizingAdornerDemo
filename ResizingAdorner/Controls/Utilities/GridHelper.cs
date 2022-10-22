using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;

namespace ResizingAdorner.Controls.Utilities;

public static class GridHelper
{
    public static List<GridCell> GetCells(Grid grid)
    {
        var columnsCount = grid.ColumnDefinitions.Count;
        var rowsCount = grid.RowDefinitions.Count;
        var cells = new List<GridCell>();

        var columnOffset = 0d;

        for (var column = 0; column < columnsCount; column++)
        {
            var columnDefinition = grid.ColumnDefinitions[column];
            var rowOffset = 0d;

            for (var row = 0; row < rowsCount; row++)
            {
                var rowDefinition = grid.RowDefinitions[row];

                var cell = new GridCell
                {
                    Column = column,
                    Row = row,
                    ActualWidth = columnDefinition.ActualWidth,
                    ActualHeight = rowDefinition.ActualHeight,
                    ColumnOffset = columnOffset,
                    RowOffset = rowOffset,
                    Bounds = new Rect(
                        columnOffset,
                        rowOffset,
                        columnDefinition.ActualWidth,
                        rowDefinition.ActualHeight)
                };

                cells.Add(cell);

                rowOffset += rowDefinition.ActualHeight;
            }

            columnOffset += columnDefinition.ActualWidth;
        }

        return cells;
    }
}
