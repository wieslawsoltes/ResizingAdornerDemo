using System;
using Avalonia;
using Avalonia.Controls;
using ResizingAdorner.Controls.Model;
using ResizingAdorner.Controls.Utilities;
using ResizingAdorner.Defaults;

namespace ResizingAdorner.Editors;

public class StackPanelEditor : IControlEditor
{
    public void Insert(Type type, Point point, object control)
    {
        if (control is not StackPanel stackPanel)
        {
            return;
        }

        if (TypeFactory.CreateControl(type) is not { } child)
        {
            return;
        }

        DefaultsProvider.AutoPositionAndStretch(child);

        stackPanel.Children.Add(child);
    }
}
