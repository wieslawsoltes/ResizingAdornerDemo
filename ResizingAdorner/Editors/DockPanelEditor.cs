using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace ResizingAdorner.Editors;

public class DockPanelEditor
{
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
    
    public static void Insert(Type type, Point point, DockPanel dockPanel)
    {
        var obj = Activator.CreateInstance(type);
        if (obj is not Control control)
        {
            return;
        }

        SetControlDefaults(control);

        dockPanel.Children.Add(control);
    }
}
