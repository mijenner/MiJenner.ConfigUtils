using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiJenner.ConfigUtils
{
    public class DesktopFolderManager
    {
        private DesktopFolderManagerConfig config;
        private Platform platform; 
        public string UserDataFolder { get; set; } = string.Empty;
        public string UserConfigFolder { get; set; } = string.Empty; 

        public DesktopFolderManager(DesktopFolderManagerConfig config)
        {
            this.config = config; 
        }

        public void DoCalcs()
        {
            // determine platform, 
            platform = DetectPlatform.TryDetect(); 



        }

    }
}
