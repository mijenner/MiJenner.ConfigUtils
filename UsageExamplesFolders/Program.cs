using MiJenner.ConfigUtils;

namespace UsageExamplesFolders
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new DesktopFolderManagerConfigBuilder()
                .WithUserDataPolicy(UserDataPolicy.PolicyFileDocument)
                .WithUserDataMagic("")
                .WithUserConfigPolicy(UserConfigPolicy.PolicyFileDocument)
                .WithUserConfigMagic("")
                .WithCompanyAndAppName("YourCompany", "YourApp")
                .Build();

            var folderManager = new DesktopFolderManager(config);

        }
    }
}