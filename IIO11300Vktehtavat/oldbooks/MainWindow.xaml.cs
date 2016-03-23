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

namespace oldbooks
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Book> books;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_GetBooks_Click(object sender, RoutedEventArgs e)
        {
            // haetaan testiolioita
            books = BookShop.GetTestBooks();
            dgBooks.DataContext = books;
        }

        private void btn_GetBooksFromSQL_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // haetaan kirjat tietokannasta ORM=muutetaan tietueet Book-olioiksi
                books = BookShop.GetBooks(true);
                dgBooks.DataContext = books;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btn_savebooks_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Book current = (Book)SP_Book.DataContext;
                BookShop.UpdateBook(current);
                MessageBox.Show(string.Format("Kirja {0} päivitetty kantaan onnistuneesti", current.ToString()));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void dgBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SP_Book.DataContext = dgBooks.SelectedItem;
        }
    }
}
