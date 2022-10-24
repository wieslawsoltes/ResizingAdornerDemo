using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace ResizingAdorner.Defaults;

public static class DefaultsProvider
{
    public static void AutoPositionAndStretch(Control control)
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

    public static void FixedPositionAndSize(Control control)
    {
        switch (control)
        {
            case Canvas canvas:
                canvas.Background = new SolidColorBrush(Colors.Gray);
                canvas.Classes.Add("resizing");
                canvas.Width = 200;
                canvas.Height = 200;
                break;
            case Grid grid:
                grid.Background = new SolidColorBrush(Colors.Gray);
                grid.Classes.Add("resizing");
                grid.Width = 200;
                grid.Height = 200;
                break;
            case StackPanel stackPanel:
                stackPanel.Background = new SolidColorBrush(Colors.Gray);
                stackPanel.Classes.Add("resizing");
                stackPanel.Width = 200;
                stackPanel.Height = 200;
                break;
            case WrapPanel wrapPanel:
                wrapPanel.Background = new SolidColorBrush(Colors.Gray);
                wrapPanel.Classes.Add("resizing");
                wrapPanel.Width = 200;
                wrapPanel.Height = 200;
                break;
            case DockPanel dockPanel:
                dockPanel.Background = new SolidColorBrush(Colors.Gray);
                dockPanel.Classes.Add("resizing");
                dockPanel.Width = 200;
                dockPanel.Height = 200;
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
}
