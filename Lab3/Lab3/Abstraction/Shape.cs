using System.Windows;
using System.Windows.Controls;

namespace Lab3.Abstraction;

public abstract class Shape
{
    public abstract void Draw(Canvas canvas);
    
    public abstract void Update(Point currentPoint);
}