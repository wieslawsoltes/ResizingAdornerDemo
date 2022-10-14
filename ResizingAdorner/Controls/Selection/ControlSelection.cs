using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using ResizingAdorner.Controls.Model;

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
        _control.AddHandler(InputElement.PointerReleasedEvent, OnPointerReleased, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
    }

    public void Register(Control control)
    {
        _adorners.Add(control);
    }

    public void Unregister(Control control)
    {
        _adorners.Remove(control);
    }

    private ResizingAdornerPresenter? FindAdorner(Control control)
    {
        if (control is ResizingAdornerPresenter resizingAdornerPresenter)
        {
            return resizingAdornerPresenter;
        }

        if (control.Parent is ResizingAdornerPresenter resizingAdornerPresenterParent)
        {
            return resizingAdornerPresenterParent;
        }

        if (control.Parent is Control controlParent)
        {
            return FindAdorner(controlParent);
        }

        return null;
    }

    private ResizingAdornerPresenter? FindHover(PointerEventArgs e)
    {
        ResizingAdornerPresenter? hover = null;

        var point = e.GetPosition(_control);
        var result = _control.InputHitTest(point);
        if (result is Control control)
        {
            var resizingAdornerPresenter = FindAdorner(control);
            if (resizingAdornerPresenter is { })
            {
                hover = resizingAdornerPresenter;
            }
        }

        return hover;
    }

    private void Select(ResizingAdornerPresenter hover)
    {
        foreach (var adorner in _adorners)
        {
            if (adorner is ResizingAdornerPresenter resizingAdornerPresenter)
            {
                resizingAdornerPresenter.ShowThumbs = Equals(resizingAdornerPresenter, hover);
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
        var hover = FindHover(e);
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

    private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {

    }

    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        var hover = FindHover(e);
        if (hover is { } && Equals(hover, _hover))
        {
            return;
        }

        if (_hover is null)
        {
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
}
