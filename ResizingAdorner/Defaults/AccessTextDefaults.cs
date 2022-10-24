using Avalonia.Controls.Primitives;
using ResizingAdorner.Controls.Model;

namespace ResizingAdorner.Defaults;

public class AccessTextDefaults : IControlDefaults
{
    public void Auto(object control)
    {
        if (control is AccessText accessText)
        {
            accessText.Text = "AccessText";
        }
    }

    public void Fixed(object control)
    {
        if (control is AccessText accessText)
        {
            accessText.Text = "AccessText"; 
        }
    }
}
