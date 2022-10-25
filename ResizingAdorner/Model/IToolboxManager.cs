using Avalonia.Controls;

namespace ResizingAdorner.Model;

public interface IToolboxManager
{
    void Initialize(ListBox listBox);
    void DeInitialize();
}
