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
        var topLevel = this.GetVisualRoot();
        if (topLevel is Control control)
        {
            ToolboxView.ControlSelection?.Initialize(control);
        }
    }

    public void OnDelete()
    {
        ToolboxView.ControlSelection?.Delete();
    }
}
