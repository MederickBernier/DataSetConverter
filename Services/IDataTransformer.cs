using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSetConverter.Services;
public interface IDataTransformer {
    List<Dictionary<string, object>> Load(string path);
    void Save(List<Dictionary<string, object>> records, string path);
    bool CanHandle(string extension);
}
