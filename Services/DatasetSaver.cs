// DatasetSaver.cs
using DataSetConverter.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace DatasetTransformer.Services {
    public class DatasetSaver {
        private readonly List<IDataTransformer> transformers;

        public DatasetSaver() {
            transformers = new List<IDataTransformer>
            {
                new JsonTransformer(),
                new YamlTransformer(),
                new CsvTransformer()
            };
        }

        public void Save(List<Dictionary<string, object>> records, string path) {
            var ext = Path.GetExtension(path);
            foreach (var transformer in transformers) {
                if (transformer.CanHandle(ext)) {
                    transformer.Save(records, path);
                    return;
                }
            }

            throw new Exception($"Unsupported output format: {ext}");
        }
    }
}
