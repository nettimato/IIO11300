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

namespace inventory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            makeFilee();
        }

        private void makeFilee()
        {
            try
            {
                string filu = xdpItems.Source.LocalPath;
                if (System.IO.File.Exists(filu))
                {
                    // nothing
                }
                else {
                    // todo
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // tallennetaan muuttunut tieto XML-tiedostoon
            try
            {
                string filu = xdpItems.Source.LocalPath;
                xdpItems.Document.Save(filu);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            if (lbItems.SelectedIndex > -1)
            {
                // huom textboxis ja listbox bindattu dataan
                lbItems.SelectedIndex = -1;
            }
            else
            {
                // lisätään uusi solmu
                string filu = xdpItems.Source.LocalPath;
                // viittaus XML-Dokumenttiin ja sen juurielementtiin
                XmlDocument doc = xdpItems.Document;
                XmlNode root = doc.SelectSingleNode("/Items");
                // luodaan uusi solmu
                XmlNode newItem = doc.CreateElement("Item");
                XmlAttribute attr = doc.CreateAttribute("Name");
                attr.Value = txtName.Text;
                newItem.Attributes.Append(attr);
                XmlAttribute attr1 = doc.CreateAttribute("Amount");
                attr1.Value = txtAmount.Text;
                newItem.Attributes.Append(attr1);
                XmlAttribute attr2 = doc.CreateAttribute("Description");
                attr2.Value = txtDesc.Text;
                newItem.Attributes.Append(attr2);
                XmlAttribute quest = doc.CreateAttribute("QuestRelated");
                if ((bool)cbQuestItem.IsChecked) quest.Value = "True";
                else quest.Value = "False";
                newItem.Attributes.Append(quest);
                XmlAttribute attr3 = doc.CreateAttribute("Image");
                attr3.Value = txtImage.Text;
                newItem.Attributes.Append(attr3);
                // lisää solmun juureen
                root.AppendChild(newItem);
                // tallennetaan filuun
                xdpItems.Document.Save(filu);
            }
        }
        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            // poistetaan elementti XML-tiedostosta
            try
            {
                string filu = xdpItems.Source.LocalPath;
                XmlDocument doc = xdpItems.Document;
                XmlNode root = doc.SelectSingleNode("/Items");
                XmlNode poistettava = null;
                //Haetaan XPathila poistettava node
                var item = doc.SelectSingleNode(string.Format("/Items/Item[@Name='{0}']", txtName.Text));
                if ((item != null) && (MessageBox.Show("Heitetäänkö " + txtName.Text + " pois?", "Listasta", MessageBoxButton.YesNo) == MessageBoxResult.Yes))
                {
                    poistettava = item;
                }
                if ((bool)cbQuestItem.IsChecked)
                {
                    MessageBox.Show("Tehtäviin liittyviä esineitä ei voi heittää pois!");
                }
                else {
                    if (poistettava != null)
                    {
                        //poistettava noodi juuresta
                        root.RemoveChild(poistettava);
                        xdpItems.Document.Save(filu);
                        //listboxin osoitin
                        lbItems.SelectedIndex = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            // nothing :D
        }
    }
}
