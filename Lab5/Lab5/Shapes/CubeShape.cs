using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Lab5.Abstraction.ShapeInterfaces;

namespace Lab5.Shapes;

public class CubeShape : ICubeShape
{
    private readonly RectangleShape _baseRectangle;
    private readonly Line _edge1;
    private readonly Line _edge2;
    private readonly Line _edge3;
    private readonly Line _edge4;
    
    private readonly Line _backEdge1;
    private readonly Line _backEdge2;
    private readonly Line _backEdge3;
    private readonly Line _backEdge4;
    
    private readonly Point _startPoint;
    
    public CubeShape(Point startPoint)
    {
        _startPoint = startPoint;
        _baseRectangle = new RectangleShape(startPoint);

        _edge1 = CreateEdgeLine();
        _edge2 = CreateEdgeLine();
        _edge3 = CreateEdgeLine();
        _edge4 = CreateEdgeLine();
        
        _backEdge1 = CreateEdgeLine();
        _backEdge2 = CreateEdgeLine();
        _backEdge3 = CreateEdgeLine();
        _backEdge4 = CreateEdgeLine();
    }
    
    private Line CreateEdgeLine()
    {
        return new Line
        {
            Stroke = Brushes.Black,
            StrokeThickness = 2
        };
    }
    
    public void Draw(Canvas canvas)
    {
        DrawRectangle(canvas);
        DrawCubeEdges(canvas);
    }

    public void Update(Point currentPoint)
    {
        UpdateRectangle(_startPoint, currentPoint);
        UpdateCubeEdges(_startPoint, currentPoint);
    }

    public void DrawRectangle(Canvas canvas)
    {
        _baseRectangle.DrawRectangle(canvas);
    }

    public void UpdateRectangle(Point startPoint, Point endPoint)
    {
        _baseRectangle.UpdateRectangle(startPoint, endPoint);
    }

    public void DrawLine(Canvas canvas)
    {
        DrawCubeEdges(canvas);
    }

    public void UpdateLine(Point start, Point end)
    {
        UpdateCubeEdges(start, end);
    }

    public void DrawCubeEdges(Canvas canvas)
    {
        canvas.Children.Add(_edge1);
        canvas.Children.Add(_edge2);
        canvas.Children.Add(_edge3);
        canvas.Children.Add(_edge4);
        
        canvas.Children.Add(_backEdge1);
        canvas.Children.Add(_backEdge2);
        canvas.Children.Add(_backEdge3);
        canvas.Children.Add(_backEdge4);
    }

    public void UpdateCubeEdges(Point startPoint, Point endPoint)
    {
        double width = Math.Abs(endPoint.X - startPoint.X);
        double height = Math.Abs(endPoint.Y - startPoint.Y);

        double leftX = Math.Min(startPoint.X, endPoint.X);
        double topY = Math.Min(startPoint.Y, endPoint.Y);
        double rightX = Math.Max(startPoint.X, endPoint.X);
        double bottomY = Math.Max(startPoint.Y, endPoint.Y);

        double offsetX = width / 4;
        double offsetY = height / 4;

        SetLinePosition(_edge1, leftX, topY, leftX + offsetX, topY - offsetY);
        SetLinePosition(_edge2, rightX, topY, rightX + offsetX, topY - offsetY);
        SetLinePosition(_edge3, leftX, bottomY, leftX + offsetX, bottomY - offsetY);
        SetLinePosition(_edge4, rightX, bottomY, rightX + offsetX, bottomY - offsetY);

        SetLinePosition(_backEdge1, leftX + offsetX, topY - offsetY, rightX + offsetX, topY - offsetY);
        SetLinePosition(_backEdge2, leftX + offsetX, topY - offsetY, leftX + offsetX, bottomY - offsetY);
        SetLinePosition(_backEdge3, rightX + offsetX, topY - offsetY, rightX + offsetX, bottomY - offsetY);
        SetLinePosition(_backEdge4, leftX + offsetX, bottomY - offsetY, rightX + offsetX, bottomY - offsetY);
    }
    
    private void SetLinePosition(Line line, double x1, double y1, double x2, double y2)
    {
        line.X1 = x1;
        line.Y1 = y1;
        line.X2 = x2;
        line.Y2 = y2;
    }
}