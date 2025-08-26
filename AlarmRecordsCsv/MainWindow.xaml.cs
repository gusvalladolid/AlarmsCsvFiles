using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Win32;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AlarmRecordsCsv.Services;

namespace AlarmRecordsCsv
{
    public partial class MainWindow : Window
    {
        private readonly IFileDialogService _fileDialog;
        private readonly IAlarmCsvService _csvService;

        public MainWindow()
        {
            InitializeComponent();

            // Simple manual wiring; swap for DI later if you like
            _fileDialog = new FileDialogService();
            _csvService = new AlarmCsvService();
        }

        private void UploadFile_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var filePath = _fileDialog.PickCsvFile();
                if (string.IsNullOrWhiteSpace(filePath)) return;

                var result = _csvService.LoadFromCsv(filePath);

                // Example: push the pretty dump to a TextBox named tbInfo
                tbInfo.Text = result.DictionaryDump;

                // You also have strong data you can bind/use:
                // result.GroupsByAlarm and result.Records
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading file: {ex.Message}", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}