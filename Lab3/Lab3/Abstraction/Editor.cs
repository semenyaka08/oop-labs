using System.Windows;
using System.Windows.Controls;

namespace Lab3.Abstraction;

public abstract class Editor
{
    protected Shape? CurrentShape;

    public abstract void StartDrawing(Point startPoint, Canvas canvas);
    public void UpdateDrawing(Point currentPoint)
    {
        CurrentShape?.Update(currentPoint);
    }
    public void FinishDrawing(Point endPoint)
    {
        CurrentShape = null;
    }
}