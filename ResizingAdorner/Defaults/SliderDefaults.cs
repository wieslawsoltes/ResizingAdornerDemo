using Avalonia.Controls;
using ResizingAdorner.Controls.Model;

namespace ResizingAdorner.Defaults;

public class SliderDefaults : IControlDefaults
{
    public void Auto(object control)
    {
        if (control is Slider slider)
        {
            slider.Value = 50;
        }
    }

    public void Fixed(object control)
    {
        if (control is Slider slider)
        {
            slider.Value = 50;
            slider.Width = 150;
        }
    }
}
