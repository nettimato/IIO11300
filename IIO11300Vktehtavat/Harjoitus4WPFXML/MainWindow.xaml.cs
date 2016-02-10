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
using System.Xml.Linq;

namespace Harjoitus4WPFXML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        XElement xe;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGetXML_Click(object sender, RoutedEventArgs e)
        {
            try {
                // ladataan xml-tiedosto ja asetetaan se DataGridin "data context":ksi
                xe = XElement.Load(GetFileName());
                dgData.DataContext = xe.Elements("tyontekija");
                // lasketaan työntekijöiden määrä ja palkkasumma, näytetään tulokset käyttäjälle
                int lkm = xe.Elements().Count();
                tbMessage.Text = string.Format("Akun tehtaalla on {0} työntekijää joista {2} vakituista. Palkat yhteensä {1}", lkm, CalculateSalarySum(), CountWorkers("vakituinen"));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private string GetFileName()
        {
            // älä kovakoodaa muuttuvia asioita koodiin!!
            //return "D:\\H8374\\tyontekijat.xml";
            // parempi tapa sijoittaa ne App.Config 
            return Harjoitus4WPFXML.Properties.Settings.Default.XMLTiedosto;
        }
        private decimal CalculateSalarySum()
        {
            decimal result = 0;
            // haetaan työntekijöiden palkat XML:stä (XElement-olioon) LINQ-kyselyllä
            var palkat = from ele in xe.Elements()
                         select ele.Element("palkka");
            foreach (var item in palkat)
            {
                result += decimal.Parse(item.Value);
            }
            return result;
        }
        private int CountWorkers(string tyosuhde)
        {
            // lasketaan annetun työsuhteen mukaiset työntekijät LINQ-kyselyllä
            var tyontekijat = from ele in xe.Elements()
                              where ele.Element("tyosuhde").Value == tyosuhde
                              select ele.Element("etunimi");
            return tyontekijat.Count();
        }
    }
}
