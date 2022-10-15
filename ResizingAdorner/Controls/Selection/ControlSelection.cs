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
    private readonly Control _control;
    private ResizingAdornerPresenter? _hover;
    private ResizingAdornerPresenter? _selected;

    public ControlSelection(Control control)
    {
        _control = control;
        _control.AddHandler(InputElement.PointerPressedEvent, OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        _control.AddHandler(InputElement.PointerMovedEvent, OnPointerMoved, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
    }

    public void Register(Control adorner)
    {
        _adorners.Add(adorner);
    }

    public void Unregister(Control adorner)
    {
        _adorners.Remove(adorner);
    }

    private void Select(ResizingAdornerPresenter? hover, ResizingAdornerPresenter? selected)
    {
        foreach (var adorner in _adorners)
        {
            if (adorner is ResizingAdornerPresenter resizingAdornerPresenter)
            {
                var showThumbs = Equals(resizingAdornerPresenter, hover) || Equals(resizingAdornerPresenter, selected);
                resizingAdornerPresenter.ShowThumbs = showThumbs;
            }
        }
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
        var selected = HitTestHelper.HitTest<ResizingAdornerPresenter>(e, _control);
        if (selected != null)
        {
            _selected = selected;
            _hover = null;
            Select(_hover, _selected);
        }
        else
        {
            _selected = null;
            _hover = null;
            Deselect();
        }
    }

    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        var hitTest = HitTestHelper.HitTest<ResizingAdornerPresenter>(e, _control);
        if (hitTest is { } && (Equals(hitTest, _hover) || Equals(hitTest, _selected)))
        {
            return;
        }

        _hover = hitTest;

        if (_hover is { } || _selected is { })
        {
            Select(_hover, _selected);
        }
        else
        {
            Deselect();
        }
    }
}
