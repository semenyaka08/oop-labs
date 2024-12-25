using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Abstraction_Shape = Lab3.Abstraction.Shape;
using Shape = Lab3.Abstraction.Shape;

namespace Lab3.Shapes;

public class LineShape : Abstraction_Shape
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

    public override void Draw(Canvas canvas)
    {
        canvas.Children.Add(_line);
    }

    public override void Update(Point currentPoint)
    {
        _line.X2 = currentPoint.X;
        _line.Y2 = currentPoint.Y;
    }
}