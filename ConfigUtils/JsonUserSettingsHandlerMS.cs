using System;
using System.IO;
using System.Text.Json;

namespace MiJenner.ConfigUtils
{
    /// <summary>
    /// Utility to handle configuration settings. 
    /// Before usage: 
    /// - Create a settings object (potentially with nested objects)
    /// - Determine full path of configuration file to use. 
    /// <code>
    /// Settings settings; 
    /// filepath = "config.json";
    /// IUserSettingsHandler<Settings> settingsHandler = new JsonUserSettingsHandlerMS<Settings>(filepath); 
    /// bool readSuccess = setthingsHandler.TryRead(out settings); 
    /// </code>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonUserSettingsHandlerMS<T> : IUserSettingsHandler<T>
    {
        private string filePath; // The file path to the JSON settings file

        /// <summary>
        /// Constructor for JsonUserSettingsHandlerMS. 
        /// It takes a full path including filename as input. 
        /// This can however be changed later through property FilePath: 
        /// <code>
        /// settingsHandler.FilePath = "newfile.json"; 
        /// </code>
        /// </summary>
        /// <param name="initialFilePath"></param>
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

        /// <summary>
        /// TryWrite tries to write a settings object to json file. 
        /// Syntax: 
        /// <code>
        /// bool writeSuccess = settingsHandler.TryWrite(settings); 
        /// </code>
        /// If successfull returns true, else false. 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
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

        /// <summary>
        /// TryRead tries to read a settings object to json file. 
        /// Syntax: 
        /// <code>
        /// bool readSuccess = settingsHandler.TryRead(out settings); 
        /// </code>
        /// If successfull returns true, else false. 
        /// In either case settings is populated, either with 
        /// valid data from json file, or with default values. 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
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
                    settings = GetDefaultSettings(); 
                }
                return true;
            }
            catch (Exception ex)
            {
                // Handle exceptions and return false
                Console.WriteLine($"Failed to read settings: {ex.Message}");
                settings = GetDefaultSettings(); 
                return false;
            }
        }

        // Method to load default settings
        private T GetDefaultSettings()
        {
            // Create and return an instance of the default settings
            return Activator.CreateInstance<T>();
        }
    }
}
