using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using ResizingAdorner.Controls.Model;
using ResizingAdorner.Controls.Utilities;

namespace ResizingAdorner.Controls.Selection;

public class ControlSelection : IControlSelection
{
    private readonly List<Control> _adorners = new ();
    private ResizingAdornerPresenter? _hover;
    private readonly Control _control;

    public ControlSelection(Control control)
    {
        _control = control;
        _control.AddHandler(InputElement.PointerPressedEvent, OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        _control.AddHandler(InputElement.PointerMovedEvent, OnPointerMoved, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
    }

    public void Register(Control control)
    {
        _adorners.Add(control);
    }

    public void Unregister(Control control)
    {
        _adorners.Remove(control);
    }

    private void Select(ResizingAdornerPresenter resizingAdornerPresenter)
    {
        resizingAdornerPresenter.ShowThumbs = true;
    }

    private void Deselect()
    {
        foreach (var adorner in _adorners)
        {
            if (adorner is ResizingAdornerPresenter resizingAdornerPresenter)
            {
                resizingAdornerPresenter.ShowThumbs = false;
            }
        }
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        var hover = HitTestHelper.HitTest<ResizingAdornerPresenter>(e, _control);
        if (hover != null)
        {
            _hover = hover;
            Select(hover);
        }
        else
        {
            _hover = null;
            Deselect();
        }
    }

    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        var hover = HitTestHelper.HitTest<ResizingAdornerPresenter>(e, _control);
        if (hover is { } && Equals(hover, _hover))
        {
            return;
        }

        if (_hover is not null)
        {
            return;
        }

        if (hover is { })
        {
            Select(hover);
        }
        else
        {
            Deselect();
        }
    }
}
