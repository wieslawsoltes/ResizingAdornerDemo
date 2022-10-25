using Avalonia;
using Avalonia.Controls;
using Avalonia.VisualTree;

namespace ResizingAdorner.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

        AttachedToVisualTree += OnAttachedToVisualTree;
    }

    private void OnAttachedToVisualTree(object? sender, VisualTreeAttachmentEventArgs e)
    {
        if (this.GetVisualRoot() is TopLevel topLevel)
        {
            Global.ControlSelection?.Initialize(topLevel);
        }
    }

    public void OnDelete()
    {
        Global.ControlSelection?.Delete();
    }
}
