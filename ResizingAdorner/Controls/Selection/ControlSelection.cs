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

    private T? FindAdorner<T>(Control control) where T : Control
    {
        if (control is T resizingAdornerPresenter)
        {
            return resizingAdornerPresenter;
        }

        if (control.Parent is T resizingAdornerPresenterParent)
        {
            return resizingAdornerPresenterParent;
        }

        if (control.Parent is Control controlParent)
        {
            return FindAdorner<T>(controlParent);
        }

        return null;
    }

    private T? FindHover<T>(PointerEventArgs e) where T : Control
    {
        var point = e.GetPosition(_control);
        var result = _control.InputHitTest(point);
        if (result is not Control control)
        {
            return default;
        }

        var resizingAdornerPresenter = FindAdorner<T>(control);
        if (resizingAdornerPresenter is { })
        {
            return resizingAdornerPresenter;
        }

        return default;
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
        var hover = FindHover<ResizingAdornerPresenter>(e);
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
        var hover = FindHover<ResizingAdornerPresenter>(e);
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
