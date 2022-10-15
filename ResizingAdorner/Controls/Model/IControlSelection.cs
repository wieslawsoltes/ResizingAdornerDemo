using Avalonia.Controls;

namespace ResizingAdorner.Controls.Model;

public interface IControlSelection
{
    void Register(Control adorner);
    void Unregister(Control adorner);
    void Delete();
}
