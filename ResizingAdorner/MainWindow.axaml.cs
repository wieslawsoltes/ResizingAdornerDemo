using System;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace ResizingAdorner;

public partial class MainWindow : Window
{
    private double _left;
    private double _top;
    private double _width;
    private double _height;
    private bool _updating;

    public MainWindow()
    {
        InitializeComponent();
    }

    // DragStarted Implementation

    private void DragStarted(object? sender, VectorEventArgs e)
    {
        if (sender is Thumb { Tag: Control control })
        {
            Console.WriteLine($"[Started] {control} {e.Vector}");
            _left = Canvas.GetLeft(control);
            _top = Canvas.GetTop(control);
            _width = control.Bounds.Width;
            _height = control.Bounds.Height;
            // control.Width = _width;
            // control.Height = _height;
        }
    }

    // DragDelta Implementation

    private void DragDeltaCenter(object? sender, VectorEventArgs e)
    {
        if (_updating)
        {
            return;
        }

        _updating = true;

        if (sender is Thumb { Tag: Control control })
        {
            Console.WriteLine($"[Center.Delta] {control} {e.Vector}");

            var left = _left + e.Vector.X;
            var top = _top + e.Vector.Y;

            Canvas.SetLeft(control, left);
            Canvas.SetTop(control, top);
        }

        _updating = false;
    }

    private void DragDeltaLeft(object? sender, VectorEventArgs e)
    {
        if (_updating)
        {
            return;
        }

        _updating = true;

        if (sender is Thumb { Tag: Control control })
        {
            Console.WriteLine($"[Left.Delta] {control} {e.Vector}");

            var left = _left + e.Vector.X;
            var width = _width - e.Vector.X;

            if (width >= 0)
            {
                Canvas.SetLeft(control, left);
                // TODO: Check for MinWidth
                control.Width = width;
            }
        }

        _updating = false;
    }

    private void DragDeltaRight(object? sender, VectorEventArgs e)
    {
        if (_updating)
        {
            return;
        }

        _updating = true;

        if (sender is Thumb { Tag: Control control })
        {
            Console.WriteLine($"[Right.Delta] {control} {e.Vector}");

            _width = control.Bounds.Width;

            var width = _width + e.Vector.X;

            if (width >= 0)
            {
                // TODO: Check for MinWidth
                control.Width = width;
            }
        }

        _updating = false;
    }

    private void DragDeltaTop(object? sender, VectorEventArgs e)
    {
        if (_updating)
        {
            return;
        }

        _updating = true;

        if (sender is Thumb { Tag: Control control })
        {
            Console.WriteLine($"[Top.Delta] {control} {e.Vector}");

            var top = _top + e.Vector.Y;
            var height = _height - e.Vector.Y;

            if (height >= 0)
            {
                Canvas.SetTop(control, top);
                // TODO: Check for MinHeight
                control.Height = height;
            }
        }

        _updating = false;
    }

    private void DragDeltaBottom(object? sender, VectorEventArgs e)
    {
        if (_updating)
        {
            return;
        }

        _updating = true;

        if (sender is Thumb { Tag: Control control })
        {
            Console.WriteLine($"[Bottom.Delta] {control} {e.Vector}");

            _height = control.Bounds.Height;

            var height = _height + e.Vector.Y;

            if (height >= 0)
            {
                // TODO: Check for MinHeight
                control.Height = height;
            }
        }

        _updating = false;
    }

    private void DragDeltaTopLeft(object? sender, VectorEventArgs e)
    {
        if (_updating)
        {
            return;
        }

        _updating = true;

        if (sender is Thumb { Tag: Control control })
        {
            Console.WriteLine($"[TopLeft.Delta] {control} {e.Vector}");

            var left = _left + e.Vector.X;
            var top = _top + e.Vector.Y;
            var width = _width - e.Vector.X;
            var height = _height - e.Vector.Y;

            if (width >= 0)
            {
                Canvas.SetLeft(control, left);
                // TODO: Check for MinWidth
                control.Width = width;
            }

            if (height >= 0)
            {
                Canvas.SetTop(control, top);
                // TODO: Check for MinHeight
                control.Height = height;
            }
        }

        _updating = false;
    }

    private void DragDeltaTopRight(object? sender, VectorEventArgs e)
    {
        if (_updating)
        {
            return;
        }

        _updating = true;

        if (sender is Thumb { Tag: Control control })
        {
            Console.WriteLine($"[TopRight.Delta] {control} {e.Vector}");

            _width = control.Bounds.Width;

            var top = _top + e.Vector.Y;
            var width = _width + e.Vector.X;
            var height = _height - e.Vector.Y;

            if (width >= 0)
            {
                // TODO: Check for MinWidth
                control.Width = width;
            }

            if (height >= 0)
            {
                Canvas.SetTop(control, top);
                // TODO: Check for MinHeight
                control.Height = height;
            }
        }

        _updating = false;
    }

    private void DragDeltaBottomLeft(object? sender, VectorEventArgs e)
    {
        if (_updating)
        {
            return;
        }

        _updating = true;

        if (sender is Thumb { Tag: Control control })
        {
            Console.WriteLine($"[BottomLeft.Delta] {control} {e.Vector}");

            _height = control.Bounds.Height;

            var left = _left + e.Vector.X;
            var width = _width - e.Vector.X;
            var height = _height + e.Vector.Y;

            if (width >= 0)
            {
                Canvas.SetLeft(control, left);
                // TODO: Check for MinWidth
                control.Width = width;
            }

            if (height >= 0)
            {
                // TODO: Check for MinHeight
                control.Height = height;
            }
        }

        _updating = false;
    }

    private void DragDeltaBottomRight(object? sender, VectorEventArgs e)
    {
        if (_updating)
        {
            return;
        }

        _updating = true;

        if (sender is Thumb { Tag: Control control })
        {
            Console.WriteLine($"[BottomRight.Delta] {control} {e.Vector}");

            _width = control.Bounds.Width;
            _height = control.Bounds.Height;

            var width = _width + e.Vector.X;
            var height = _height + e.Vector.Y;

            if (width >= 0)
            {
                // TODO: Check for MinWidth
                control.Width = width;
            }

            if (height >= 0)
            {
                // TODO: Check for MinHeight
                control.Height = height;
            }
        }

        _updating = false;
    }

    // DragStarted Event Handlers
    
    private void PART_ThumbCenter_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, e);
    }

    private void PART_ThumbLeft_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, e);
    }

    private void PART_ThumbRight_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, e);
    }

    private void PART_ThumbTop_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, e);
    }

    private void PART_ThumbBottom_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, e);
    }

    private void PART_ThumbTopLeft_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, e);
    }

    private void PART_ThumbTopRight_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, e);
    }

    private void PART_ThumbBottomLeft_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, e);
    }

    private void PART_ThumbBottomRight_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted(sender, e);
    }

    // DragDelta Event Handlers

    private void PART_ThumbCenter_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaCenter(sender, e);
    }

    private void PART_ThumbLeft_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaLeft(sender, e);
    }

    private void PART_ThumbRight_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaRight(sender, e);
    }

    private void PART_ThumbTop_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaTop(sender, e);
    }

    private void PART_ThumbBottom_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaBottom(sender, e);
    }

    private void PART_ThumbTopLeft_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaTopLeft(sender, e);
    }

    private void PART_ThumbTopRight_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaTopRight(sender, e);
    }

    private void PART_ThumbBottomLeft_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaBottomLeft(sender, e);
    }

    private void PART_ThumbBottomRight_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaBottomRight(sender, e);
    }
}
