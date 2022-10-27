using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace ResizingAdorner.XamlDom;

public class GridDemo
{
    public XamlDom Dom { get; }

    public GridDemo()
    {
        var pg = XamlPropertyRegistry.Properties[typeof(Grid)];
        var pe = XamlPropertyRegistry.Properties[typeof(Ellipse)];
        var pr = XamlPropertyRegistry.Properties[typeof(Rectangle)];

        var root = new XamlNode
        {
            ControlType = typeof(Grid),
            Values = new ()
            {
                [pg["Name"]] = "Grid",
                [pg["Width"]] = 500d,
                [pg["Height"]] = 500d,
                [pg["Background"]] = new SolidColorBrush(Colors.WhiteSmoke),
                [pg["ColumnDefinitions"]] = ColumnDefinitions.Parse("100,*,100"),
                [pg["RowDefinitions"]] = RowDefinitions.Parse("100,*,100"),
            },
            Children = new ()
            {
                new XamlNode
                {
                    ControlType = typeof(Ellipse),
                    Values = new ()
                    {
                        [pe["Fill"]] = new SolidColorBrush(Colors.Red),
                        [pe["Grid.Column"]] = 0,
                        [pe["Grid.Row"]] = 0,
                    },   
                },
                new XamlNode
                {
                    ControlType = typeof(Rectangle),
                    Values = new ()
                    {
                        [pr["Fill"]] = new SolidColorBrush(Colors.Green),
                        [pr["Grid.Column"]] = 1,
                        [pr["Grid.Row"]] = 1,
                    },   
                },
                new XamlNode
                {
                    ControlType = typeof(Rectangle),
                    Values = new ()
                    {
                        [pr["Fill"]] = new SolidColorBrush(Colors.Blue),
                        [pr["Grid.Column"]] = 2,
                        [pr["Grid.Row"]] = 2,
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
