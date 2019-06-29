using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RemoteDesktopClient.Model;

namespace RemoteDesktopClient.View
{
    /// <summary>
    /// Interakční logika pro ConfigEditorGUI.xaml
    /// </summary>
    public partial class ConfigEditorGUI : Window
    {
        List<Config2> conf;
        public ConfigEditorGUI()
        {
            InitializeComponent();
            


            ConfigEditor2.ReadConfig();
            conf = ConfigEditor2.GetFullConfig();
            foreach(Config2 config in conf)
            {
                Model.SettingsContent gc = new SettingsContent();
                gc.name = config.remoteName;
                gc.host = config.remoteHost;
                gc.port = config.remotePort.ToString();
                gc.user = config.remoteUser;
                gc.pass = config.remotePass;
                gc.alternativePorts = "";
                for(int i = 0; i < config.alternativePorts.Count; i++) { gc.alternativePorts += (config.alternativePorts[i] + ", "); }
                //gc.alternativePorts = gc.alternativePorts.Remove(gc.alternativePorts.Length - 1);
                if(gc.alternativePorts.Length > 0)
                {
                    gc.alternativePorts = gc.alternativePorts.Remove(gc.alternativePorts.Length - 2, 2);
                }
                dataGrid.Items.Add(gc);
            }
           
        }

        public void OKClick(object sender, EventArgs e)
        {


            //MessageBox.Show(Jmeno.Text);
            
            try
            {
                List<Config2> newConfig = new List<Config2>();
                
                foreach (SettingsContent gc in dataGrid.Items)
                {
                    var alternativePortsList = new List<int>();
                    foreach (string s in gc.alternativePorts.Replace(" ", "").Split(','))
                    {
                        alternativePortsList.Add(int.Parse(s));
                    }
                    newConfig.Add(new Config2(gc.host, int.Parse(gc.port), gc.user, gc.pass, gc.name, alternativePortsList));

                }
                ConfigEditor2.EditConfig(newConfig);
                ConfigEditor2.WriteConfigToFile(newConfig);
            }
            catch
            {
                MessageBox.Show("Chyba v konifguraci!");
            }
        }
        public void PlusClick(object sender, EventArgs e)
        {

        }


    }
}
