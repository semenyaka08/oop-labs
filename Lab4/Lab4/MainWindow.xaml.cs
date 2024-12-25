using System.Windows;
using System.Windows.Input;
using Lab4.Shapes;

namespace Lab4;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private Editor _currentEditor = null!;
    private bool _isDrawing;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void UpdateSelectedShapeMenu(string shapeName)
    {
        SelectedShapeMenuItem.Header = $"Вибрано: {shapeName}";
    }
    
    private void OnLineClick(object sender, RoutedEventArgs e)
    {
        _currentEditor = new Editor(startPoint => new LineShape(startPoint));
        UpdateSelectedShapeMenu("Лінія");
    }

    private void OnRectangleClick(object sender, RoutedEventArgs e)
    {
        _currentEditor = new Editor(startPoint => new RectangleShape(startPoint));
        UpdateSelectedShapeMenu("Прямокутник");
    }

    private void OnEllipseClick(object sender, RoutedEventArgs e)
    {
        _currentEditor = new Editor(startPoint => new EllipseShape(startPoint));
        UpdateSelectedShapeMenu("Еліпс");
    }
    
    private void OnPointClick(object sender, RoutedEventArgs e)
    {
        _currentEditor = new Editor(startPoint => new PointShape(startPoint));
        UpdateSelectedShapeMenu("Точка");
    }
    
    private void OnCubeClick(object sender, RoutedEventArgs e)
    {
        _currentEditor = new Editor(startPoint => new CubeShape(startPoint));
        UpdateSelectedShapeMenu("Куб");
    }
    
    private void OnLineWithCirclesClick(object sender, RoutedEventArgs e)
    {
        _currentEditor = new Editor(startPoint => new LineWithCircles(startPoint));
        UpdateSelectedShapeMenu("Лінія з кружечками на кінцях");
    }
    
    private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        _isDrawing = true;
        Point startPoint = e.GetPosition(DrawingCanvas);
        _currentEditor.StartDrawing(startPoint, DrawingCanvas);
    }

    private void Canvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_isDrawing)
            return;

        Point currentPosition = e.GetPosition(DrawingCanvas);
        _currentEditor.UpdateDrawing(currentPosition);
    }

    private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (_isDrawing)
        {
            Point endPoint = e.GetPosition(DrawingCanvas);
            _currentEditor.FinishDrawing(endPoint);
            _isDrawing = false;
        }
    }
}