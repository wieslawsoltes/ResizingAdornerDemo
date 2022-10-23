using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using ResizingAdorner.Controls.Model;
using ResizingAdorner.Controls.Selection;
using ResizingAdorner.Controls.Utilities;
using ResizingAdorner.Editors;

namespace ResizingAdorner;

public partial class MainView : UserControl
{
    public static readonly IControlSelection? ControlSelection = new ControlSelection();

    private readonly CanvasEditor _canvasEditor = new ();
    private readonly GridEditor _gridEditor = new ();

    public MainView()
    {
        InitializeComponent();

        AttachedToVisualTree += (_, _) =>
        {
            var topLevel = this.GetVisualRoot();
            if (topLevel is Control control)
            {
                ControlSelection?.Initialize(control);
            }

            _canvasEditor.Initialize(Canvas);
            _gridEditor.Initialize(Grid);
        };

        InitToolbox();
        
        ControlTypes.AddHandler(InputElement.PointerPressedEvent, ToolBox_PointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        ControlTypes.AddHandler(InputElement.PointerReleasedEvent, ToolBox_PointerReleased, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        ControlTypes.AddHandler(InputElement.PointerMovedEvent, ToolBox_PointerMoved, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
    }

    private bool _isPressed;
    private bool _isDragging;
    private Point _start;
    private ListBoxItem? _dragItem;
    
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
        if (_isDragging)
        {
            var inputElement = this.InputHitTest(e.GetPosition(ControlTypes));
            Console.WriteLine(inputElement);

            if (Equals(inputElement, Canvas))
            {
                _canvasEditor.Insert(_dragItem.DataContext as Type, e.GetPosition(Canvas));
            }
            
            if (Equals(inputElement, Grid))
            {
                _gridEditor.Insert(_dragItem.DataContext as Type, e.GetPosition(Grid));
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

    private void InitToolbox()
    {
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

        controlTypes.Sort((a, b) => a.Name.CompareTo(b.Name));

        ControlTypes.Items = controlTypes;
    }
    
    public void OnDelete()
    {
        ControlSelection?.Delete();
    }

    public void OnInsertGrid(Type type)
    {
        _gridEditor.Insert(type, _gridEditor.InsertPoint);
    }

    public void OnInsertCanvas(Type type)
    {
        _canvasEditor.Insert(type, _canvasEditor.InsertPoint);
    }
}
