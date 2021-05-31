using E_BookStore.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace E_BookStore.DAO
{
    class MainUIDAO
    {
        private string connString;

        public MainUIDAO(string host, int port, string database, string username, string password)
        {
            this.connString = "Server=" + host + ";Database="
            + database
            + ";port=" + port + ";User Id=" + username + ";password=" +
            password;

        }
        private int findIndex(string[] arr, string key)
        {
            return Array.FindIndex<String>(arr, element => element.ToUpper() == key.ToUpper());
        }
        public List<Book> getallBookUI()
        {
            var conn = new MySqlConnection(connString);
            List<Book> bookList = new List<Book>();
            try
            {
                conn.Open();    
                string sqlStatement = "call getBookUI();";
                var cmd = new MySqlCommand(sqlStatement, conn);
                var reader = cmd.ExecuteReader();
                
                string[] columnName = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnName[i] = reader.GetName(i);
                }
                while (reader.Read())
                {
                    Book book = new Book();
                    book.Id = reader.GetInt32(findIndex(columnName, "Product_ID"));
                    book.ImgUrl = reader.GetString(findIndex(columnName, "imgUrl"));
                    book.Name = reader.GetString(findIndex(columnName, "name"));
                    book.Quantiy = reader.GetInt32(findIndex(columnName, "sum(Quantity)"));
                    book.Price = reader.GetInt32(findIndex(columnName, "price"));
                    DateTime date = DateTime.ParseExact(reader.GetString(findIndex(columnName, "Publish_year")), "yyyy", CultureInfo.InvariantCulture);
                    book.PublishYear = date;
                    bookList.Add(book);
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return bookList;
        }
        public List<Magazine> getallMagaUI()
        {
            var conn = new MySqlConnection(connString);
            List<Magazine> magaList = new List<Magazine>();
            try
            {
                conn.Open();
                string sqlStatement = "call getMagazineUI();";
                var cmd = new MySqlCommand(sqlStatement, conn);
                var reader = cmd.ExecuteReader();

                string[] columnName = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnName[i] = reader.GetName(i);
                }
                while (reader.Read())
                {
                    Magazine maga = new Magazine();
                    MagazineSeri seri = new MagazineSeri();
                    maga.Id = reader.GetInt32(findIndex(columnName, "Product_ID"));
                    maga.ImgUrl = reader.GetString(findIndex(columnName, "imgUrl"));
                    seri.Name = reader.GetString(findIndex(columnName, "Name"));
                    maga.SeriName = seri;
                    maga.No = reader.GetInt32(findIndex(columnName, "NO"));
                    maga.Quantiy = reader.GetInt32(findIndex(columnName, "sum(Quantity)"));
                    maga.Price = reader.GetInt32(findIndex(columnName, "price"));
                    DateTime date = DateTime.ParseExact(reader.GetString(findIndex(columnName, "Publish_date")), "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    maga.PublishDate = date;
                    magaList.Add(maga);
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return magaList;
        }
    }
}
