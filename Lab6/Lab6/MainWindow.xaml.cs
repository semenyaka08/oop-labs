using System.Diagnostics;
using System.IO;
using System.Windows;

namespace Lab6
{
    public partial class MainWindow : Window
    {
        private Process? _process2;
        private Process? _process3;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void ExecuteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int n = int.Parse(MatrixSizeTextBox.Text);
                int min = int.Parse(MinValueTextBox.Text);
                int max = int.Parse(MaxValueTextBox.Text);

                if (min > max)
                {
                    MessageBox.Show("Мінімальне значення не може бути більше максимального.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string clipboardData = $"{n},{min},{max}";
                
                Clipboard.SetText(clipboardData);
                
                // Запуск Object2
                string object2Path = Path.Combine("D:\\OOPLabs\\Lab6\\Object2\\bin\\Debug\\net9.0-windows\\Object2.exe");
                if (File.Exists(object2Path))
                {
                    if (_process2 == null)
                    {
                        _process2 = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = object2Path,
                                UseShellExecute = true
                            }
                        };
                        _process2.Start();
                        _process2.EnableRaisingEvents = true;
                        _process2.Exited += (s, args) => _process2 = null;
                    }
                }
                else
                {
                    MessageBox.Show("Не знайдено Object2.exe.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                await Task.Delay(500);
                
                Console.WriteLine("********");
                Console.WriteLine(Clipboard.GetText());
                
                
                string object3Path = Path.Combine("D:\\OOPLabs\\Lab6\\Object3\\bin\\Debug\\net9.0-windows\\Object3.exe");
                if (File.Exists(object3Path))
                {
                    string clipText = Clipboard.GetText();
                    Console.WriteLine($"Clipboard text within object3:");
                    Console.Write(clipText);
                    
                    if (_process3 == null)
                    {
                        _process3 = new Process
                        {
                            StartInfo = new ProcessStartInfo
                            {
                                FileName = object3Path,
                                UseShellExecute = true
                            }
                        };
                        _process3.Start();
                        _process3.EnableRaisingEvents = true;
                        _process3.Exited += (s, args) => _process3 = null;
                    }
                    
                }
                else
                {
                    MessageBox.Show("Не знайдено Object3.exe.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Будь ласка, введіть коректні числові значення.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сталася помилка: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
