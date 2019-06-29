using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RemoteDesktopClient.Model;

namespace RemoteDesktopClient
{
    /// <summary>
    /// Interakční logika pro MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Config conf;
        List<Config2> config;
        List<GridContent> GridConent;
        public MainWindow()
        {
            InitializeComponent();
           // MessageBox.Show(TestPort("8.8.8.8", 3389).ToString());
            

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Process.Start("mstsc", "/v:" + conf.host + ":" + conf.port);
            //System.Diagnostics.Process.Start("mstsc", "/v:" + conf.host + ":" + conf.remotePort);
        }
        public bool TestPort(string host, int port)
        {
            using (System.Net.Sockets.TcpClient tcpClient = new System.Net.Sockets.TcpClient())
            {
                try
                {
                    tcpClient.Connect(host, port);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        private void PripojitClick(object sender, RoutedEventArgs e)
        {
            string host = "";
            int port = 0;
            string user = "";
            string pass = "";

            
            for(int i = 0; i < Config.GetCount(0); i++)
            {
                if(config[i].remoteName == (sender as Button).DataContext)
                {
                    host = config[i].remoteHost;
                    port = config[i].workingPort;
                    user = config[i].remoteUser;
                    pass = config[i].remotePass;
                    //MessageBox.Show(host + "  " + port + "  " + user + "   " + pass + "   " + i);
                }
            }
            try
            {
                String szCmd = "/c cmdkey /generic:" + host + " /user:" + user + " /pass:" + pass + " & mstsc.exe /v " + host+":"+port;
                //MessageBox.Show(szCmd);
                ProcessStartInfo info = new ProcessStartInfo("cmd.exe", szCmd);
                Process proc = new Process();
                proc.StartInfo = info;
                proc.Start();

            }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); }
            


            //Process.Start("cmdkey /add:" + host + " /user:" + user + " /pass:" + pass);
            // Process.Start("mstsc", "/v:" + host + ":" + port);
            //MessageBox.Show((sender as Button).DataContext.ToString());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.Items.Clear();
            GridConent = new List<GridContent>();
            ConfigEditor2.ReadConfig();
            config = ConfigEditor2.GetFullConfig();

            for (int i = 0; i < config.Count; i++)
            {
                GridContent gc = new GridContent();
                gc.popis = config[i].remoteName;
                gc.port = config[i].remotePort;
                gc.stav = config[i].remoteHost;
                gc.color = "red";
                if (TestPort(config[i].remoteHost, config[i].remotePort))
                {
                    gc.port = config[i].remotePort;
                    //gc.stav = "Aktivní";
                    gc.color = "green";
                    config[i].workingPort = config[i].remotePort;
                }
                else
                {
                    for (int j = 0; j < config[i].alternativePorts.Count; j++)
                    {
                        if (TestPort(config[i].remoteHost, config[i].alternativePorts[j]))
                        {
                            gc.port = config[i].alternativePorts[j];
                            //gc.stav = "Aktivní";
                            gc.color = "green";
                            config[i].workingPort = config[i].alternativePorts[j];
                        }
                    }
                }
                
                //gc.port = config[i].remotePort;
                gc.button = (config[i].remoteName);
                dataGrid.Items.Add(gc);
                GridConent.Add(gc);
                //MessageBox.Show(config[0].alternativePorts[0].ToString());
            }

            /* OLD WAY OF CONFIG
            GridConent = new List<GridContent>();
            conf = ConfigEditor.startConfig();
            for (int i = 0; i < Config.GetCount(0); i++)
            {
                GridContent gc = new GridContent();
                gc.popis = Config.GetName(i);
                gc.port = 0;
                if (TestPort(Config.GetHost(i), Config.GetPort(i)))
                {
                    gc.port = Config.GetPort(i);
                }
                else
                {
                    for (int j = 0; j < Config.GetAlternativePorts(i).Count; j++)
                    {
                        if (TestPort(Config.GetHost(i), Config.GetAlternativePorts(i)[j]))
                        {
                            gc.port = Config.GetPort(i);
                        }
                    }
                }
                gc.stav = gc.port == 0 ? "Neaktivní" : "Aktivní";
                gc.color = gc.port == 0 ? "red" : "green";
                gc.port = Config.GetPort(i);
                gc.button = (Config.GetName(i));
                dataGrid.Items.Add(gc);
                GridConent.Add(gc);
            }
            */
            this.Title = "RemoteDesktop Client";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            View.ConfigEditorGUI gui = new View.ConfigEditorGUI();
            gui.Show();
        }

        private void RefreshClicked(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            Window_Loaded(this, new RoutedEventArgs());
            this.Cursor = Cursors.Arrow;
        }
        private void PlusClicked(object sender, EventArgs e)
        {
            View.Plus plus = new View.Plus();
            plus.Show();
        }
        private void MinusClicked(object sender, EventArgs e)
        {
            View.Minus minus = new View.Minus();
            minus.Show();
        }
    }
}
