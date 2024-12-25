using System.Windows;
using System.Windows.Controls;
using Lab3.Abstraction;
using Lab3.Shapes;

namespace Lab3.Editors;

public class PointEditor : Editor
{
    public override void StartDrawing(Point startPoint, Canvas canvas)
    {
        CurrentShape = new PointShape(startPoint);
        CurrentShape.Draw(canvas);
    }
}