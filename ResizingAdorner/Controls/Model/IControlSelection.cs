using Avalonia.Controls;

namespace ResizingAdorner.Controls.Model;

public interface IControlSelection
{
    void Register(Control control);
    void Unregister(Control control);
}
