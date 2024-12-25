using System.Windows;
using System.Windows.Input;
using Lab2.Abstraction;
using Lab2.Editors;

namespace Lab2;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private BaseEditor _currentEditor;
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
        _currentEditor = new LineEditor();
        UpdateSelectedShapeMenu("Лінія");
    }

    private void OnRectangleClick(object sender, RoutedEventArgs e)
    {
        _currentEditor = new RectangleEditor();
        UpdateSelectedShapeMenu("Прямокутник");
    }

    private void OnEllipseClick(object sender, RoutedEventArgs e)
    {
        _currentEditor = new EllipseEditor();
        UpdateSelectedShapeMenu("Еліпс");
    }
    
    private void OnPointClick(object sender, RoutedEventArgs e)
    {
        _currentEditor = new PointEditor();
        UpdateSelectedShapeMenu("Точка");
    }
    
    private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        _isDrawing = true;
        Point startPoint = e.GetPosition(drawingCanvas);
        _currentEditor.StartDrawing(startPoint, drawingCanvas);
    }

    private void Canvas_MouseMove(object sender, MouseEventArgs e)
    {
        if (!_isDrawing)
            return;

        Point currentPosition = e.GetPosition(drawingCanvas);
        _currentEditor.UpdateDrawing(currentPosition);
    }

    private void Canvas_MouseUp(object sender, MouseButtonEventArgs e)
    {
        if (_isDrawing)
        {
            Point endPoint = e.GetPosition(drawingCanvas);
            _currentEditor.FinishDrawing(endPoint);
            _isDrawing = false;
        }
    }
}