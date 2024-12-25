using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Lab4.Abstraction.ShapeInterfaces;

namespace Lab4.Shapes;

public class LineWithCircles : ILineWithCirclesShape
{
    private readonly Line _line;
    private readonly Ellipse _startCircle;
    private readonly Ellipse _endCircle;
    private const double CircleRadius = 5;

    public LineWithCircles(Point startPoint)
    {
        _line = new Line
        {
            Stroke = Brushes.Black,
            StrokeThickness = 2,
            X1 = startPoint.X,
            Y1 = startPoint.Y,
            X2 = startPoint.X,
            Y2 = startPoint.Y
        };

        _startCircle = CreateCircle(startPoint);
        _endCircle = CreateCircle(startPoint);
    }
    
    private Ellipse CreateCircle(Point center)
    {
        return new Ellipse
        {
            Stroke = Brushes.Black,
            Fill = Brushes.Black,
            Width = CircleRadius * 2,
            Height = CircleRadius * 2
        };
    }
    
    public void Draw(Canvas canvas)
    {
        DrawLine(canvas);
        DrawCirclesOnEnds(canvas);
    }

    public void Update(Point currentPoint)
    {
        UpdateLine(new Point(X1, Y1), currentPoint);
        UpdateCirclesOnEnds(new Point(X1, Y1), currentPoint);
    }

    private double X1 { get => _line.X1; set => _line.X1 = value; }
    private double Y1 { get => _line.Y1; set => _line.Y1 = value; }
    private double X2 { get => _line.X2; set => _line.X2 = value; }
    private double Y2 { get => _line.Y2; set => _line.Y2 = value; }
    
    public void DrawLine(Canvas canvas)
    {
        canvas.Children.Add(_line);
    }

    public void UpdateLine(Point start, Point end)
    {
        X1 = start.X;
        Y1 = start.Y;
        X2 = end.X;
        Y2 = end.Y;
    }

    public void DrawEllipse(Canvas canvas)
    {
        throw new NotImplementedException();
    }

    public void UpdateEllipsePosition(Point currentPoint)
    {
        throw new NotImplementedException();
    }

    public void DrawCirclesOnEnds(Canvas canvas)
    {
        canvas.Children.Add(_startCircle);
        canvas.Children.Add(_endCircle);
        UpdateCirclesOnEnds(new Point(X1, Y1), new Point(X2, Y2));
    }

    public void UpdateCirclesOnEnds(Point start, Point end)
    {
        UpdateCirclePosition(_startCircle, start.X, start.Y);
        UpdateCirclePosition(_endCircle, end.X, end.Y);
    }
    
    private void UpdateCirclePosition(Ellipse circle, double x, double y)
    {
        Canvas.SetLeft(circle, x - CircleRadius);
        Canvas.SetTop(circle, y - CircleRadius);
    }
}