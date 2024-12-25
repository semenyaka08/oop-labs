using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Lab5.Abstraction.ShapeInterfaces;

namespace Lab5.Shapes;

public class RectangleShape : IRectangleShape
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

    public void Draw(Canvas canvas)
    {
        DrawRectangle(canvas);
    }

    public void Update(Point currentPoint)
    {
        UpdateRectangle(_startPoint, currentPoint);
    }

    public void DrawRectangle(Canvas canvas)
    {
        Canvas.SetLeft(_rectangle, _startPoint.X);
        Canvas.SetTop(_rectangle, _startPoint.Y);
        canvas.Children.Add(_rectangle);
    }

    public void UpdateRectangle(Point startPoint, Point endPoint)
    {
        double width = Math.Abs(endPoint.X - startPoint.X);
        double height = Math.Abs(endPoint.Y - startPoint.Y);
        _rectangle.Width = width;
        _rectangle.Height = height;

        Canvas.SetLeft(_rectangle, Math.Min(endPoint.X, startPoint.X));
        Canvas.SetTop(_rectangle, Math.Min(endPoint.Y, startPoint.Y));
    }
}