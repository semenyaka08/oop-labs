using System.Windows;
using System.Windows.Controls;

namespace Lab2.Abstraction;

public abstract class BaseEditor
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