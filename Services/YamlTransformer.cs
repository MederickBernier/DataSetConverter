// YamlTransformer.cs
using DataSetConverter.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace DatasetTransformer.Services {
    public class YamlTransformer : IDataTransformer {
        public bool CanHandle(string extension) {
            return extension.Equals(".yaml", StringComparison.OrdinalIgnoreCase) ||
                   extension.Equals(".yml", StringComparison.OrdinalIgnoreCase);
        }

        public List<Dictionary<string, object>> Load(string path) {
            var content = File.ReadAllText(path);
            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var rawRecords = deserializer.Deserialize<List<Dictionary<string, object>>>(content);
            var records = new List<Dictionary<string, object>>();

            foreach (var record in rawRecords) {
                var normalized = new Dictionary<string, object>();
                foreach (var kvp in record) {
                    var val = kvp.Value?.ToString()?.Trim();
                    if (val == null) {
                        normalized[kvp.Key] = "";
                    } else if (bool.TryParse(val, out var b)) {
                        normalized[kvp.Key] = b;
                    } else if (int.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out var i)) {
                        normalized[kvp.Key] = i;
                    } else if (double.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out var d)) {
                        normalized[kvp.Key] = d;
                    } else {
                        normalized[kvp.Key] = val;
                    }
                }
                records.Add(normalized);
            }

            return records;
        }

        public void Save(List<Dictionary<string, object>> records, string path) {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var yaml = serializer.Serialize(records);
            File.WriteAllText(path, yaml);
        }
    }
}
