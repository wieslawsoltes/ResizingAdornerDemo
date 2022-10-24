using Avalonia.Controls;
using ResizingAdorner.Controls.Model;

namespace ResizingAdorner.Defaults;

public class ProgressBarDefaults : IControlDefaults
{
    public void Auto(object control)
    {
        if (control is ProgressBar progressBar)
        {
            progressBar.Value = 50;
        }
    }

    public void Fixed(object control)
    {
        if (control is ProgressBar progressBar)
        {
            progressBar.Value = 50;
            progressBar.Height = 20;
            progressBar.Width = 150;
        }
    }
}
