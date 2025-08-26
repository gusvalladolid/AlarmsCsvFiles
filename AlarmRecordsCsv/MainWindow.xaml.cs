using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Win32;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AlarmRecordsCsv
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void UploadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Select a CSV File",
                Filter = "CSV files (*.csv)|*.csv",
                DefaultExt = ".csv",
                CheckFileExists = true,
                ValidateNames = true,
                
            };
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                try
                {
                    /*
                    string[] lines = System.IO.File.ReadAllLines(filePath, Encoding.UTF8);
                    StringBuilder content = new StringBuilder();
                    foreach (string line in lines)
                    {
                        content.AppendLine(line);
                    }
                    Debug.WriteLine("CONTENTTTTTTTTTTT" + content.ToString());
                    tbInfo.Text = content.ToString();*/

                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        HasHeaderRecord = true, // Assuming the CSV has a header row
                        MissingFieldFound = null, // Ignore missing fields
                        BadDataFound = null, // Ignore bad data
                        HeaderValidated = null, // Ignore header validation
                        Delimiter = ";",
                    };
                    using (var reader = new StreamReader(filePath))
                    using (var csv = new CsvHelper.CsvReader(reader, config))
                    {
                        Dictionary<string, List<Models.AlarmRecord>> alarmsDictionary = new Dictionary<string, List<Models.AlarmRecord>>();
                        // Read the CSV file
                        csv.Read();
                        csv.ReadHeader();
                        var records = csv.GetRecords<Models.AlarmRecord>().ToList();
                        // Display the records in the TextBox
                        StringBuilder content = new StringBuilder();
                        foreach (var record in records)
                        {
                            if (alarmsDictionary.ContainsKey(record.AlarmNumber))
                            {
                                alarmsDictionary[record.AlarmNumber] = new List<Models.AlarmRecord>();
                            }
                            content.AppendLine($"Alarm Number: {record.AlarmNumber}, Start Time: {record.StartTime}, End Time: {record.EndTime}, Fault Time: {record.FaultTimeSeconds}");
                            //Debug.WriteLine(content);
                        }
                        //Debug.WriteLine("ACCESS RECORD INFO"+ records[0].AlarmNumber);
                        tbInfo.Text = content.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error reading file: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}