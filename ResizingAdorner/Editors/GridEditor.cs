using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using ResizingAdorner.Controls.Utilities;

namespace ResizingAdorner.Editors;

public class GridEditor
{
    private Grid? _grid;

    public Point InsertPoint { get; private set; }

    public void Initialize(Grid grid)
    {
        _grid = grid;
        _grid.AddHandler(InputElement.PointerPressedEvent, Grid_PointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
    }

    public void DeInitialize()
    {
        if (_grid is { })
        {
            _grid.RemoveHandler(InputElement.PointerPressedEvent, Grid_PointerPressed);
            _grid = null;
        }
    }

    private void Grid_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (_grid is null)
        {
            return;
        }

        if (e.GetCurrentPoint(_grid).Properties.IsRightButtonPressed)
        {
            InsertPoint = e.GetPosition(_grid);
        }
    }

    private static void SetControlDefaults(Control control)
    {
        switch (control)
        {
            case Canvas canvas:
                canvas.Background = new SolidColorBrush(Colors.Gray);
                canvas.Classes.Add("resizing");
                break;
            case Grid grid:
                grid.Background = new SolidColorBrush(Colors.Gray);
                grid.Classes.Add("resizing");
                break;
            case StackPanel stackPanel:
                stackPanel.Background = new SolidColorBrush(Colors.Gray);
                stackPanel.Classes.Add("resizing");
                break;
            case WrapPanel wrapPanel:
                wrapPanel.Background = new SolidColorBrush(Colors.Gray);
                wrapPanel.Classes.Add("resizing");
                break;
            case DockPanel dockPanel:
                dockPanel.Background = new SolidColorBrush(Colors.Gray);
                dockPanel.Classes.Add("resizing");
                break;
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
                break;
            case Ellipse ellipse:
                ellipse.Fill = new SolidColorBrush(Colors.Gray);
                break;
            case Rectangle rectangle:
                rectangle.Fill = new SolidColorBrush(Colors.Gray);
                break;
        }
    }

    public static void Insert(Type type, Point point, Grid grid)
    {
        var obj = Activator.CreateInstance(type);
        if (obj is not Control control)
        {
            return;
        }

        SetControlDefaults(control);

        var cells = GridHelper.GetCells(grid);

        foreach (var cell in cells)
        {
            if (cell.Bounds.Contains(point))
            {
                Grid.SetColumn(control, cell.Column);
                Grid.SetRow(control, cell.Row);
            }
        }

        grid.Children.Add(control);
    }
}
