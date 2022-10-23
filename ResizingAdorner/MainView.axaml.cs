using System;
using System.Collections.Generic;
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

            _canvasEditor.Initialize(Canvas);
            _gridEditor.Initialize(Grid);
        };

        InitToolbox();
    }

    private void InitToolbox()
    {
        var controlType = typeof(Control);
        var controlsAssembly = controlType.Assembly;
        var controlTypes = new List<Type>();

        foreach (var t in controlsAssembly.GetTypes())
        {
            if (!t.IsAbstract || t.IsPublic || t.IsClass)
            {
                var b = t.BaseType;
                while (b != null)
                {
                    if (b == controlType)
                    {
                        controlTypes.Add(t);
                        break;
                    }

                    b = b.BaseType;
                }
            }
        }

        controlTypes.Sort((a, b) => a.Name.CompareTo(b.Name));

        ControlTypes.Items = controlTypes;
    }
    
    public void OnDelete()
    {
        ControlSelection?.Delete();
    }

    public void OnInsertGrid(Type type)
    {
        _gridEditor.Insert(type, _gridEditor.InsertPoint);
    }

    public void OnInsertCanvas(Type type)
    {
        _canvasEditor.Insert(type, _canvasEditor.InsertPoint);
    }
}
