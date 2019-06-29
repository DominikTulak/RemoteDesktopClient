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
        List<Config> config;
        List<GridContent> GridConent;
        public MainWindow()
        {
            InitializeComponent();
            

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

            for(int i = 0; i < ConfigEditor.GetFullConfig().Count; i++)
            {
                if(config[i].remoteName == (sender as Button).DataContext)
                {
                    host = config[i].remoteHost;
                    port = config[i].workingPort;
                    user = config[i].remoteUser;
                    pass = config[i].remotePass;
                }
            }
            try
            {
                String szCmd = "/c cmdkey /generic:" + host + " /user:" + user + " /pass:" + pass + " & mstsc.exe /v " + host+":"+port;
                ProcessStartInfo info = new ProcessStartInfo("cmd.exe", szCmd);
                Process proc = new Process();
                proc.StartInfo = info;
                proc.Start();
            }
            catch(Exception ex) { MessageBox.Show(ex.ToString()); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.Items.Clear();
            GridConent = new List<GridContent>();
            ConfigEditor.ReadConfig();
            config = ConfigEditor.GetFullConfig();

            for (int i = 0; i < config.Count; i++)
            {
                GridContent gc = new GridContent();
                gc.popis = config[i].remoteName;
                gc.port = config[i].remotePort;
                gc.stav = "Neaktivní";
                gc.color = "red";
                if (TestPort(config[i].remoteHost, config[i].remotePort))
                {
                    gc.port = config[i].remotePort;
                    gc.color = "green";
                    gc.stav = "Aktivní";
                    config[i].workingPort = config[i].remotePort;
                }
                else
                {
                    for (int j = 0; j < config[i].alternativePorts.Count; j++)
                    {
                        if (TestPort(config[i].remoteHost, config[i].alternativePorts[j]))
                        {
                            gc.port = config[i].alternativePorts[j];
                            gc.color = "green";
                            gc.stav = "Aktivní";
                            config[i].workingPort = config[i].alternativePorts[j];
                        }
                    }
                }
                gc.host = config[i].remoteHost + ":" + config[i].workingPort;
                gc.button = (config[i].remoteName);
                dataGrid.Items.Add(gc);
                GridConent.Add(gc);
            }
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
