using Avalonia.Controls;

namespace ResizingAdorner;

public interface IControlSelection
{
    void Register(Control control);
    void Unregister(Control control);
}
