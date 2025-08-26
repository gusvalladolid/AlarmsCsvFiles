using AlarmRecordsCsv.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmRecordsCsv.Services
{
    public interface IAlarmCsvService
    {
        AlarmLoadResult LoadFromCsv(string filePath, string delimiter = ";");
    }

    public sealed class AlarmCsvService : IAlarmCsvService
    {
        public AlarmLoadResult LoadFromCsv(string filePath, string delimiter = ";")
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = true,
                MissingFieldFound = null,
                BadDataFound = null,
                HeaderValidated = null,
                Delimiter = delimiter,
                IgnoreBlankLines = true,
                TrimOptions = TrimOptions.Trim
            };

            using var reader = new StreamReader(filePath);
            using var csv = new CsvReader(reader, config);

            // Map that fuses DATE + TIME into DateTime?
            csv.Context.RegisterClassMap<AlarmRecordMap>();

            // Read & parse
            csv.Read();
            csv.ReadHeader();
            var records = csv.GetRecords<AlarmRecord>().ToList();

            // Group
            const string NoAlarmNumberKey = "noAlarmNumber";
            var groups = new Dictionary<string, List<AlarmRecord>>(StringComparer.OrdinalIgnoreCase);
            foreach (var r in records)
            {
                // Example: treat as "noAlarmNumber" if null, empty, or not starting with '*'
                var key = string.IsNullOrWhiteSpace(r.AlarmNumber) || !r.AlarmNumber.StartsWith("*")
                    ? NoAlarmNumberKey
                    : r.AlarmNumber!;

                if (!groups.TryGetValue(key, out var list))
                {
                    list = new List<AlarmRecord>();
                    groups[key] = list;
                }
                list.Add(r);
            }

            
            var sb = new StringBuilder();
            foreach (var kvp in groups.OrderBy(g => g.Key))
            {
                sb.AppendLine($"Alarm Number Key: {kvp.Key}");
                foreach (var r in kvp.Value)
                {
                    var startStr = r.StartTime?.ToString("yyyy/MM/dd HH:mm:ss") ?? "-";
                    var endStr = r.EndTime?.ToString("yyyy/MM/dd HH:mm:ss") ?? "-";
                    sb.AppendLine($"   Start: {startStr}, End: {endStr}, Fault(s): {r.FaultTimeSeconds}");
                }
            }

            return new AlarmLoadResult
            {
                GroupsByAlarm = groups,
                Records = records,
                DictionaryDump = sb.ToString()
            };
        }
    }
}
