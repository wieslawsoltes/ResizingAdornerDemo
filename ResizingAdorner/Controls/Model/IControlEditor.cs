using System;
using Avalonia;

namespace ResizingAdorner.Controls.Model;

public interface IControlEditor
{
    void Insert(Type type, Point point, object control, IControlDefaults? controlDefaults);
}
