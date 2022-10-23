using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace ResizingAdorner.Editors;

public class CanvasEditor
{
    private Canvas? _canvas;

    public Point InsertPoint { get; private set; }

    public void Initialize(Canvas canvas)
    {
        _canvas = canvas;
        _canvas.AddHandler(InputElement.PointerPressedEvent, Canvas_PointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
    }

    public void DeInitialize()
    {
        if (_canvas is { })
        {
            _canvas.RemoveHandler(InputElement.PointerPressedEvent, Canvas_PointerPressed);
            _canvas = null;
        }
    }

    private void Canvas_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (_canvas is null)
        {
            return;
        }

        if (e.GetCurrentPoint(_canvas).Properties.IsRightButtonPressed)
        {
            InsertPoint = e.GetPosition(_canvas);
        }
    }

    private void SetControlDefaults(Control control)
    {
        switch (control)
        {
            case TextBlock textBlock:
                textBlock.Text = "TextBlock";
                break;
            case TextBox textBox:
                textBox.Text = "TextBox";
                break;
            case Label label:
                label.Content = "Label";
                break;
            case CheckBox checkBox:
                checkBox.Content = "CheckBox";
                break;
            case RadioButton radioButton:
                radioButton.Content = "RadioButton";
                break;
            case Button button:
                button.Content = "Button";
                break;
            case Slider slider:
                slider.Value = 50;
                slider.Width = 150;
                break;
            case Ellipse ellipse:
                ellipse.Fill = new SolidColorBrush(Colors.Gray);
                ellipse.Width = 100;
                ellipse.Height = 100;
                break;
            case Rectangle rectangle:
                rectangle.Fill = new SolidColorBrush(Colors.Gray);
                rectangle.Width = 100;
                rectangle.Height = 100;
                break;
        }
    }

    public void Insert(Type type, Point point)
    {
        if (_canvas is null)
        {
            return;
        }

        var obj = Activator.CreateInstance(type);
        if (obj is not Control control)
        {
            return;
        }

        SetControlDefaults(control);

        Canvas.SetLeft(control, point.X);
        Canvas.SetTop(control, point.Y);

        _canvas.Children.Add(control);
    }
}
