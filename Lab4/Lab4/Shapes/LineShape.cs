using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Lab4.Abstraction.ShapeInterfaces;

namespace Lab4.Shapes;

public class LineShape : ILineShape
{
    private readonly Line _line;

    public LineShape(Point startPoint)
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
    }

    public void Draw(Canvas canvas)
    {
        canvas.Children.Add(_line);
    }

    public void Update(Point currentPoint)
    {
        UpdateLine(new Point(_line.X1, _line.Y1), currentPoint);
    }


    public void DrawLine(Canvas canvas)
    {
        canvas.Children.Add(_line);
    }

    public void UpdateLine(Point start, Point end)
    {
        _line.X1 = start.X;
        _line.Y1 = start.Y;
        _line.X2 = end.X;
        _line.Y2 = end.Y;
    }
}