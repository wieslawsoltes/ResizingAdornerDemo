using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;

namespace ResizingAdorner;

[TemplatePart("PART_ThumbCenter", typeof(Thumb))]
[TemplatePart("PART_ThumbLeft", typeof(Thumb))]
[TemplatePart("PART_ThumbRight", typeof(Thumb))]
[TemplatePart("PART_ThumbTop", typeof(Thumb))]
[TemplatePart("PART_ThumbBottom", typeof(Thumb))]
[TemplatePart("PART_ThumbTopLeft", typeof(Thumb))]
[TemplatePart("PART_ThumbTopRight", typeof(Thumb))]
[TemplatePart("PART_ThumbBottomLeft", typeof(Thumb))]
[TemplatePart("PART_ThumbBottomRight", typeof(Thumb))]
public class ResizingAdornerPresenter : TemplatedControl
{
    public static readonly StyledProperty<Control?> AdornedControlProperty = 
        AvaloniaProperty.Register<ResizingAdornerPresenter, Control?>(nameof(AdornedControl));

    public static readonly StyledProperty<double> AdornedWidthProperty = 
        AvaloniaProperty.Register<ResizingAdornerPresenter, double>(nameof(AdornedWidth));

    public static readonly StyledProperty<double> AdornedHeightProperty = 
        AvaloniaProperty.Register<ResizingAdornerPresenter, double>(nameof(AdornedHeight));

    private double _left;
    private double _top;
    private double _width;
    private double _height;
    private bool _updating;
    private Thumb? _thumbCenter;
    private Thumb? _thumbLeft;
    private Thumb? _thumbRight;
    private Thumb? _thumbTop;
    private Thumb? _thumbBottom;
    private Thumb? _thumbTopLeft;
    private Thumb? _thumbTopRight;
    private Thumb? _thumbBottomLeft;
    private Thumb? _thumbBottomRight;

    public Control? AdornedControl
    {
        get => GetValue(AdornedControlProperty);
        set => SetValue(AdornedControlProperty, value);
    }

    public double AdornedWidth
    {
        get => GetValue(AdornedWidthProperty);
        set => SetValue(AdornedWidthProperty, value);
    }

    public double AdornedHeight
    {
        get => GetValue(AdornedHeightProperty);
        set => SetValue(AdornedHeightProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        _thumbCenter = e.NameScope.Find<Thumb>("PART_ThumbCenter");
        _thumbLeft = e.NameScope.Find<Thumb>("PART_ThumbLeft");
        _thumbRight = e.NameScope.Find<Thumb>("PART_ThumbRight");
        _thumbTop = e.NameScope.Find<Thumb>("PART_ThumbTop");
        _thumbBottom = e.NameScope.Find<Thumb>("PART_ThumbBottom");
        _thumbTopLeft = e.NameScope.Find<Thumb>("PART_ThumbTopLeft");
        _thumbTopRight = e.NameScope.Find<Thumb>("PART_ThumbTopRight");
        _thumbBottomLeft = e.NameScope.Find<Thumb>("PART_ThumbBottomLeft");
        _thumbBottomRight = e.NameScope.Find<Thumb>("PART_ThumbBottomRight");

        if (_thumbCenter is { })
        {
            _thumbCenter.DragStarted += PART_ThumbCenter_OnDragStarted;
            _thumbCenter.DragDelta += PART_ThumbCenter_OnDragDelta;
        }

        if (_thumbLeft is { })
        {
            _thumbLeft.DragStarted += PART_ThumbLeft_OnDragStarted;
            _thumbLeft.DragDelta += PART_ThumbLeft_OnDragDelta;
        }

        if (_thumbRight is { })
        {
            _thumbRight.DragStarted += PART_ThumbRight_OnDragStarted;
            _thumbRight.DragDelta += PART_ThumbRight_OnDragDelta;
        }

        if (_thumbTop is { })
        {
            _thumbTop.DragStarted += PART_ThumbTop_OnDragStarted;
            _thumbTop.DragDelta += PART_ThumbTop_OnDragDelta;
        }

        if (_thumbBottom is { })
        {
            _thumbBottom.DragStarted += PART_ThumbBottom_OnDragStarted;
            _thumbBottom.DragDelta += PART_ThumbBottom_OnDragDelta;
        }

        if (_thumbTopLeft is { })
        {
            _thumbTopLeft.DragStarted += PART_ThumbTopLeft_OnDragStarted;
            _thumbTopLeft.DragDelta += PART_ThumbTopLeft_OnDragDelta;
        }

        if (_thumbTopRight is { })
        {
            _thumbTopRight.DragStarted += PART_ThumbTopRight_OnDragStarted;
            _thumbTopRight.DragDelta += PART_ThumbTopRight_OnDragDelta;
        }

        if (_thumbBottomLeft is { })
        {
            _thumbBottomLeft.DragStarted += PART_ThumbBottomLeft_OnDragStarted;
            _thumbBottomLeft.DragDelta += PART_ThumbBottomLeft_OnDragDelta;
        }

        if (_thumbBottomRight is { })
        {
            _thumbBottomRight.DragStarted += PART_ThumbBottomRight_OnDragStarted;
            _thumbBottomRight.DragDelta += PART_ThumbBottomRight_OnDragDelta;
        }
    }

    // DragStarted Implementation

    private void DragStarted(object? sender, VectorEventArgs e)
    {
        if (AdornedControl is { } control)
        {
            // Console.WriteLine($"[Started] {control} {e.Vector}");
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

        if (AdornedControl is { } control)
        {
            // Console.WriteLine($"[Center.Delta] {control} {e.Vector}");

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

        if (AdornedControl is { } control)
        {
            // Console.WriteLine($"[Left.Delta] {control} {e.Vector}");

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

        if (AdornedControl is { } control)
        {
            // Console.WriteLine($"[Right.Delta] {control} {e.Vector}");

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

        if (AdornedControl is { } control)
        {
            // Console.WriteLine($"[Top.Delta] {control} {e.Vector}");

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

        if (AdornedControl is { } control)
        {
            // Console.WriteLine($"[Bottom.Delta] {control} {e.Vector}");

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

        if (AdornedControl is { } control)
        {
            // Console.WriteLine($"[TopLeft.Delta] {control} {e.Vector}");

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

        if (AdornedControl is { } control)
        {
            // Console.WriteLine($"[TopRight.Delta] {control} {e.Vector}");

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

        if (AdornedControl is { } control)
        {
            // Console.WriteLine($"[BottomLeft.Delta] {control} {e.Vector}");

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

        if (AdornedControl is { } control)
        {
            // Console.WriteLine($"[BottomRight.Delta] {control} {e.Vector}");

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

