﻿using System;
using Avalonia;
using Avalonia.Controls;
using ResizingAdorner.Controls.Model;
using ResizingAdorner.Controls.Utilities;
using ResizingAdorner.Defaults;

namespace ResizingAdorner.Editors;

public class WrapPanelEditor : IControlEditor
{
    public void Insert(Type type, Point point, object control)
    {
        if (control is not WrapPanel wrapPanel)
        {
            return;
        }

        if (TypeFactory.CreateControl(type) is not { } child)
        {
            return;
        }

        DefaultsProvider.FixedPositionAndSize(child);

        wrapPanel.Children.Add(child);
    }
}
