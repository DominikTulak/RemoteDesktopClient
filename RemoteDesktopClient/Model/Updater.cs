using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using System.Windows;
namespace RemoteDesktopClient.Model
{
    static class Updater
    {
        
        public static void CheckForUpdates(string version)
        {
            try
            {
                using (WebClient wc = new WebClient())
                {
                    var json = wc.DownloadString("http://tulak.host/update.json");
                    dynamic parsed = JsonConvert.DeserializeObject(json);
                    string newVersion = parsed.RemoteDesktopClient.version;
                    if (IsNewer(version, newVersion))
                    {
                        MessageBox.Show(String.Format("Tato verze ({0}) je zastaralá. Aktuální verze je: {1}", version, newVersion));
                    }
                    //MessageBox.Show(newVersion + Updater.IsNewer(version, newVersion) + version);

                }
            } catch { MessageBox.Show("internet vergessen");  }
            
        }
        private static bool IsNewer(string oldVersion, string newVersion)
        {
            while(oldVersion.Split('.').Count() != newVersion.Split('.').Count())
            {
                if(oldVersion.Split('.').Count() > newVersion.Split('.').Count())
                {
                    newVersion += ".0";
                } else
                {
                    oldVersion += ".0";
                }
            }
            for(int i = 0; i < oldVersion.Split('.').Count(); i++)
            {
                if(int.Parse(oldVersion.Split('.')[i]) > int.Parse(newVersion.Split('.')[i]))
                {
                    return false;
                }
                if (int.Parse(oldVersion.Split('.')[i]) < int.Parse(newVersion.Split('.')[i]))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
