using System;
using System.Collections.Generic;
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

namespace RemoteDesktopClient.View
{
    /// <summary>
    /// Interakční logika pro Minus.xaml
    /// </summary>
    public partial class Minus : Window
    {
        public Minus()
        {
            InitializeComponent();
        }
        public void RemoveClicked(object sender, RoutedEventArgs e)
        {
            bool odstraneno = Model.ConfigEditor.RemoveByName(Name.Text);
            MessageBox.Show(odstraneno?"Úspěšně odstraněno!":"Prvek nenalezen!", "Odstranit prvek", MessageBoxButton.OK, odstraneno?MessageBoxImage.Information:MessageBoxImage.Error );
            if(odstraneno) Model.ConfigEditor.WriteConfigToFile(Model.ConfigEditor.GetFullConfig());
            this.Close();
        }
    }
}
