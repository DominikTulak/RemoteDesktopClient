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
    /// Interakční logika pro Plus.xaml
    /// </summary>
    public partial class Plus : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        public string name;
        public string host;
        public string user;
        public string pass;
        public string port;
        public string port1;
        public string port2;
        public string port3;
    
        public Plus()
        {
            InitializeComponent();
        }
        private void AddClicked(object sender, EventArgs e)
        {
            try
            {
                List<int> AlternativePortsList = new List<int>();
                if (Port1.Text != "") { AlternativePortsList.Add(int.Parse(Port1.Text)); }
                if (Port2.Text != "") { AlternativePortsList.Add(int.Parse(Port2.Text)); }
                if (Port3.Text != "") { AlternativePortsList.Add(int.Parse(Port3.Text)); }
                ConfigEditor.AddItem(Host.Text, int.Parse(Port.Text), User.Text, Pass.Text, Name.Text, AlternativePortsList);
                ConfigEditor.WriteConfigToFile(ConfigEditor.GetFullConfig());
                MessageBox.Show("Úspěšně přidáno!", "Přidat položku", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                this.Close();
            } catch { MessageBox.Show("Chyba v konfigu!", "Přidat položku", MessageBoxButton.OK, MessageBoxImage.Error); }
        }
    }
}
