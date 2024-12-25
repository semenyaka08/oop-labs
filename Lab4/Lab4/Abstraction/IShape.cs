using System.Windows;
using System.Windows.Controls;

namespace Lab4.Abstraction;

public interface IShape
{
    public void Draw(Canvas canvas);
    
    public void Update(Point currentPoint);
}