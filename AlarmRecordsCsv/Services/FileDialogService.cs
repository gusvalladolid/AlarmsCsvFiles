using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace AlarmRecordsCsv.Services
{
    public interface IFileDialogService
    {
        string? PickCsvFile();
    }

    public sealed class FileDialogService : IFileDialogService
    {
        public string? PickCsvFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Title = "Select a CSV File",
                Filter = "CSV files (*.csv)|*.csv",
                DefaultExt = ".csv",
                CheckFileExists = true,
                ValidateNames = true
            };
            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }
    }
}