using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel; // for ObservableCollection
using System.ComponentModel;
using System.Data.Entity;
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

namespace hardj10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BookShopEntities ctx;
        ObservableCollection<Book> localBooks;
        ICollectionView view; // datagriding filtteröintiä varten
        bool IsBooks; // onko gridissä kirjat vai asiakkaat
        public MainWindow()
        {
            InitializeComponent();
            IniMyStuff();
        }
        private void IniMyStuff() {
            // luodaan konteksti = datasisältö
            ctx = new BookShopEntities();
            // ladataan kirjojen tiedot paikalliseksi
            ctx.Books.Load();
            localBooks = ctx.Books.Local;
            // täytetään combobox kirjailijoitten maiden nimillä
            // huom: lambda-tyylin LINQ-kysely
            cbCountries.DataContext = localBooks.Select(n => n.country).Distinct();
            // luodaan view
            view = CollectionViewSource.GetDefaultView(localBooks);
        }

        private void btnHaeAsiakkaat_Click(object sender, RoutedEventArgs e)
        {
            // haetaan asiakkaat
            // vaihtoehto 1
            dgBooks.DataContext = ctx.Customers.ToList();

            IsBooks = false;
            // mahdollinen filtteröinti pois
            cbCountries.SelectedIndex = -1;
        }

        private void btnUusi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnPoista_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTallenna_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTilaukset_Click(object sender, RoutedEventArgs e)
        {
            // haetaan EDM navigaatio-ominaisuuksien avulla valitun asiakkaan tilaukset ja sen---
            string msg = "";
            Customer current = (Customer)spCustomer.DataContext;
            msg += string.Format("Asiakkaan {0} tilaukset \n", current.lastname);
            foreach (var tilaus in current.Orders) {
                msg += string.Format("Tilaus {0} sisältää {1} tilausriviä\n", tilaus.odate, tilaus.Orderitems.Count);
                foreach (var tilausrivi in tilaus.Orderitems)
                {
                    msg += string.Format("- kirja {0}\n", tilausrivi.Book.name);
                }
            }
            MessageBox.Show(msg);
        }

        private void btnHaeKirjat_Click(object sender, RoutedEventArgs e)
        {
            // vaihtoehto 1
            // dgBooks.DataContext = ctx.Books.ToList();
            // vaihtoehto 2
            dgBooks.DataContext = localBooks;
            IsBooks = true;
        }

        private void dgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // näytetään valitun entiteetin tiedot stackpanelissa
            if (IsBooks)
            {
                spBook.DataContext = dgBooks.SelectedItem;
            }
            else {
                spCustomer.DataContext = dgBooks.SelectedItem;
            }
        }

        private void cbCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // asetetaan filtteri päälle
            view.Filter = MyCountryFilter;
        }
        private bool MyCountryFilter(object item)
        {
            if (cbCountries.SelectedIndex == -1)
            {
                return true;
            }
            else
            {
                return (item as Book).country.Contains(cbCountries.SelectedItem.ToString());
            }
        }
    }
}
