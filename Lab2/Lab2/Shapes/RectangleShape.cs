using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Shape = Lab2.Abstraction.Shape;

namespace Lab2.Shapes;

public class RectangleShape : Shape
{
    private readonly Rectangle _rectangle;
    private Point _startPoint;

    public RectangleShape(Point startPoint)
    {
        _startPoint = startPoint;
        _rectangle = new Rectangle
        {
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };
    }

    public override void Draw(Canvas canvas)
    {
        Canvas.SetLeft(_rectangle, _startPoint.X);
        Canvas.SetTop(_rectangle, _startPoint.Y);
        canvas.Children.Add(_rectangle);
    }

    public override void Update(Point currentPoint)
    {
        double width = Math.Abs(currentPoint.X - _startPoint.X);
        double height = Math.Abs(currentPoint.Y - _startPoint.Y);
        _rectangle.Width = width;
        _rectangle.Height = height;

        Canvas.SetLeft(_rectangle, Math.Min(currentPoint.X, _startPoint.X));
        Canvas.SetTop(_rectangle, Math.Min(currentPoint.Y, _startPoint.Y));
    }
}