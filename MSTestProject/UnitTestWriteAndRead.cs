using MiJenner.ConfigUtils;
using System.IO;
using System.Text.RegularExpressions;

namespace MSTestProject
{
    [TestClass]
    public class JsonUserSettingsHandlerTests
    {
        [TestMethod]
        public void TestWriteAndReadSettings()
        {
            // Arrange
            string filePath = "test_settings.json";
            IUserSettingsHandler<Settings> settingsHandler = new JsonUserSettingsHandlerMS<Settings>(filePath);
            var expectedSettings = new Settings() { MyString = "test", MyInt = 42 };

            // Act
            bool writeSuccess = settingsHandler.TryWrite(expectedSettings);
            bool readSuccess = settingsHandler.TryRead(out Settings actualSettings);

            // Assert
            Assert.IsTrue(writeSuccess, "Writing settings should return true.");
            Assert.IsTrue(readSuccess, "Reading settings should return true.");
            Assert.AreEqual(expectedSettings.MyString, actualSettings.MyString, "MyString property should match.");
            Assert.AreEqual(expectedSettings.MyInt, actualSettings.MyInt, "MyInt property should match.");

            // Clean up (delete the test file)
            File.Delete(filePath);
        }

        [TestMethod]
        public void TestReadEmptyFile()
        {
            // Reading from an empty file should return default values. 

            // Arrange
            IUserSettingsHandler<Settings> settingsHandler = new JsonUserSettingsHandlerMS<Settings>("empty.json");
            var expectedSettings = new Settings(); // with default values. 

            // Act
            bool readSuccess = settingsHandler.TryRead(out Settings actualSettings);

            // Assert
            Assert.IsTrue(readSuccess, "Reading settings should return false.");
            Assert.AreEqual(expectedSettings.MyString, actualSettings.MyString, "MyString property should match.");
            Assert.AreEqual(expectedSettings.MyInt, actualSettings.MyInt, "MyInt property should match.");
            Assert.AreEqual(expectedSettings.MyBool, actualSettings.MyBool, "MyInt property should match.");
            Assert.AreEqual(expectedSettings.MyDouble, actualSettings.MyDouble, "MyInt property should match.");

            // Clean up (delete the test file)

        }

        [TestMethod]
        public void TestOverwriteExistingSettings()
        {
            // Arrange
            string testFilePath = "dummy.json";
            // Create an initial JSON file with some data
            string initialJson = "{\"MyString\":\"InitialValue\",\"MyInt\":42}";
            File.WriteAllText(testFilePath, initialJson);

            // Instantiate the handler with the test file path
            IUserSettingsHandler<Settings> settingsHandler = new JsonUserSettingsHandlerMS<Settings>(testFilePath);

            // Create new settings to overwrite the existing data
            var newSettings = new Settings { MyString = "NewValue", MyInt = 99 };

            // Act
            bool writeSuccess = settingsHandler.TryWrite(newSettings);

            // Assert
            Assert.IsTrue(writeSuccess, "Writing settings should return true.");

            // Check that the existing JSON file has been overwritten
            string updatedJson = File.ReadAllText(testFilePath);
            string expectedJson = "{\"MyString\":\"NewValue\",\"MyInt\":99,\"MyBool\":false,\"MyDouble\":3.1425}";
            // Note: expected is specified values and defaults. 
            // remove linebreaks and white space. 
            updatedJson = Regex.Replace(updatedJson, @"\s+", "");
            expectedJson = Regex.Replace(expectedJson, @"\s+", "");

            Assert.AreEqual(expectedJson, updatedJson, "JSON contents should match the new settings.");

            // Clean up (delete the test file)
            File.Delete(testFilePath);

        }


        [TestMethod]
        public void TestReadFromNonExistentFile()
        {
            // Arrange
            string nonExistentFilePath = "non_existent_settings.json";
            // Ensure that the file does not exist
            if (File.Exists(nonExistentFilePath))
            {
                File.Delete(nonExistentFilePath);
            }

            // Instantiate the handler with the non-existent file path
            IUserSettingsHandler<Settings> settingsHandler = new JsonUserSettingsHandlerMS<Settings>(nonExistentFilePath);

            // Act
            bool readSuccess = settingsHandler.TryRead(out Settings settings);

            // Assert
            Assert.IsFalse(readSuccess, "Reading from a non-existent file should return false.");
            // Compare the settings object with default values
            Settings defaultSettings = new Settings(); 
            Assert.AreEqual(defaultSettings.MyString, settings.MyString);
            Assert.AreEqual(defaultSettings.MyInt, settings.MyInt);
            Assert.AreEqual(defaultSettings.MyBool, settings.MyBool);
            Assert.AreEqual(defaultSettings.MyDouble, settings.MyDouble);
    }



