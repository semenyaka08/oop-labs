using System.Windows;
using System.Windows.Input;
using Lab5.Services;
using Lab5.Shapes;

namespace Lab5;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    private readonly Editor _currentEditor = Editor.Instance;
    private bool _isDrawing;
    private TableWindow? _tableWindow;
    private readonly List<ShapeData> _shapesData = [];
    private string _currentShapeName = string.Empty;
    private ShapeData? _shapeData;
    private readonly DataService _dataService;
    
    public MainWindow()
    {
        InitializeComponent();
        _dataService = new DataService();
    }
    
    private void OnOpenTableClick(object sender, RoutedEventArgs e)
    {
        if (_tableWindow == null || !_tableWindow.IsVisible)
        {
            _tableWindow = new TableWindow();
            _tableWindow.Closed += (s, e) => _tableWindow = null;
            _tableWindow.Show();
        }
        else
        {
            _tableWindow.Activate(); 
        }
        _tableWindow.LoadData(_shapesData);
    }
    
    private void UpdateSelectedShapeMenu(string shapeName)
    {
        SelectedShapeMenuItem.Header = $"Вибрано: {shapeName}";
    }
    
    private void OnLineClick(object sender, RoutedEventArgs e)
    {
        _currentEditor.SetShapeFactory(startPoint => new LineShape(startPoint));
        _currentShapeName = "Лінія";
        UpdateSelectedShapeMenu("Лінія");
    }

    private void OnRectangleClick(object sender, RoutedEventArgs e)
    {
        _currentEditor.SetShapeFactory(startPoint => new RectangleShape(startPoint));
        _currentShapeName = "Прямокутник";
        UpdateSelectedShapeMenu("Прямокутник");
    }

    private void OnEllipseClick(object sender, RoutedEventArgs e)
    {
        _currentEditor.SetShapeFactory(startPoint => new EllipseShape(startPoint));
        _currentShapeName = "Еліпс";
        UpdateSelectedShapeMenu("Еліпс");
    }
    
    private void OnPointClick(object sender, RoutedEventArgs e)
    {
        _currentEditor.SetShapeFactory(startPoint => new PointShape(startPoint));
        _currentShapeName = "Точка";
        UpdateSelectedShapeMenu("Точка");
    }
    
    private void OnCubeClick(object sender, RoutedEventArgs e)
    {
        _currentEditor.SetShapeFactory(startPoint => new CubeShape(startPoint));
        _currentShapeName = "Куб";
        UpdateSelectedShapeMenu("Куб");
    }
    
    private void OnLineWithCirclesClick(object sender, RoutedEventArgs e)
    {
        _currentEditor.SetShapeFactory(startPoint => new LineWithCircles(startPoint));
        _currentShapeName = "Лінія з кружечками на кінцях";
        UpdateSelectedShapeMenu("Лінія з кружечками на кінцях");
    }
    
    private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        _isDrawing = true;
        Point startPoint = e.GetPosition(DrawingCanvas);
        _currentEditor.StartDrawing(startPoint, DrawingCanvas);
        _shapeData = new ShapeData{Name = _currentShapeName, X1 = (int)startPoint.X, Y1 = (int)startPoint.Y};
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
            _shapeData!.X2 = (int)endPoint.X;
            _shapeData!.Y2 = (int)endPoint.Y;
            _shapesData.Add(_shapeData);
            _currentEditor.FinishDrawing(endPoint);
            _tableWindow?.LoadData(_shapesData);
            _dataService.SaveDataToCsv(_shapesData);
            _isDrawing = false;
        }
    }
}