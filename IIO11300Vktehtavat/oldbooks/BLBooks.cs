using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oldbooks
{
    [Serializable]
    public class Book
    {
        #region PROPERTIES
        private int id;
        public int Id
        {
            // read only
            get { return id; }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string author;
        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        private string country;
        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        private int year;
        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        #endregion
        #region CONSTRUCTORS
        public Book (int id)
        {
            this.id = id;
        }
        public Book (int id, string name, string author, string country, int year)
        {
            this.id = id;
            this.name = name;
            this.author = author;
            this.country = country;
            this.year = year;
        }
        #endregion
        #region METHODS
        public override string ToString()
        {
            return name + " kirjoittanut " + author;
        }
        #endregion
    }
    public class BookShop
    {
        private static string cs = oldbooks.Properties.Settings.Default.Kirjakauppa;
        public static List<Book> GetTestBooks()
        {
            // metodi palauttaa kokoelman book-olioita
            List<Book> temp = new List<Book>();
            temp.Add(new Book(1, "Sota ja Rauha", "Leo Tolstoi", "Venäjä", 1867));
            temp.Add(new Book(1, "Anna karenina", "Leo Tolstoi", "Venäjä", 1877));
            return temp;
        }
        public static List<Book> GetBooks(Boolean useDB)
        {
            DataTable dt;
            List<Book> temp = new List<Book>();
            if (useDB)
            {
                dt = DBBooks.GetBooks(cs);
            }
            else
            {
                dt = DBBooks.GetTestData();
            }
            // tehdään ORM eli datatablen rivit muutetaan olioiksi
            Book book;
            foreach (DataRow dr in dt.Rows)
            {
                book = new Book((int)dr[0]);
                book.Name = dr["name"].ToString();
                book.Author = dr["author"].ToString();
                book.Country = dr["country"].ToString();
                book.Year = (int)dr["year"];
                // olio lisätään kokoelmaan
                temp.Add(book);
            }
            return temp;
        }
        public static void UpdateBook(Book book)
        {
            try
            {
                int lkm = DBBooks.UpdateBook(cs, book.Id, book.Name, book.Author, book.Country, book.Year);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
