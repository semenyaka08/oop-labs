using System.IO;
using System.Text;

namespace Lab5.Services;

public class DataService
{
    public void SaveDataToCsv(IEnumerable<ShapeData> shapes)
    {
        var sb = new StringBuilder();
        
        sb.AppendLine("Назва, x1, y1, x2, y2");
        
        foreach (var shape in shapes)
        {
            sb.AppendLine($"{shape.Name}, {shape.X1}, {shape.Y1}, {shape.X2}, {shape.Y2}");
        }

        const string filePath = @"D:\OOPLabs\Lab5\Lab5\shapes_data.csv";
        
        File.WriteAllText(filePath, sb.ToString());
    }
}