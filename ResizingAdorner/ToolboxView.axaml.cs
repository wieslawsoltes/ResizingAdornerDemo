using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
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

        var controlType = typeof(Control);
        var controlsAssembly = controlType.Assembly;
        var controlTypes = new List<Type>();

        foreach (var t in controlsAssembly.GetTypes())
        {
            if (!t.IsAbstract && t.IsPublic && t.IsClass && !t.ContainsGenericParameters)
            {
                var b = t.BaseType;
                while (b != null)
                {
                    if (b == controlType)
                    {
                        controlTypes.Add(t);
                        break;
                    }

                    b = b.BaseType;
                }
            }
        }

        controlTypes.Sort((a, b) => string.Compare(a.Name, b.Name, StringComparison.Ordinal));

        ControlTypes.Items = controlTypes;
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

    private void ToolBox_PointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        if (_isDragging && _dragItem is { } && _dragItem.DataContext is Type type)
        {
            var inputElement = this.InputHitTest(e.GetPosition(ControlTypes));
            Console.WriteLine(inputElement);

            if (inputElement is Canvas canvas)
            {
                CanvasEditor.Insert(type, e.GetPosition(canvas), canvas);
            }
            
            if (inputElement is Grid grid)
            {
                GridEditor.Insert(type, e.GetPosition(grid), grid);
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

