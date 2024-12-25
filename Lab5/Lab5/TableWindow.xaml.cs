using System.Collections.ObjectModel;
using System.Windows;

namespace Lab5;

public partial class TableWindow : Window
{
    private ObservableCollection<ShapeData> ShapeCollection { get; set; }

    public TableWindow()
    {
        InitializeComponent();
        ShapeCollection = new ObservableCollection<ShapeData>();
        TableDataGrid.ItemsSource = ShapeCollection;
    }

    public void LoadData(IEnumerable<ShapeData> shapes)
    {
        ShapeCollection.Clear();  
        foreach (var shape in shapes)
        {
            ShapeCollection.Add(shape);
        }
    }
}