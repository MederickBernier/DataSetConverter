# Dataset Transformer CLI

A lightweight, modular command-line tool written in C# that converts structured datasets between **JSON**, **YAML**, and **CSV** formats.

Supports format detection, type inference, and extensible architecture using the `IDataTransformer` interface.

---

## 🚀 Features
- Convert between JSON, YAML, and CSV
- Automatic type normalization (e.g., "true" → `true`, "123" → `123`)
- Pluggable architecture via interface-based transformers
- CLI usage with simple input/output arguments

---

## 🧰 Requirements
- .NET 8 SDK
- NuGet packages:
  - `Newtonsoft.Json`
  - `YamlDotNet`

---

## 📦 Installation
Clone the repository and restore packages:
```bash
cd DatasetTransformer
dotnet restore
```

---

## 🛠 Usage
```bash
dotnet run -- --input input.json --output output.csv
```

### 🧪 Example Commands
- `dotnet run -- --input input.json --output output.yaml`
- `dotnet run -- --input input.yaml --output output.json`
- `dotnet run -- --input input.csv --output output.json`

Make sure files are in the project directory or use full paths.

---

## 📂 File Structure
```
DatasetTransformer/
├── Program.cs
├── Services/
│   ├── IDataTransformer.cs
│   ├── JsonTransformer.cs
│   ├── YamlTransformer.cs
│   ├── CsvTransformer.cs
│   ├── DatasetLoader.cs
│   └── DatasetSaver.cs
└── README.md
```

---

## 🔄 Supported Conversions
| From | To | ✅ |
|------|----|----|
| JSON | CSV | ✅ |
| JSON | YAML | ✅ |
| YAML | JSON | ✅ |
| YAML | CSV | ✅ |
| CSV | JSON | ✅ |
| CSV | YAML | ✅ |

---

## 📘 License
MIT License. Feel free to modify and use for personal or professional projects.
