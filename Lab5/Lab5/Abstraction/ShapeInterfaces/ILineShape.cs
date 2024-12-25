using System.Windows;
using System.Windows.Controls;

namespace Lab5.Abstraction.ShapeInterfaces;

public interface ILineShape : IShape
{
    void DrawLine(Canvas canvas);
    void UpdateLine(Point start, Point end);
}