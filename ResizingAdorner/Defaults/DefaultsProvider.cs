using Avalonia.Controls;
using Avalonia.Controls.Primitives;
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
            case Panel panel:
                panel.Background = new SolidColorBrush(Colors.Gray);
                panel.Classes.Add("resizing");
                break;
            case Border border:
                border.Background = new SolidColorBrush(Colors.Gray);
                break;
            case Decorator decorator:
                break;
            case Viewbox viewbox:
                break;
            case ScrollViewer scrollViewer:
                scrollViewer.Background = new SolidColorBrush(Colors.Gray);
                break;
            case AccessText accessText:
                accessText.Text = "AccessText";
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
            case ContentControl contentControl:
                contentControl.Background = new SolidColorBrush(Colors.Gray);
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
            case Panel panel:
                panel.Background = new SolidColorBrush(Colors.Gray);
                panel.Classes.Add("resizing");
                panel.Width = 200;
                panel.Height = 200;
                break;
            case Border border:
                border.Background = new SolidColorBrush(Colors.Gray);
                border.Width = 200;
                border.Height = 200;
                break;
            case Decorator decorator:
                decorator.Width = 200;
                decorator.Height = 200;
                break;
            case Viewbox viewbox:
                viewbox.Width = 200;
                viewbox.Height = 200;
                break;
            case ScrollViewer scrollViewer:
                scrollViewer.Background = new SolidColorBrush(Colors.Gray);
                scrollViewer.Width = 200;
                scrollViewer.Height = 200;
                break;
            case AccessText accessText:
                accessText.Text = "AccessText";
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
            case ContentControl contentControl:
                contentControl.Background = new SolidColorBrush(Colors.Gray);
                contentControl.Width = 200;
                contentControl.Height = 200;
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
