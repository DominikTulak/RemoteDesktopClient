using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
namespace RemoteDesktopClient.Model
{
    class Config2
    {
        public string remoteHost { get; private set; }
        public int remotePort { get; private set; }
        public string remoteUser { get; private set; }
        public string remotePass { get; private set; }
        public string remoteName { get; private set; }
        public int workingPort { get; set; }
        public List<int> alternativePorts { get; private set; }

        public Config2(string remoteHost, int remotePort, string remoteUser, string remotePass, string remoteName, List<int> alternativePorts)
        {
            this.remoteHost = remoteHost;
            this.remotePort = remotePort;
            this.remoteUser = remoteUser;
            this.remotePass = remotePass;
            this.remoteName = remoteName;
            this.alternativePorts = alternativePorts;
            ConfigEditor2.config.Add(this);
        }
    }
}
