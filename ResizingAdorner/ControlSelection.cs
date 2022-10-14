using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace ResizingAdorner;

public class ControlSelection
{
    private ResizingAdornerPresenter? _hover = null;
    private readonly Control _control;

    public ControlSelection(Control control)
    {
        _control = control;
        _control.AddHandler(InputElement.PointerPressedEvent, OnPointerPressed, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        _control.AddHandler(InputElement.PointerMovedEvent, OnPointerMoved, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
        _control.AddHandler(InputElement.PointerReleasedEvent, OnPointerReleased, RoutingStrategies.Tunnel | RoutingStrategies.Bubble);
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

    private void Deselect()
    {
        foreach (var adorner in Editor.s_adorners)
        {
            if (adorner is ResizingAdornerPresenter resizingAdornerPresenter)
            {
                resizingAdornerPresenter.ShowThumbs = false;
            }
        }
    }

    private void Select(ResizingAdornerPresenter hover)
    {
        foreach (var adorner in Editor.s_adorners)
        {
            if (adorner is ResizingAdornerPresenter resizingAdornerPresenter)
            {
                if (!Equals(resizingAdornerPresenter, hover))
                {
                    resizingAdornerPresenter.ShowThumbs = false;
                }
                else
                {
                    resizingAdornerPresenter.ShowThumbs = true;
                }
            }
        }
    }
}
