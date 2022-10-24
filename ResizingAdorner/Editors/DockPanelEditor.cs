using System;
using Avalonia;
using Avalonia.Controls;
using ResizingAdorner.Defaults;

namespace ResizingAdorner.Editors;

public class DockPanelEditor
{
    public static void Insert(Type type, Point point, DockPanel dockPanel)
    {
        var obj = Activator.CreateInstance(type);
        if (obj is not Control control)
        {
            return;
        }

        DefaultsProvider.AutoPositionAndStretch(control);

        dockPanel.Children.Add(control);
    }
}
