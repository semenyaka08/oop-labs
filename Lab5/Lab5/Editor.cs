using System.Windows;
using System.Windows.Controls;
using Lab5.Abstraction;

namespace Lab5;

public class Editor
{
    private static Editor? _instance; 
    private Func<Point, IShape>? _shapeFactory;
    private IShape? _currentShape;
    
    private Editor() { }
    
    public static Editor Instance => _instance ??= new Editor();
    
    public void SetShapeFactory(Func<Point, IShape> shapeFactory)
    {
        _shapeFactory = shapeFactory;
    }

    public void StartDrawing(Point startPoint, Canvas canvas)
    {
        if (_shapeFactory == null)
            throw new InvalidOperationException("Shape factory is not set.");
        
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