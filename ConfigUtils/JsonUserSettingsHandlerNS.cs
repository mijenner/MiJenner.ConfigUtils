using Newtonsoft.Json; 
using System; 
using System.IO; 

namespace MiJenner.ConfigUtils
{
    public class JsonUserSettingsHandlerNS<T> : IUserSettingsHandler<T>
    {
        private string filePath; // The file path to the JSON settings file

        public JsonUserSettingsHandlerNS(string initialFilePath)
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
                // Serialize and write settings to JSON file
                var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
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
                // Read and deserialize settings from JSON file
                var json = File.ReadAllText(filePath);
                if (!string.IsNullOrEmpty(json))
                {
                    settings = JsonConvert.DeserializeObject<T>(json);
                } else
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
