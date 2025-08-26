using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmRecordsCsv.Models
{
    // Combined model: StartTime and EndTime are DATE+TIME fused into DateTime?
    public sealed class AlarmRecord
    {
        public string? AlarmNumber { get; set; }   // e.g., "*703116"
        public DateTime? StartTime { get; set; }   // yyyy/MM/dd HH:mm:ss (nullable)
        public DateTime? EndTime { get; set; }   // yyyy/MM/dd HH:mm:ss (nullable)
        public int? FaultTimeSeconds { get; set; }   // parsed int (nullable)
    }
}
