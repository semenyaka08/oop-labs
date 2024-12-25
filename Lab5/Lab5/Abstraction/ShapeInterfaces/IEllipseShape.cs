using System.Windows;
using System.Windows.Controls;

namespace Lab5.Abstraction.ShapeInterfaces;

public interface IEllipseShape : IShape
{
    void DrawEllipse(Canvas canvas);
    void UpdateEllipsePosition(Point currentPoint);
}