using Avalonia.Controls;

namespace ResizingAdorner.Controls;

public interface IControlSelection
{
    void Register(Control control);
    void Unregister(Control control);
}
