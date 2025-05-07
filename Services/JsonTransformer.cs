using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSetConverter.Services;
public class JsonTransformer : IDataTransformer {
    public bool CanHandle(string extension) {
        return extension.Equals(".json", StringComparison.OrdinalIgnoreCase);
    }

    public List<Dictionary<string, object>> Load(string path) {
        var content = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(content);
    }

    public void Save(List<Dictionary<string, object>> records, string path) {
        var json = JsonConvert.SerializeObject(records, Formatting.Indented);
        File.WriteAllText(path, json);
    }
}
