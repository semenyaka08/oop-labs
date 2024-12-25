using System.Windows;
using System.Windows.Controls;

namespace Lab4.Abstraction.ShapeInterfaces;

public interface IRectangleShape : IShape
{
    void DrawRectangle(Canvas canvas);
    void UpdateRectangle(Point startPoint, Point endPoint);
}