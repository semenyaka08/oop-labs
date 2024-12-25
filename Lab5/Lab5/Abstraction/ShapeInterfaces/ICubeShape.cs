using System.Windows;
using System.Windows.Controls;

namespace Lab5.Abstraction.ShapeInterfaces;

public interface ICubeShape : IRectangleShape, ILineShape
{
    void DrawCubeEdges(Canvas canvas);
    void UpdateCubeEdges(Point startPoint, Point endPoint);
}