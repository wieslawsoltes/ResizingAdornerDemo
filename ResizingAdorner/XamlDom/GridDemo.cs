using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace ResizingAdorner.XamlDom;

public class GridDemo
{
    public XamlDom Dom { get; }

    public GridDemo()
    {
        var root = new XamlDomNode
        {
            ControlType = typeof(Grid),
            Values = new ()
            {
                ["Name"] = "Grid",
                ["Width"] = 500d,
                ["Height"] = 500d,
                ["Background"] = new SolidColorBrush(Colors.White),
                ["ColumnDefinitions"] = ColumnDefinitions.Parse("100,*,100"),
                ["RowDefinitions"] = RowDefinitions.Parse("100,*,100"),
            },
            Children = new ()
            {
                new XamlDomNode
                {
                    ControlType = typeof(Ellipse),
                    Values = new ()
                    {
                        ["Fill"] = "Red",
                        ["Grid.Column"] = 0,
                        ["Grid.Row"] = 0,
                    },   
                },
                new XamlDomNode
                {
                    ControlType = typeof(Rectangle),
                    Values = new ()
                    {
                        ["Fill"] = new SolidColorBrush(Colors.Red),
                        ["Grid.Column"] = 1,
                        ["Grid.Row"] = 1,
                    },   
                },
                new XamlDomNode
                {
                    ControlType = typeof(Rectangle),
                    Values = new ()
                    {
                        ["Fill"] = new SolidColorBrush(Colors.Blue),
                        ["Grid.Column"] = 2,
                        ["Grid.Row"] = 2,
                    },   
                },
            }
        };

        Dom = new XamlDom
        {
            Root = root
        };

        Dom.Root.CreateControl();
    }
}
