using Avalonia.Controls;
using ResizingAdorner.Controls.Model;
using ResizingAdorner.Controls.Selection;

namespace ResizingAdorner;

public partial class MainWindow : Window
{
    public static IControlSelection? s_controlSelection;

    public MainWindow()
    {
        InitializeComponent();

        s_controlSelection = new ControlSelection(this);
    }
}
