using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiJenner.ConfigUtils
{
    public class DesktopFolderManagerConfig
    {
        public UserDataPolicy UserDataPolicy { get; set; } = UserDataPolicy.PolicyFileAppDataLocal;
        public string UserDataMagic { get; set; } = string.Empty;
        public UserConfigPolicy UserConfigPolicy { get; set; }
        public string UserConfigMagic { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string AppName { get; set; } = string.Empty; 
    }
}

