using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Shape = Lab2.Abstraction.Shape;

namespace Lab2.Shapes;

public class EllipseShape : Shape
{
    private readonly Ellipse _ellipse;
    private Point _centerPoint;

    public EllipseShape(Point centerPoint)
    {
        _centerPoint = centerPoint;
        _ellipse = new Ellipse
        {
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };
    }

    public override void Draw(Canvas canvas)
    {
        Canvas.SetLeft(_ellipse, _centerPoint.X);
        Canvas.SetTop(_ellipse, _centerPoint.Y);
        canvas.Children.Add(_ellipse);
    }

    public override void Update(Point currentPoint)
    {
        double radiusX = Math.Abs(currentPoint.X - _centerPoint.X);
        double radiusY = Math.Abs(currentPoint.Y - _centerPoint.Y);
        
        _ellipse.Width = 2 * radiusX;
        _ellipse.Height = 2 * radiusY;
        
        Canvas.SetLeft(_ellipse, _centerPoint.X - radiusX);
        Canvas.SetTop(_ellipse, _centerPoint.Y - radiusY);
    }
}