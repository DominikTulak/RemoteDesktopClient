using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace RemoteDesktopClient.Model
{
    static class ConfigEditor
    {
        private static string pathWithEnv = @"%USERPROFILE%\AppData\Local\RemoteServer\client.conf";
        public static Config startConfig()
        {
            if (configExists())
            {
                return loadConfig();
            }
            else
            {
                return createConfig();
            }
        }
        private static bool configExists()
        {
            if (File.Exists(Environment.ExpandEnvironmentVariables(pathWithEnv)))
            {
                //Console.WriteLine("trie");
                return true;

            }
            return false;
        }
        private static Config loadConfig()
        {
            StreamReader sr = new StreamReader(Environment.ExpandEnvironmentVariables(pathWithEnv));
            string[] text = sr.ReadToEnd().Split('\n');
            sr.Close();
            Config cf = new Config();


            for (int line = 0; line < text.Length; line++)
            {
                var txt = text[line].Split('=');
                try { if (txt[0][0] == '#') { continue; } } catch { }
                try
                {
                    //MessageBox.Show(txt[0].Split('.')[0]);
                    switch (txt[0].Split('.')[0])
                    {
                        case "remoteHost":
                            Config.Add(Convert.ToInt32(txt[0].Split('.')[1]), Config.type.host, txt[1].Replace("\r", ""));
                            break;
                        case "remotePort":
                            Config.Add(Convert.ToInt32(txt[0].Split('.')[1]), Config.type.port, Convert.ToUInt32(txt[1]));
                            break;
                        case "remoteUser":
                            Config.Add(Convert.ToInt32(txt[0].Split('.')[1]), Config.type.user, txt[1]);
                            break;
                        case "remotePass":
                            Config.Add(Convert.ToInt32(txt[0].Split('.')[1]), Config.type.password, txt[1]);
                            break;
                        case "remoteName":
                            //MessageBox.Show("lol");
                            Config.Add(Convert.ToInt32(txt[0].Split('.')[1]), Config.type.name, txt[1]);
                            break;
                        case "AlternativePort":
                            Config.Add(Convert.ToInt32(txt[0].Split('.')[1]), Config.type.AlternativePorts, Convert.ToInt32(txt[1]));
                            break;
                    }
                }
                catch
                {
                    //
                }
            }
            //MessageBox.Show(Config.GetUser(1));
            return cf;

        }
        private static Config createConfig()
        {
            if (!Directory.Exists(Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\AppData\Local\RemoteServer")))
            {
                Directory.CreateDirectory(Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\AppData\Local\RemoteServer"));
            }

            StreamWriter sw = new StreamWriter(Environment.ExpandEnvironmentVariables(pathWithEnv));
            sw.WriteLine("#RemoteDesktop Client config file");
            sw.WriteLine("remoteName.0=PC1");
            sw.WriteLine("remoteHost.0=185.28.100.185");
            sw.WriteLine("remotePort.0=30000");
            sw.WriteLine("AlternativePort.0=30001");
            sw.WriteLine("remoteUser.0=Admin");
            sw.WriteLine("remotePass.0=Admin");
            sw.Close();
            return loadConfig();
        }
    }
}
