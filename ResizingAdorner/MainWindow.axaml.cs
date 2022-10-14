using Avalonia.Controls;

namespace ResizingAdorner;

public partial class MainWindow : Window
{
    private readonly ControlSelection _controlSelection;

    public MainWindow()
    {
        InitializeComponent();

        _controlSelection = new ControlSelection(this);
    }
}
