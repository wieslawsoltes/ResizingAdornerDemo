using System;
using Avalonia;
using Avalonia.Controls;
using ResizingAdorner.Controls.Model;

namespace ResizingAdorner.Controls.Resizers;

public class GridControlResizer : IControlResizer
{
    private Grid? _grid;
    private int _column;
    private int _row;
    private int _columnSpan;
    private int _rowSpan;

    public bool EnableSnap { get; set; }

    public double SnapX { get; set; }

    public double SnapY { get; set; }

    public void Start(Control control)
    {
        _grid = control.Parent as Grid;
        _column = Grid.GetColumn(control);
        _row = Grid.GetRow(control);
        _columnSpan = Grid.GetColumnSpan(control);
        _rowSpan = Grid.GetRowSpan(control);
    }

    public void Move(Control control, Point origin, Vector vector)
    {
        // TODO:
        Console.WriteLine($"[Move] bounds='{control.Bounds}', origin='{origin}', vector='{vector}'");
    }

    public void Left(Control control, Point origin, Vector vector)
    {
        // TODO:
        Console.WriteLine($"[Left] bounds='{control.Bounds}', origin='{origin}', vector='{vector}'");
    }

    public void Right(Control control, Point origin, Vector vector)
    {
        // TODO:
        Console.WriteLine($"[Right] bounds='{control.Bounds}', origin='{origin}', vector='{vector}'");
    }

    public void Top(Control control, Point origin, Vector vector)
    {
        // TODO:
        Console.WriteLine($"[Top] bounds='{control.Bounds}', origin='{origin}', vector='{vector}'");
    }

    public void Bottom(Control control, Point origin, Vector vector)
    {
        // TODO:
        Console.WriteLine($"[Bottom] bounds='{control.Bounds}', origin='{origin}', vector='{vector}'");
    }
}
