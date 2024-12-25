using System.Windows;
using System.Windows.Controls;
using Lab3.Abstraction;
using Lab3.Shapes;

namespace Lab3.Editors;

public class RectangleEditor : Editor
{
    public override void StartDrawing(Point startPoint, Canvas canvas)
    {
        CurrentShape = new RectangleShape(startPoint);
        CurrentShape.Draw(canvas);
    }
}