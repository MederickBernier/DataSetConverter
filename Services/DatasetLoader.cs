// DatasetLoader.cs
using DataSetConverter.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace DatasetTransformer.Services {
    public class DatasetLoader {
        private readonly List<IDataTransformer> transformers;

        public DatasetLoader() {
            transformers = new List<IDataTransformer>
            {
                new JsonTransformer(),
                new YamlTransformer(),
                new CsvTransformer()
            };
        }

        public List<Dictionary<string, object>> Load(string path) {
            var ext = Path.GetExtension(path);
            foreach (var transformer in transformers) {
                if (transformer.CanHandle(ext)) {
                    return transformer.Load(path);
                }
            }

            throw new Exception($"Unsupported input format: {ext}");
        }
    }
}
