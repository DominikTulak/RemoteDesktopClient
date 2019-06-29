using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RemoteDesktopClient.Model
{
    class SettingsContent
    {
        public string name { get; set; }
        public string host { get; set; }
        public string port { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
        public string alternativePorts { get; set; }
    }
}
