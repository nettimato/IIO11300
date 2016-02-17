using System;
using System.Collections.Generic;
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
using System.Xml;

namespace MovieXMLMK2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // tallennetaan muuttunut tieto XML-tiedostoon
            try {
                string filu = xdpMovies.Source.LocalPath;
                xdpMovies.Document.Save(filu);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            if (lbMovies.SelectedIndex > -1)
            {
                // huom textboxis ja listbox bindattu dataan
                lbMovies.SelectedIndex = -1;
            }
            else
            {
                // lisätään uusi solmu
                string filu = xdpMovies.Source.LocalPath;
                // viittaus XML-Dokumenttiin ja sen juurielementtiin
                XmlDocument doc = xdpMovies.Document;
                XmlNode root = doc.SelectSingleNode("/Movies");
                // luodaan uusi solmu
                XmlNode newMovie = doc.CreateElement("Movie");
                XmlAttribute attr = doc.CreateAttribute("Name");
                attr.Value = txtName.Text;
                newMovie.Attributes.Append(attr);
                XmlAttribute attr2 = doc.CreateAttribute("Director");
                attr2.Value = txtDirector.Text;
                newMovie.Attributes.Append(attr2);
                XmlAttribute attr3 = doc.CreateAttribute("Country");
                attr3.Value = txtCountry.Text;
                newMovie.Attributes.Append(attr3);
                // lisää solmun juureen
                root.AppendChild(newMovie);
                // tallennetaan filuun
                xdpMovies.Document.Save(filu);
            }
        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            // poistetaan elementti XML-tiedostosta
        }
    }
}
