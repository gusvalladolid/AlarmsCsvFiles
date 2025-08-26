using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlarmRecordsCsv.Models
{
    // CSV columns (semicolon-delimited), in order:
    // 0: ALARM NUMBER
    // 1: DATE  (start)
    // 2: TIME  (start)
    // 3: DATE  (end)
    // 4: TIME  (end)
    // 5: FAULT TIME (s)
    public sealed class AlarmRecordMap : ClassMap<AlarmRecord>
    {
        public AlarmRecordMap()
        {
            Map(m => m.AlarmNumber).Index(0);

            Map(m => m.StartTime).Convert(ctx =>
            {
                var d = ctx.Row.GetField(1);
                var t = ctx.Row.GetField(2);
                return TryParseDateTime(d, t);
            });

            Map(m => m.EndTime).Convert(ctx =>
            {
                var d = ctx.Row.GetField(3);
                var t = ctx.Row.GetField(4);
                return TryParseDateTime(d, t);
            });

            Map(m => m.FaultTimeSeconds).Convert(ctx =>
                int.TryParse(ctx.Row.GetField(5), out var s) ? s : (int?)null);
        }

        private static DateTime? TryParseDateTime(string? date, string? time)
        {
            if (string.IsNullOrWhiteSpace(date) || string.IsNullOrWhiteSpace(time))
                return null;

            var combined = $"{date} {time}";
            return DateTime.TryParseExact(
                combined, "yyyy/MM/dd HH:mm:ss",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out var dt)
                ? dt : (DateTime?)null;
        }
    }
}
