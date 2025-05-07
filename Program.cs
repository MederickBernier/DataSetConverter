// Program.cs
using System;
using DatasetTransformer.Services;

namespace DatasetTransformer {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("=== Dataset Transformer CLI ===");

            if (args.Length < 2) {
                Console.WriteLine("Usage: dotnet run -- --input input.json --output output.csv");
                return;
            }

            string? inputPath = null;
            string? outputPath = null;

            for (int i = 0; i < args.Length; i++) {
                if (args[i] == "--input" && i + 1 < args.Length)
                    inputPath = args[++i];
                if (args[i] == "--output" && i + 1 < args.Length)
                    outputPath = args[++i];
            }

            if (string.IsNullOrWhiteSpace(inputPath) || string.IsNullOrWhiteSpace(outputPath)) {
                Console.WriteLine("Missing required arguments.");
                return;
            }

            try {
                var loader = new DatasetLoader();
                var saver = new DatasetSaver();

                var records = loader.Load(inputPath);
                if (records == null || records.Count == 0) {
                    Console.WriteLine("No data found in input.");
                    return;
                }

                saver.Save(records, outputPath);
            } catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