        [TestMethod]
        public void TestReadWithTooManyKeys()
        {
            // Arrange
            string tooManyKeysFilePath = "too_many_keys_settings.json";
            // Create a JSON file with more keys than the settings object can handle
            string jsonWithTooManyKeys = "{\"MyString\":\"Value1\",\"MyInt\":42,\"ExtraKey1\":\"ExtraValue1\",\"ExtraKey2\":\"ExtraValue2\"}";
            File.WriteAllText(tooManyKeysFilePath, jsonWithTooManyKeys);

            // Instantiate the handler with the file path
            IUserSettingsHandler<Settings> settingsHandler = new JsonUserSettingsHandlerMS<Settings>(tooManyKeysFilePath);

            // Act
            bool readSuccess = settingsHandler.TryRead(out Settings settings);

            // Assert
            Assert.IsTrue(readSuccess, "Reading from a JSON file should return true.");
            Assert.IsNotNull(settings, "Settings object should not be null.");

            // Ensure that only the expected properties are populated in the settings object
            Assert.AreEqual("Value1", settings.MyString);
            Assert.AreEqual(42, settings.MyInt);
            // Compare the remaining settings object with default values
            Settings defaultSettings = new Settings();
            Assert.AreEqual(defaultSettings.MyBool, settings.MyBool);
            Assert.AreEqual(defaultSettings.MyDouble, settings.MyDouble);

        }


        [TestMethod]
        public void ReadInvalidJsonFile()
        {
            // Arrange
            string invalidJsonFilePath = "invalid_settings.json";
            // Create an invalid JSON file with syntax errors
            string invalidJson = "{\"MyString\":\"Value1\",\"MyInt\":42,";
            File.WriteAllText(invalidJsonFilePath, invalidJson);

            // Instantiate the handler with the file path
            IUserSettingsHandler<Settings> settingsHandler = new JsonUserSettingsHandlerMS<Settings>(invalidJsonFilePath);

            // Act
            bool readSuccess = settingsHandler.TryRead(out Settings settings);

            // Assert
            Assert.IsFalse(readSuccess, "Reading from an invalid JSON file should return false.");
            // Compare the settings object with default values
            Settings defaultSettings = new Settings();
            Assert.AreEqual(defaultSettings.MyString, settings.MyString);
            Assert.AreEqual(defaultSettings.MyInt, settings.MyInt);
            Assert.AreEqual(defaultSettings.MyBool, settings.MyBool);
            Assert.AreEqual(defaultSettings.MyDouble, settings.MyDouble);
        }

        [TestMethod]
        public void ReadCorruptedJsonFile()
        {
            // Arrange
            string corruptedJsonFilePath = "corrupted_settings.json";
            // Create a corrupted JSON file with invalid data types
            string corruptedJson = "{\"MyString\":42,\"MyInt\":\"Value\"}";
            File.WriteAllText(corruptedJsonFilePath, corruptedJson);

            // Instantiate the handler with the file path
            IUserSettingsHandler<Settings> settingsHandler = new JsonUserSettingsHandlerMS<Settings>(corruptedJsonFilePath);

            // Act
            bool readSuccess = settingsHandler.TryRead(out Settings settings);

            // Assert
            Assert.IsFalse(readSuccess, "Reading from a corrupted JSON file should return false.");
            // Compare the settings object with default values
            Settings defaultSettings = new Settings();
            Assert.AreEqual(defaultSettings.MyString, settings.MyString);
            Assert.AreEqual(defaultSettings.MyInt, settings.MyInt);
            Assert.AreEqual(defaultSettings.MyBool, settings.MyBool);
            Assert.AreEqual(defaultSettings.MyDouble, settings.MyDouble);
        }
    }
}
