using System;
using System.IO;
using System.Text.Json;

namespace MiJenner.ConfigUtils
{
    public class JsonUserSettingsHandlerMS<T> : IUserSettingsHandler<T>
    {
        private string filePath; // The file path to the JSON settings file

        public JsonUserSettingsHandlerMS(string initialFilePath)
        {
            this.filePath = initialFilePath;
        }

        // Property to get or set the file path
        public string FilePath
        {
            get => filePath;
            set => filePath = value;
        }

        public bool TryWrite(T settings)
        {
            try
            {
                // Serialize and write settings to JSON file using System.Text.Json
                var json = JsonSerializer.Serialize(settings, new JsonSerializerOptions
                {
                    WriteIndented = true // Formatting option to make it indented
                });
                File.WriteAllText(filePath, json);
                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions and return false
                Console.WriteLine($"Failed to write settings: {ex.Message}");
                return false;
            }
        }

        public bool TryRead(out T settings)
        {
            try
            {
                // Read and deserialize settings from JSON file using System.Text.Json
                var json = File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(json))
                {
                    settings = JsonSerializer.Deserialize<T>(json);
                }
                else
                {
                    settings = Activator.CreateInstance<T>();
                }
                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions and return false
                Console.WriteLine($"Failed to read settings: {ex.Message}");
                settings = default(T);
                return false;
            }
        }
    }
}
