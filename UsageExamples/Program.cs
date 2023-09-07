using MiJenner.ConfigUtils;
using System;

namespace UsageExamples
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // First create a settings handler: 
            IUserSettingsHandler<Settings> settingsHandler = new JsonUserSettingsHandlerMS<Settings>("usersettings.json");

            // First test, write object properties to json file: 
            Settings settings = new Settings() { MyString = "set1", MyInt = 5, MyBool = true, MyDouble = 14.16 }; 
            if (settingsHandler.TryWrite(settings))
            {
                Console.WriteLine("Writing object to json file:" + settingsHandler.FilePath);
                Console.WriteLine("TryWrite returned true");
                Console.WriteLine("Object: ");
                settings.Print();
            }

            // Second test, read from json file created above to object: 
            Console.WriteLine("-------------------------------------------");
            Settings settingsb;
            if (settingsHandler.TryRead(out settingsb))
            {
                Console.WriteLine("Reading from above created json file:" + settingsHandler.FilePath + " to new object");
                Console.WriteLine("TryRead returned true");
                Console.WriteLine("Object read from JSON file:");
                settingsb.Print(); 
            }

            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("-------------------------------------------");
            // Second round of tests: Nesting settings can also be handled: 
            // Here we will use another object to store, this requires a new settings
            // handler. 
            IUserSettingsHandler<Settings2> settingsHandler2 = new JsonUserSettingsHandlerMS<Settings2>("usersettings2.json");

            Settings2 settings2 = new Settings2() 
            {
                KeyOne = 42,
                KeyTwo = true,
                KeyThree = new NestedSettings
                {
                    Message = "Hello, nested settings!"
                }
            };
            // Write object with nested object to file: 
            if (settingsHandler2.TryWrite(settings2))
            {
                Console.WriteLine("Writing from object (nested) to json file " + settingsHandler2.FilePath);
                Console.WriteLine("TryWrite returned true");
                Console.WriteLine("Object: ");
                settings2.Print();
            }

            // Read from json file (nested) to object (nested): 
            Console.WriteLine("-------------------------------------------");
            Settings2 settings2b;
            if (settingsHandler2.TryRead(out settings2b))
            {
                Console.WriteLine("Reading from above created json (nested) file:" + settingsHandler2.FilePath + " to new object (nested)");
                Console.WriteLine("TryRead returned true");
                Console.WriteLine("Object read from JSON file:");
                settings2b.Print();
            }

            // Third round of tests, from file with too much data: 
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("-------------------------------------------");
            settingsHandler.FilePath = "usersettings-more.json";
            Settings settings3;
            if (settingsHandler.TryRead(out settings3))
            {
                Console.WriteLine("Reading nested json file with too many props:" + settingsHandler.FilePath + " to new object (nested)");
                Console.WriteLine("TryRead returned true");
                Console.WriteLine("Object read from JSON file:");
                settings3.Print();
            }

            // with nested data: 
            settingsHandler2.FilePath = "usersettings2-more.json";
            Console.WriteLine("-------------------------------------------");
            Settings2 settings3b;
            if (settingsHandler2.TryRead(out settings3b))
            {
                Console.WriteLine("Reading from nested json file with too many props:" + settingsHandler2.FilePath + " to new object (nested)");
                Console.WriteLine("TryRead returned true");
                Console.WriteLine("Object read from JSON file:");
                settings3b.Print();
            }

            // Fourth round of tests, from file with too few keys: 
            Console.WriteLine("-------------------------------------------");
            Console.WriteLine("-------------------------------------------");
            settingsHandler.FilePath = "usersettings-less.json";
            Settings settings4;
            if (settingsHandler.TryRead(out settings4))
            {
                Console.WriteLine("Reading nested json file with too few props:" + settingsHandler.FilePath + " to new object (nested)");
                Console.WriteLine("TryRead returned true");
                Console.WriteLine("Object read from JSON file:");
                settings4.Print();
                Console.WriteLine("Note: Some properties may have default values");
            }

            // with nested data: 
            settingsHandler2.FilePath = "usersettings2-less.json";
            Console.WriteLine("-------------------------------------------");
            Settings2 settings4b;
            if (settingsHandler2.TryRead(out settings4b))
            {
                Console.WriteLine("Reading from nested json file with too few props:" + settingsHandler2.FilePath + " to new object (nested)");
                Console.WriteLine("TryRead returned true");
                Console.WriteLine("Object read from JSON file:");
                settings4b.Print();
                Console.WriteLine("Note: Some properties may have default values");
            }

        }
    }
}
