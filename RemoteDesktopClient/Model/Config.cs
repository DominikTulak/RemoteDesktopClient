using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RemoteDesktopClient.Model
{
    class Config
    {
        public enum type { host, user, password, port, AlternativePorts, name, count };
        public static List<Hosts> hosts;
        public Config()
        {
            hosts = new List<Hosts>();
        }
        internal class Hosts
        {
            public string host;
            public int port;
            public List<int> AlternativePorts;
            public string user;
            public string pass;
            public string name;
            public Hosts(string host, int port, List<int> AlternativePorts, string user, string pass, string name)
            {
                this.host = host;
                this.port = port;
                this.AlternativePorts = AlternativePorts;
                this.user = user;
                this.pass = pass;
                this.name = name;
            }
            public static Hosts empty()
            {
                return new Hosts("185.28.100.185", 30000, new List<int>(), "", "", "");
            }
        }
        public static void Add(int id, type typ, object value)
        {
            switch (typ)
            {
                case type.host:
                    if(hosts.Count > id)
                    {
                        hosts[id].host = (value as string);
                    }
                    else
                    {
                        hosts.Add(Hosts.empty());
                        hosts[id].host = (value as string);
                    }
                    break;
                case type.password:
                    if (hosts.Count > id)
                    {
                        hosts[id].pass = (value as string);
                    }
                    else
                    {
                        hosts.Add(Hosts.empty());
                        hosts[id].pass = (value as string);
                    }
                    break;
                case type.port:
                    if (hosts.Count > id)
                    {
                        hosts[id].port = Convert.ToInt32(value);
                    }
                    else
                    {
                        hosts.Add(Hosts.empty());
                        hosts[id].port = Convert.ToInt32(value);
                    }
                    break;
                case type.user:
                    if (hosts.Count > id)
                    {
                        hosts[id].user = (value as string);
                    }
                    else
                    {
                        hosts.Add(Hosts.empty());
                        hosts[id].user = (value as string);
                    }
                    break;
                case type.name:
                    //MessageBox.Show(value.ToString());
                    if (hosts.Count > id)
                    {
                        hosts[id].name = (value as string);
                    }
                    else
                    {
                        hosts.Add(Hosts.empty());
                        hosts[id].name = (value as string);
                    }
                    break;
                case type.AlternativePorts:
                    if (hosts.Count > id)
                    {
                        hosts[id].AlternativePorts.Add(Convert.ToInt32(value));
                    }
                    else
                    {
                        hosts.Add(Hosts.empty());
                        hosts[id].AlternativePorts.Add(Convert.ToInt32(value));
                    }
                    break;
            }
        }
        public static string GetHost(int i) { return hosts[i].host; }
        public static string GetPassword(int i) { return hosts[i].pass; }
        public static int GetPort(int i) { return hosts[i].port; }
        public static string GetName(int i) { return hosts[i].name; }
        public static string GetUser(int i) { return hosts[i].user; }
        public static List<int> GetAlternativePorts(int i) { return hosts[i].AlternativePorts; }
        public static int GetCount(int i) { return hosts.Count; }

        /*
        public static object Get(int id, type typ)
        {
            switch (typ)
            {
                case type.host:
                    return hosts[id].host;
                case type.password:
                    return hosts[id].pass;
                case type.port:
                    return hosts[id].port;
                case type.user:
                    return hosts[id].user;
                case type.name:
                    return hosts[id].name;
                case type.count:
                    return hosts.Count;
                case type.AlternativePorts:
                    return hosts[id].AlternativePorts;
            }
            return "";
        }*/
    }
}
