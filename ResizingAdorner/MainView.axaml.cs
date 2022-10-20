using System;
using Avalonia.Controls;
using Avalonia.VisualTree;
using ResizingAdorner.Controls.Model;
using ResizingAdorner.Controls.Selection;
using ResizingAdorner.Editors;

namespace ResizingAdorner;

public partial class MainView : UserControl
{
    public static readonly IControlSelection? ControlSelection = new ControlSelection();

    private readonly CanvasEditor _canvasEditor = new ();

    public MainView()
    {
        InitializeComponent();

        AttachedToVisualTree += (_, _) =>
        {
            var topLevel = this.GetVisualRoot();
            if (topLevel is Control control)
            {
                ControlSelection?.Initialize(control);
            }

            _canvasEditor.Initialize(Canvas);
        };
    }

    public void OnDelete()
    {
        ControlSelection?.Delete();
    }

    public void OnInsert(Type type)
    {
        _canvasEditor.Insert(type, _canvasEditor.InsertPoint);
    }
}
