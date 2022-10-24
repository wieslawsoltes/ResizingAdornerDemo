﻿using System;
using Avalonia;
using Avalonia.Controls;
using ResizingAdorner.Controls.Model;
using ResizingAdorner.Controls.Utilities;
using ResizingAdorner.Defaults;

namespace ResizingAdorner.Editors;

public class GridEditor : IControlEditor
{
    public void Insert(Type type, Point point, object control)
    {
        if (control is not Grid grid)
        {
            return;
        }

        if (TypeFactory.CreateControl(type) is not { } child)
        {
            return;
        }

        DefaultsProvider.AutoPositionAndStretch(child);

        var cells = GridHelper.GetCells(grid);

        foreach (var cell in cells)
        {
            if (cell.Bounds.Contains(point))
            {
                Grid.SetColumn(child, cell.Column);
                Grid.SetRow(child, cell.Row);
            }
        }

        grid.Children.Add(child);
    }
}
