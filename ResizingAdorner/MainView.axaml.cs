using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.VisualTree;
using ResizingAdorner.Controls.Model;
using ResizingAdorner.Controls.Selection;

namespace ResizingAdorner;

public partial class MainView : UserControl
{
    public static readonly IControlSelection? ControlSelection = new ControlSelection();

    private Point _insertPoint;

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

            Canvas.AddHandler(InputElement.PointerPressedEvent, Canvas_PointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        };
    }

    private void Canvas_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.GetCurrentPoint(Canvas).Properties.IsRightButtonPressed)
        {
            _insertPoint = e.GetPosition(Canvas);
        }
    }

    public void OnDelete()
    {
        ControlSelection?.Delete();
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

    public void OnInsert(Type type)
    {
        var obj = Activator.CreateInstance(type);
        if (obj is Control control)
        {
            SetControlDefaults(control);

            Canvas.SetLeft(control, _insertPoint.X);
            Canvas.SetTop(control, _insertPoint.Y);

            Canvas.Children.Add(control);
        }
    }
}

