using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using ResizingAdorner.Controls.Model;

namespace ResizingAdorner.Controls;

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

    public static readonly StyledProperty<IControlResizer?> ControlResizerProperty = 
        AvaloniaProperty.Register<ResizingAdornerPresenter, IControlResizer?>(nameof(ControlResizer));

    public static readonly StyledProperty<IControlSelection?> ControlSelectionProperty = 
        AvaloniaProperty.Register<ResizingAdornerPresenter, IControlSelection?>(nameof(ControlSelection));

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
    private Point _startPoint;

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

    public IControlResizer? ControlResizer
    {
        get => GetValue(ControlResizerProperty);
        set => SetValue(ControlResizerProperty, value);
    }

    public IControlSelection? ControlSelection
    {
        get => GetValue(ControlSelectionProperty);
        set => SetValue(ControlSelectionProperty, value);
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        if (AdornedControl is { })
        {
            ControlSelection?.Register(this);
        }
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        
        if (AdornedControl is { })
        {
            ControlSelection?.Unregister(this);
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
            _thumbCenter.DragStarted += Thumb_OnDragStarted;
            _thumbCenter.DragDelta += PART_ThumbCenter_OnDragDelta;
            _thumbCenter.AddHandler(InputElement.PointerPressedEvent, Thumb_OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        }

        if (_thumbLeft is { })
        {
            _thumbLeft.DragStarted += Thumb_OnDragStarted;
            _thumbLeft.DragDelta += PART_ThumbLeft_OnDragDelta;
            _thumbLeft.AddHandler(InputElement.PointerPressedEvent, Thumb_OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        }

        if (_thumbRight is { })
        {
            _thumbRight.DragStarted += Thumb_OnDragStarted;
            _thumbRight.DragDelta += PART_ThumbRight_OnDragDelta;
            _thumbRight.AddHandler(InputElement.PointerPressedEvent, Thumb_OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        }

        if (_thumbTop is { })
        {
            _thumbTop.DragStarted += Thumb_OnDragStarted;
            _thumbTop.DragDelta += PART_ThumbTop_OnDragDelta;
            _thumbTop.AddHandler(InputElement.PointerPressedEvent, Thumb_OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        }

        if (_thumbBottom is { })
        {
            _thumbBottom.DragStarted += Thumb_OnDragStarted;
            _thumbBottom.DragDelta += PART_ThumbBottom_OnDragDelta;
            _thumbBottom.AddHandler(InputElement.PointerPressedEvent, Thumb_OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        }

        if (_thumbTopLeft is { })
        {
            _thumbTopLeft.DragStarted += Thumb_OnDragStarted;
            _thumbTopLeft.DragDelta += PART_ThumbTopLeft_OnDragDelta;
            _thumbTopLeft.AddHandler(InputElement.PointerPressedEvent, Thumb_OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        }

        if (_thumbTopRight is { })
        {
            _thumbTopRight.DragStarted += Thumb_OnDragStarted;
            _thumbTopRight.DragDelta += PART_ThumbTopRight_OnDragDelta;
            _thumbTopRight.AddHandler(InputElement.PointerPressedEvent, Thumb_OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        }

        if (_thumbBottomLeft is { })
        {
            _thumbBottomLeft.DragStarted += Thumb_OnDragStarted;
            _thumbBottomLeft.DragDelta += PART_ThumbBottomLeft_OnDragDelta;
            _thumbBottomLeft.AddHandler(InputElement.PointerPressedEvent, Thumb_OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        }

        if (_thumbBottomRight is { })
        {
            _thumbBottomRight.DragStarted += Thumb_OnDragStarted;
            _thumbBottomRight.DragDelta += PART_ThumbBottomRight_OnDragDelta;
            _thumbBottomRight.AddHandler(InputElement.PointerPressedEvent, Thumb_OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        }
    }

    private void DragStarted()
    {
        if (AdornedControl is { } control)
        {
            ControlResizer?.Start(control);
        }
    }

    private void DragDeltaCenter(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            ControlResizer?.Move(control, _startPoint, e.Vector);
            _updating = false;
        }
    }

    private void DragDeltaLeft(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            ControlResizer?.Left(control, _startPoint, e.Vector);
            _updating = false;
        }
    }

    private void DragDeltaRight(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            ControlResizer?.Right(control, _startPoint, e.Vector);
            _updating = false;
        }
    }

    private void DragDeltaTop(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            ControlResizer?.Top(control, _startPoint, e.Vector);
            _updating = false;
        }
    }

    private void DragDeltaBottom(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            ControlResizer?.Bottom(control, _startPoint, e.Vector);
            _updating = false;
        }
    }

    private void DragDeltaTopLeft(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            ControlResizer?.Left(control, _startPoint, e.Vector);
            ControlResizer?.Top(control, _startPoint, e.Vector);
            _updating = false;
        }
    }

    private void DragDeltaTopRight(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            ControlResizer?.Right(control, _startPoint, e.Vector);
            ControlResizer?.Top(control, _startPoint, e.Vector);
            _updating = false;
        }
    }

    private void DragDeltaBottomLeft(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            ControlResizer?.Left(control, _startPoint, e.Vector);
            ControlResizer?.Bottom(control, _startPoint, e.Vector);
            _updating = false;
        }
    }

    private void DragDeltaBottomRight(VectorEventArgs e)
    {
        if (!_updating && AdornedControl is { } control)
        {
            _updating = true;
            ControlResizer?.Right(control, _startPoint, e.Vector);
            ControlResizer?.Bottom(control, _startPoint, e.Vector);
            _updating = false;
        }
    }

    private void Thumb_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (AdornedControl is { } control)
        {
            _startPoint = e.GetPosition(control);
        }
    }

    private void Thumb_OnDragStarted(object? sender, VectorEventArgs e)
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

