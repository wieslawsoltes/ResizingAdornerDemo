using Avalonia.Controls;
using Avalonia.Media;
using ResizingAdorner.Controls.Model;

namespace ResizingAdorner.Defaults;

public class BorderDefaults : IControlDefaults
{
    public void Auto(object control)
    {
        if (control is Border border)
        {
            border.Background = new SolidColorBrush(Colors.Gray);
        }
    }

    public void Fixed(object control)
    {
        if (control is Border border)
        {
            border.Background = new SolidColorBrush(Colors.Gray);
            border.Width = 200;
            border.Height = 200;
        }
    }
}
