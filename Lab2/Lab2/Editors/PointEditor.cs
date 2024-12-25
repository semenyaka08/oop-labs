using System.Windows;
using System.Windows.Controls;
using Lab2.Abstraction;
using Lab2.Shapes;

namespace Lab2.Editors;

public class PointEditor : BaseEditor
{
    public override void StartDrawing(Point startPoint, Canvas canvas)
    {
        CurrentShape = new PointShape(startPoint);
        CurrentShape.Draw(canvas);
    }
}