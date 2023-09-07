using MiJenner.ConfigUtils;
using System.IO;

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
            IUserSettingsHandler<Settings> settingsHandler = new JsonUserSettingsHandlerNS<Settings>(filePath);
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
            IUserSettingsHandler<Settings> settingsHandler = new JsonUserSettingsHandlerNS<Settings>("empty.json");
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
    }
}
