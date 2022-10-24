using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using ResizingAdorner.Controls;
using ResizingAdorner.Controls.Model;
using ResizingAdorner.Controls.Utilities;
using ResizingAdorner.Editors;

namespace ResizingAdorner;

public partial class ToolboxView : UserControl
{
    private bool _isPressed;
    private bool _isDragging;
    private Point _start;
    private ListBoxItem? _dragItem;

    public ToolboxView()
    {
        InitializeComponent();

        InitializeToolbox();
    }

    private void InitializeToolbox()
    {
        ControlTypes.AddHandler(InputElement.PointerPressedEvent, ToolBox_PointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        ControlTypes.AddHandler(InputElement.PointerReleasedEvent, ToolBox_PointerReleased, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        ControlTypes.AddHandler(InputElement.PointerMovedEvent, ToolBox_PointerMoved, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);

        ControlTypes.Items = GetControlTypes();
    }

    private List<Type> GetControlTypes()
    {
        var controlType = typeof(Control);
        var topLevelType = typeof(TopLevel);
        var controlsAssembly = controlType.Assembly;
        var controlTypes = new List<Type>();

        foreach (var t in controlsAssembly.GetTypes())
        {
            if (!t.IsAbstract
                && t.IsPublic
                && t.IsClass
                && !t.ContainsGenericParameters
                && t.GetConstructors().Any(x => x.GetParameters().Length == 0))
            {
                if (HasBaseType(t, controlType) && !HasBaseType(t, topLevelType))
                {
                    controlTypes.Add(t);
                }
            }
        }

        controlTypes.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));

        return controlTypes;
    }

    private bool HasBaseType(Type t, Type baseType)
    {
        var b = t.BaseType;
        while (b != null)
        {
            if (b == baseType)
            {
                return true;
            }

            b = b.BaseType;
        }

        return false;
    }

    private Control FinDropControl(Control control)
    {
        var adorner = HitTestHelper.FindControl<ResizingAdornerPresenter>(control);
        if (adorner is { AdornedControl: { } })
        {
            if (adorner.AdornedControl is ResizingAdornerPresenter)
            {
                return FinDropControl(adorner.AdornedControl);
            }
            control = adorner.AdornedControl;
        }

        return control;
    }

    private void ToolBox_PointerMoved(object? sender, PointerEventArgs e)
    {
        if (_isPressed)
        {
            if (!_isDragging)
            {
                var point = e.GetPosition(ControlTypes);
                var deltaX = Math.Abs((_start - point).X);
                var deltaY = Math.Abs((_start - point).Y);
                if (deltaX > 3d || deltaY > 3d)
                {
                    _isDragging = true;
                    Console.WriteLine(_isDragging);
                }
            }
            else
            {
                var inputElement = this.InputHitTest(e.GetPosition(ControlTypes));
                Console.WriteLine(inputElement);
                // TODO: Move/add preview
            }
        }
    }

    private Dictionary<Type, IControlEditor> _controlEditors = new()
    {
        [typeof(Border)] = new BorderEditor(),
        [typeof(Button)] = new ButtonEditor(),
        [typeof(Canvas)] = new CanvasEditor(),
        [typeof(ContentControl)] = new ContentControlEditor(),
        [typeof(Decorator)] = new DecoratorEditor(),
        [typeof(DockPanel)] = new DockPanelEditor(),
        [typeof(Grid)] = new GridEditor(),
        [typeof(Label)] = new LabelEditor(),
        [typeof(Panel)] = new PanelEditor(),
        [typeof(ScrollViewer)] = new ScrollViewerEditor(),
        [typeof(StackPanel)] = new StackPanelEditor(),
        [typeof(Viewbox)] = new ViewboxEditor(),
        [typeof(WrapPanel)] = new WrapPanelEditor(),
    };

    private void ToolBox_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (_isDragging && _dragItem is { } && _dragItem.DataContext is Type type)
        {
            var inputElement = this.InputHitTest(e.GetPosition(ControlTypes));
            Console.WriteLine(inputElement);

            if (inputElement is Control control)
            {
                control = FinDropControl(control);

                if (_controlEditors.TryGetValue(control.GetType(), out var controlEditor))
                {
                    controlEditor.Insert(type, e.GetPosition(control), control);
                }
            }
        }

        _isPressed = false;
        _isDragging = false;
        _dragItem = null;
    }

    private void ToolBox_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var listBoxItem = HitTestHelper.HitTest<ListBoxItem>(e, ControlTypes);
        if (listBoxItem is { })
        {
            _start = e.GetPosition(ControlTypes);
            _isPressed = true;
            _isDragging = false;
            _dragItem = listBoxItem;
        }
    }
}

