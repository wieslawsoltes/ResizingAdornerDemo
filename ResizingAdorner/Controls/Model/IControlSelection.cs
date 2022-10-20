using Avalonia.Controls;

namespace ResizingAdorner.Controls.Model;

public interface IControlSelection
{
    void Initialize(Control control);
    void DeInitialize();
    void Register(Control adorner);
    void Unregister(Control adorner);
    void Delete();
}
