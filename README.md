# MiJenner.ConfigUtils-UserSettings
Offers functionality to store a settings object to/from json files. It includes an implementation using Microsofts Microsoft.Text.Json and another implementation Newtonsofts Newtonsoft.json. 

# Getting started 
## Add using statement 
```cs
using MiJenner.ConfigUtils; 
```

## Create Settings class and object 
First create the class and object you want to hold user settings in the application, e.g.

```cs
public class Settings
{
   public string MyString { get; set; } = "DefaultName";
   public int MyInt { get; set; } = 25;
   public bool MyBool { get; set; } = false;
   public double MyDouble { get; set; } = 3.1425;
}
```

Note: It is important that you define default values for the various settings like in the example above. 

Somewhere else in your code you instantiate an object of above type, e.g.: 
```cs
Settings settings = new Settings() { MyString = "set1", MyInt = 5, MyBool = true, MyDouble = 14.16 };
```

## Create UserSettings handler 
Now create an instance of the UserSettings handler: 

```cs
IUserSettingsHandler<Settings> settingsHandler = new JsonUserSettingsHandlerMS<Settings>("usersettings.json");
```

Note: above file specification will use current directory which you do not want. Use e.g. [link]https://github.com/mijenner/MiJenner.ConfigUtils-FolderManager to determine a proper full path like ```C:\Users\john\AppData\Roaming\YourCompany\AppName\usersettings.json```. 

## Write settings to json file 
When needed you can easily try to write values in settings object to the json file: 

```cs
settingsHandler.TryWrite(settings)
```

Note: TryWrite returns a boolean depending on success or failure. 

## Read settings from json file
In the beginning of loading your application you may want to try to read the settings from a json file and into your application. Here you can use: 

```cs
settingsHandler.TryRead(out settingsb)
```

Note: TryRead returns a boolean depending on success or failure. 

# More advanced solutions
If you are looking for a more advanced solution, with event based triggering etc ..., please consider [https://github.com/anakic/Jot](https://github.com/anakic/Jot) 

# NuGet package 
For ease of use there is a NuGet package available. 
