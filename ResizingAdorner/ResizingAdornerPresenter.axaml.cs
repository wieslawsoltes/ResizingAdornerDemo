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

    public static readonly StyledProperty<bool> ShowThumbsProperty = 
        AvaloniaProperty.Register<ResizingAdornerPresenter, bool>(nameof(ShowThumbs));

    private readonly IControlResizer _controlResizer = new CanvasControlResizer();
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

    public bool ShowThumbs
    {
        get => GetValue(ShowThumbsProperty);
        set => SetValue(ShowThumbsProperty, value);
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        if (AdornedControl is { })
        {
            Editor.s_adorners.Add(this);
        }
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        
        if (AdornedControl is { })
        {
            Editor.s_adorners.Remove(this);
        }
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

    private void DragStarted()
    {
        if (AdornedControl is { } control)
        {
            _controlResizer.Start(control);
        }
    }

    private void DragDeltaCenter(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            _controlResizer.Move(control, e.Vector);
            _updating = false;
        }
    }

    private void DragDeltaLeft(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            _controlResizer.Left(control, e.Vector);
            _updating = false;
        }
    }

    private void DragDeltaRight(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            _controlResizer.Right(control, e.Vector);
            _updating = false;
        }
    }

    private void DragDeltaTop(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            _controlResizer.Top(control, e.Vector);
            _updating = false;
        }
    }

    private void DragDeltaBottom(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            _controlResizer.Bottom(control, e.Vector);
            _updating = false;
        }
    }

    private void DragDeltaTopLeft(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            _controlResizer.Left(control, e.Vector);
            _controlResizer.Top(control, e.Vector);
            _updating = false;
        }
    }

    private void DragDeltaTopRight(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            _controlResizer.Right(control, e.Vector);
            _controlResizer.Top(control, e.Vector);
            _updating = false;
        }
    }

    private void DragDeltaBottomLeft(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            _controlResizer.Left(control, e.Vector);
            _controlResizer.Bottom(control, e.Vector);
            _updating = false;
        }
    }

    private void DragDeltaBottomRight(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            _controlResizer.Right(control, e.Vector);
            _controlResizer.Bottom(control, e.Vector);
            _updating = false;
        }
    }

    private void PART_ThumbCenter_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted();
    }

    private void PART_ThumbLeft_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted();
    }

    private void PART_ThumbRight_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted();
    }

    private void PART_ThumbTop_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted();
    }

    private void PART_ThumbBottom_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted();
    }

    private void PART_ThumbTopLeft_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted();
    }

    private void PART_ThumbTopRight_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted();
    }

    private void PART_ThumbBottomLeft_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted();
    }

    private void PART_ThumbBottomRight_OnDragStarted(object? sender, VectorEventArgs e)
    {
        DragStarted();
    }

    private void PART_ThumbCenter_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaCenter(e);
    }

    private void PART_ThumbLeft_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaLeft(e);
    }

    private void PART_ThumbRight_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaRight(e);
    }

    private void PART_ThumbTop_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaTop(e);
    }

    private void PART_ThumbBottom_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaBottom(e);
    }

    private void PART_ThumbTopLeft_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaTopLeft(e);
    }

    private void PART_ThumbTopRight_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaTopRight(e);
    }

    private void PART_ThumbBottomLeft_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaBottomLeft(e);
    }

    private void PART_ThumbBottomRight_OnDragDelta(object? sender, VectorEventArgs e)
    {
        DragDeltaBottomRight(e);
    }
}

