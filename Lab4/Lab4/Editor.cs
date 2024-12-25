using System.Windows;
using System.Windows.Controls;
using Lab4.Abstraction;

namespace Lab4;

public class Editor
{
    private readonly Func<Point, IShape> _shapeFactory;
    private IShape? _currentShape;

    public Editor(Func<Point, IShape> shapeFactory)
    {
        _shapeFactory = shapeFactory;
    }

    public void StartDrawing(Point startPoint, Canvas canvas)
    {
        _currentShape = _shapeFactory(startPoint);
        _currentShape.Draw(canvas);
    }

    public void UpdateDrawing(Point currentPoint)
    {
        _currentShape?.Update(currentPoint);
    }

    public void FinishDrawing(Point endPoint)
    {
        _currentShape = null;
    }
}