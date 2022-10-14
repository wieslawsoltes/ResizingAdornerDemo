using Avalonia.Controls;

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
