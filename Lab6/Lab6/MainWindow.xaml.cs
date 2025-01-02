using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
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

                string data = $"{n},{min},{max}";
                
                LogToFile(data);
                
                string object2Path = Path.Combine("D:\\OOPLabs\\Lab6\\Object2\\bin\\Debug\\net9.0-windows\\Object2.exe");
                if (File.Exists(object2Path))
                {
                    _process2 ??= Process.Start(object2Path);
                }
                else
                {
                    MessageBox.Show("Не знайдено Object2.exe.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                await SendDataThroughNamedPipeAsync("Lab6ToObject2", data);
                
                string object3Path = Path.Combine("D:\\OOPLabs\\Lab6\\Object3\\bin\\Debug\\net9.0-windows\\Object3.exe");
                if (File.Exists(object3Path))
                {
                    _process3 ??= Process.Start(object3Path);
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
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _process2?.CloseMainWindow();
            _process3?.CloseMainWindow();
        }
        
        private async Task SendDataThroughNamedPipeAsync(string pipeName, string data)
        {
            await using var pipeClient = new NamedPipeClientStream(".", pipeName, PipeDirection.Out);
            try
            {
                await pipeClient.ConnectAsync(3000);
                await using var writer = new StreamWriter(pipeClient);
                writer.AutoFlush = true;
                await writer.WriteLineAsync(data);
            }
            catch (Exception ex)
            {
                LogToFile($"Помилка передачі даних: {ex.Message}");
            }
        }
        
        private void LogToFile(string message)
        {
            string logFilePath = "logLab6.txt";
            using StreamWriter writer = new StreamWriter(logFilePath, append: true);
            writer.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}
