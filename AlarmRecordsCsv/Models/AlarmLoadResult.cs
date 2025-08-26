using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmRecordsCsv.Models
{
    public sealed class AlarmLoadResult
    {
        public Dictionary<string, List<AlarmRecord>> GroupsByAlarm { get; init; } =
            new(StringComparer.OrdinalIgnoreCase);

        public IReadOnlyList<AlarmRecord> Records { get; init; } = Array.Empty<AlarmRecord>();

        // Useful prebuilt text output (like your dictDump)
        public string DictionaryDump { get; init; } = string.Empty;
    }
}
