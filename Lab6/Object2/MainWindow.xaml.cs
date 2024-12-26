using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Windows;

namespace Object2
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            StartNamedPipeServer();
        }

        private async void StartNamedPipeServer()
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    try
                    {
                        await using var pipeServer = new NamedPipeServerStream("Lab6ToObject2", PipeDirection.In);
                        await pipeServer.WaitForConnectionAsync();
                        
                        using var reader = new StreamReader(pipeServer);
                        string? data = await reader.ReadLineAsync();
                        
                        if (data != null)
                        {
                            Application.Current.Dispatcher.Invoke(() => OnDataReceived(data));
                        }
                    }
                    catch (Exception ex)
                    {
                        LogToFile($"Помилка з'єднання: {ex.Message}");
                    }
                }
            });
        }
        
        private void OnDataReceived(string data)
        {
            try
            {
                int[,] matrix = ParseMatrixFromString(data);
                string value = DisplayMatrix(matrix);
                Clipboard.SetText(value);
                LogToFile($"Вхідні дані: {data}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка обробки даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int[,] ParseMatrixFromString(string data)
        {
            string[] parameters = data.Split(',');

            if (parameters.Length != 3)
            {
                throw new FormatException("Рядок повинен містити три параметри: n, min, max.");
            }

            int n = int.Parse(parameters[0].Trim());
            int min = int.Parse(parameters[1].Trim());
            int max = int.Parse(parameters[2].Trim());

            if (min > max)
            {
                throw new ArgumentException("Мінімальне значення не може бути більше максимального.");
            }

            int[,] matrix = new int[n, n];
            Random rand = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = rand.Next(min, max + 1);
                }
            }

            return matrix;
        }

        private string DisplayMatrix(int[,] matrix)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    sb.Append(matrix[i, j].ToString().PadRight(5));
                }
                sb.AppendLine();
            }
            string serializedMatrix = SerializeMatrix(matrix);
            LogToFile(serializedMatrix);
            MatrixDisplayTextBox.Text = sb.ToString();
            return serializedMatrix;
        }
        
        private static string SerializeMatrix(int[,] matrix)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    sb.Append(matrix[i, j]);
                    if (j < matrix.GetLength(1) - 1)
                        sb.Append(',');
                }
                if (i < matrix.GetLength(0) - 1)
                    sb.AppendLine();
            }
            return sb.ToString();
        }
        
        private void LogToFile(string message)
        {
            string logFilePath = "logObject2.txt";
            using StreamWriter writer = new StreamWriter(logFilePath, append: true);
            writer.WriteLine($"{DateTime.Now}: {message}");
        }
    }
}
