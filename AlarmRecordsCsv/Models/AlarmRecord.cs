using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmRecordsCsv.Models
{
    internal class AlarmRecord
    {
        [Name("ALARM NUMBER")]
        public string? AlarmNumber { get; set; }   // e.g., "*703116" or blank on short/system rows
        [Name("DATE")]
        public string? StartDate { get; set; }     // "yyyy/MM/dd"
        [Name("TIME")]
        public string? StartTime { get; set; }     // "HH:mm:ss"
        [Name("DATE")]
        public string? EndDate { get; set; }       // may be empty on short rows
        [Name("TIME")]
        public string? EndTime { get; set; }       // may be empty on short rows
        [Name("FAULT TIME (s)")]
        public string? FaultTimeSeconds { get; set; } // "1696" etc
    }


    /*
    internal class AlarmRecord
    {
        [Name("ALARM NUMBER")]
        public string? AlarmNumber { get; set; }   // e.g., "*703116" or blank on short/system rows
        [Name("DATE")]
        public string? StartDate { get; set; }     // "yyyy/MM/dd"
        [Name("TIME")]
        public string? StartTime { get; set; }     // "HH:mm:ss"
        [Name("DATE")]
        public string? EndDate { get; set; }       // may be empty on short rows
        [Name("TIME")]
        public string? EndTime { get; set; }       // may be empty on short rows
        [Name("FAULT TIME (s)")]
        public string? FaultTimeSeconds { get; set; } // "1696" etc
    }
    */

}
