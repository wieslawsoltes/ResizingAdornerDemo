using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;

namespace ResizingAdorner;

public class BoundsAdorner : Panel
{
    public static readonly StyledProperty<Control?> AdornedControlProperty = 
        AvaloniaProperty.Register<BoundsAdorner, Control?>(nameof(AdornedControl));

    public Control? AdornedControl
    {
        get => GetValue(AdornedControlProperty);
        set => SetValue(AdornedControlProperty, value);
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        if (AdornedControl is { })
        {
            Editor.s_adorners.Add(this);
        }
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        
        if (AdornedControl is { })
        {
            Editor.s_adorners.Remove(this);
        }
    }
}

public class Editor : AvaloniaObject
{
    public static readonly AttachedProperty<bool> IsPointerOverProperty = 
        AvaloniaProperty.RegisterAttached<Control, bool>("IsPointerOver", typeof(Editor));

    public static readonly AttachedProperty<bool> IsSelectedProperty = 
        AvaloniaProperty.RegisterAttached<Control, bool>("IsSelected", typeof(Editor));

    public static List<Control> s_adorners = new (); 

    static Editor()
    {
        IsSelectedProperty.Changed.Subscribe(IsSelectedChanged);
    }
    
    public static bool GetIsPointerOver(Control obj)
    {
        return obj.GetValue(IsPointerOverProperty);
    }

    public static void SetIsPointerOver(Control obj, bool value)
    {
        obj.SetValue(IsPointerOverProperty, value);
    }

    public static bool GetIsSelected(Control obj)
    {
        return obj.GetValue(IsSelectedProperty);
    }

    public static void SetIsSelected(Control obj, bool value)
    {
        obj.SetValue(IsSelectedProperty, value);
    }

    private static void IsSelectedChanged(AvaloniaPropertyChangedEventArgs<bool> e)
    {
        var oldIsSelected = e.OldValue.GetValueOrDefault();
        var newIsSelected = e.NewValue.GetValueOrDefault();

        if (oldIsSelected == newIsSelected)
        {
            return;
        }

        if (e.Sender is Control control)
        {
            SetSelectedPseudoClass(newIsSelected, control);
        }
    }

    public static void SetSelectedPseudoClass(bool isSelected, Control control)
    {
        if (isSelected)
        {
            ((IPseudoClasses)control.Classes).Add(":selected");
        }
        else
        {
            ((IPseudoClasses)control.Classes).Remove(":selected");
        }
    }
    
    public static void SetHoverPseudoClass(bool isPointerOver, Control control)
    {
        if (isPointerOver)
        {
            ((IPseudoClasses)control.Classes).Add(":hover");
        }
        else
        {
            ((IPseudoClasses)control.Classes).Remove(":hover");
        }
    }
}
