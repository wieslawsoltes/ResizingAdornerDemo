using System;
using Avalonia;
using Avalonia.Controls;
using ResizingAdorner.Controls.Model;
using ResizingAdorner.Controls.Utilities;
using ResizingAdorner.Defaults;

namespace ResizingAdorner.Editors;

public class DockPanelEditor : IControlEditor
{
    public void Insert(Type type, Point point, object control)
    {
        if (control is not DockPanel dockPanel)
        {
            return;
        }

        if (TypeFactory.CreateControl(type) is not { } child)
        {
            return;
        }

        DefaultsProvider.AutoPositionAndStretch(child);

        dockPanel.Children.Add(child);
    }
}
