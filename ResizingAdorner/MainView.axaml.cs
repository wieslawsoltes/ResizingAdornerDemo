using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using ResizingAdorner.Controls.Model;
using ResizingAdorner.Controls.Selection;
using ResizingAdorner.Controls.Utilities;
using ResizingAdorner.Editors;

namespace ResizingAdorner;

public partial class MainView : UserControl
{
    public static readonly IControlSelection? ControlSelection = new ControlSelection();
    private readonly CanvasEditor _canvasEditor = new ();
    private readonly GridEditor _gridEditor = new ();

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
        };
    }

    public void OnDelete()
    {
        ControlSelection?.Delete();
    }
}
