using System.Windows;
using System.Windows.Input;
using Lab3.Abstraction;
using Lab3.Editors;

namespace Lab3;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private Editor _currentEditor;
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