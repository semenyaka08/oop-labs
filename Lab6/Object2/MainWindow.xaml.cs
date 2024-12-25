using System.Text;
using System.Windows;

namespace Object2
{
    public partial class MainWindow : Window
    {
        private string _lastClipboardText = string.Empty;
        
        public MainWindow()
        {
            InitializeComponent();
            StartClipboardWatcher();
        }

        private void StartClipboardWatcher()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    await Task.Delay(500); 
                    string clipboardText = string.Empty;

                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (Clipboard.ContainsText())
                        {
                            clipboardText = Clipboard.GetText();
                        }
                    });

                    if (clipboardText != _lastClipboardText)
                    {
                        _lastClipboardText = clipboardText;
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            OnClipboardUpdated(clipboardText);
                        });
                    }
                }
            });
        }
        
        private void OnClipboardUpdated(string clipboardText)
        {
            try
            {
                int[,] matrix = ParseMatrixFromString(clipboardText);
                DisplayMatrix(matrix);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private int[,] ParseMatrixFromString(string matrixString)
        {
            string[] parameters = matrixString.Split(',');

            // Перевіряємо, чи маємо три параметри
            if (parameters.Length != 3)
            {
                throw new FormatException("Рядок повинен містити три параметри: n, min, max.");
            }

            // Парсимо параметри
            int n = int.Parse(parameters[0].Trim());
            int min = int.Parse(parameters[1].Trim());
            int max = int.Parse(parameters[2].Trim());

            // Перевіряємо, чи min не більше max
            if (min > max)
            {
                throw new ArgumentException("Мінімальне значення не може бути більше максимального.");
            }

            // Генеруємо матрицю
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

        private void DisplayMatrix(int[,] matrix)
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
            Clipboard.SetText(serializedMatrix);
            _lastClipboardText = serializedMatrix;
            MatrixDisplayTextBox.Text = sb.ToString();
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
    }
}
