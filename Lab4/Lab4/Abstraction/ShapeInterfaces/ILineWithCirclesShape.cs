using System.Windows;
using System.Windows.Controls;

namespace Lab4.Abstraction.ShapeInterfaces;

public interface ILineWithCirclesShape : ILineShape, IEllipseShape
{
    void DrawCirclesOnEnds(Canvas canvas);
    void UpdateCirclesOnEnds(Point start, Point end);
}