using System;
using Avalonia;
using Avalonia.Controls;
using ResizingAdorner.Controls.Utilities;
using ResizingAdorner.Defaults;

namespace ResizingAdorner.Editors;

public class GridEditor
{
    public static void Insert(Type type, Point point, Grid grid)
    {
        var obj = Activator.CreateInstance(type);
        if (obj is not Control control)
        {
            return;
        }

        DefaultsProvider.AutoPositionAndStretch(control);

        var cells = GridHelper.GetCells(grid);

        foreach (var cell in cells)
        {
            if (cell.Bounds.Contains(point))
            {
                Grid.SetColumn(control, cell.Column);
                Grid.SetRow(control, cell.Row);
            }
        }

        grid.Children.Add(control);
    }
}
