using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Abstraction_Shape = Lab3.Abstraction.Shape;
using Shape = Lab3.Abstraction.Shape;


namespace Lab3.Shapes;

public class PointShape : Abstraction_Shape
{
    private readonly Ellipse _point;
    private Point _startPoint;

    public PointShape(Point startPoint)
    {
        _startPoint = startPoint;
        _point = new Ellipse
        {
            Fill = Brushes.Black,
            Width = 5,
            Height = 5
        };
    }

    public override void Draw(Canvas canvas)
    {
        Canvas.SetLeft(_point, _startPoint.X - 2.5);
        Canvas.SetTop(_point, _startPoint.Y - 2.5);
        canvas.Children.Add(_point);
    }

    public override void Update(Point currentPoint)
    { }
}