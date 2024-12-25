using System.Globalization;
using System.Windows;

namespace Object3
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
                clipboardText = clipboardText.Replace("\\r\\n", "\r\n");
                int[,] matrix = DeserializeMatrix(clipboardText);
                double determinant = CalculateDeterminant(matrix);
                DeterminantTextBox.Text = determinant.ToString(CultureInfo.CurrentCulture);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка обновления: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private static double CalculateDeterminant(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            if (n == 1)
                return matrix[0, 0];

            double det = 0;
            for (int p = 0; p < n; p++)
            {
                int[,] subMatrix = new int[n - 1, n - 1];
                for (int i = 1; i < n; i++)
                {
                    int colIndex = 0;
                    for (int j = 0; j < n; j++)
                    {
                        if (j == p)
                            continue;
                        subMatrix[i - 1, colIndex] = matrix[i, j];
                        colIndex++;
                    }
                }
                det += matrix[0, p] * Math.Pow(-1, p) * CalculateDeterminant(subMatrix);
            }
            return det;
        }
        
        private static int[,] DeserializeMatrix(string input)
        {
            var rows = input.Split(new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            
            int rowCount = rows.Length;
            int columnCount = rows[0].Split(',').Length;
            
            int[,] matrix = new int[rowCount, columnCount];
            
            for (int i = 0; i < rowCount; i++)
            {
                var values = rows[i].Split(',');
                for (int j = 0; j < columnCount; j++)
                {
                    matrix[i, j] = int.Parse(values[j]);
                }
            }

            return matrix;
        }

    }
}
