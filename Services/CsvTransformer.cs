// CsvTransformer.cs
using DataSetConverter.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace DatasetTransformer.Services {
    public class CsvTransformer : IDataTransformer {
        public bool CanHandle(string extension) {
            return extension.Equals(".csv", StringComparison.OrdinalIgnoreCase);
        }

        public List<Dictionary<string, object>> Load(string path) {
            var lines = File.ReadAllLines(path);
            if (lines.Length < 2) {
                throw new Exception("CSV must have at least a header and one data row.");
            }

            var headers = lines[0].Split(',');
            var records = new List<Dictionary<string, object>>();

            for (int i = 1; i < lines.Length; i++) {
                var values = lines[i].Split(',');
                var dict = new Dictionary<string, object>();

                for (int j = 0; j < headers.Length && j < values.Length; j++) {
                    var val = values[j].Trim();
                    if (bool.TryParse(val, out var b))
                        dict[headers[j]] = b;
                    else if (int.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out var iVal))
                        dict[headers[j]] = iVal;
                    else if (double.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out var dVal))
                        dict[headers[j]] = dVal;
                    else
                        dict[headers[j]] = val;
                }

                records.Add(dict);
            }

            return records;
        }

        public void Save(List<Dictionary<string, object>> records, string path) {
            var sb = new StringBuilder();
            var headers = new List<string>(records[0].Keys);
            sb.AppendLine(string.Join(",", headers));

            foreach (var record in records) {
                var row = new List<string>();
                foreach (var header in headers) {
                    record.TryGetValue(header, out var value);
                    row.Add(value?.ToString()?.Replace(",", " ") ?? "");
                }
                sb.AppendLine(string.Join(",", row));
            }

            File.WriteAllText(path, sb.ToString());
        }
    }
}
