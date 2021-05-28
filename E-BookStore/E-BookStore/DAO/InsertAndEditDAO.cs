using E_BookStore.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace E_BookStore.DAO
{
    class InsertAndEditDAO
    {
        string connString = "";
        public InsertAndEditDAO(string host, int port, string database, string username, string password)
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
        public List<string> getAllProID()
        {
            var conn = new MySqlConnection(connString);
            List<string> proID = new List<string>();
            try
            {
                conn.Open();
                string sqlStatement = "call getAllProID();";
                var cmd = new MySqlCommand(sqlStatement, conn);
                var reader = cmd.ExecuteReader();

                string[] columnName = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnName[i] = reader.GetName(i);
                }
                while (reader.Read())
                {
                    proID.Add(reader.GetString(findIndex(columnName, "Product_ID")));
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return proID;
        }
        public List<string> getAllBookID()
        {
            var conn = new MySqlConnection(connString);
            List<string> proID = new List<string>();
            try
            {
                conn.Open();
                string sqlStatement = "call getAllBookID();";
                var cmd = new MySqlCommand(sqlStatement, conn);
                var reader = cmd.ExecuteReader();

                string[] columnName = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnName[i] = reader.GetName(i);
                }
                while (reader.Read())
                {
                    proID.Add(reader.GetString(findIndex(columnName, "Book_Pro_ID")));
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return proID;
        }
        public List<string> getAllMagaID()
        {
            var conn = new MySqlConnection(connString);
            List<string> proID = new List<string>();
            try
            {
                conn.Open();
                string sqlStatement = "call getAllMagazineID();";
                var cmd = new MySqlCommand(sqlStatement, conn);
                var reader = cmd.ExecuteReader();

                string[] columnName = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnName[i] = reader.GetName(i);
                }
                while (reader.Read())
                {
                    proID.Add(reader.GetString(findIndex(columnName, "Maga_Pro_ID")));
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return proID;
        }
        public List<string> getAllMagaSeriID()
        {
            var conn = new MySqlConnection(connString);
            List<string> proID = new List<string>();
            try
            {
                conn.Open();
                string sqlStatement = "call getAllMagazineSeriID();";
                var cmd = new MySqlCommand(sqlStatement, conn);
                var reader = cmd.ExecuteReader();

                string[] columnName = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnName[i] = reader.GetName(i);
                }
                while (reader.Read())
                {
                    proID.Add(reader.GetString(findIndex(columnName, "Seri_name_ID")));
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return proID;
        }

        public bool insertBook(string proID, string imgUrl, string name, string price, string quantity, 
            string city, string street, string language, string publisher, string publishYear, string pages)
        {
            MySqlConnection con = new MySqlConnection(connString);
            try
            {
                con.Open();
                string Query1 = "insert into products(Sto_Street, Sto_City, Language, Price, Quantity, Product_ID, imgUrl) values('" + street + "','" + city + "','" + language + "'," + price + "," + quantity + "," + proID + ",'" + imgUrl + "');";
                string Query = "insert into BOOKS(name, publisher, publish_year, pages, book_Pro_ID) values ('" + name + "','" + publisher + "'," + publishYear + "," + pages + "," + proID + ");";
                MySqlCommand cmd = new MySqlCommand(Query, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                MySqlCommand cmd1 = new MySqlCommand(Query1, con);
                cmd1.CommandType = CommandType.Text;
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public bool insertMaga(string proID, string imgUrl, string seriNameID, string no, string price, string quantity,
            string city, string street, string language, string publishDate)
        {
            MySqlConnection con = new MySqlConnection(connString);
            try
            {
                con.Open();
                string Query1 = "insert into products(Sto_Street, Sto_City, Language, Price, Quantity, Product_ID, imgUrl) values('" + street + "','" + city + "','" + language + "'," + price + "," + quantity + "," + proID + ",'" + imgUrl + "');";
                string Query = "insert into magazines(Publish_date, NO, Maga_Pro_ID, Magazine_ID) values (STR_TO_DATE('" + publishDate + "',' %m/%m/%Y')," + no + "," + proID + "," + seriNameID + ");";
                MySqlCommand cmd = new MySqlCommand(Query, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                MySqlCommand cmd1 = new MySqlCommand(Query1, con);
                cmd1.CommandType = CommandType.Text;
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public Book getBookDetail(int bookId)
        {
            var conn = new MySqlConnection(connString);
            Book book = new Book();
            try
            {
                conn.Open();
                string sqlStatement = "call getBookDetail(" + bookId.ToString() + ");";
                var cmd = new MySqlCommand(sqlStatement, conn);
                var reader = cmd.ExecuteReader();

                string[] columnName = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnName[i] = reader.GetName(i);
                }
                while (reader.Read())
                {
                    book.Id = reader.GetInt32(findIndex(columnName, "Product_ID"));
                    book.ImgUrl = reader.GetString(findIndex(columnName, "imgUrl"));
                    book.Name = reader.GetString(findIndex(columnName, "name"));
                    book.Quantiy = reader.GetInt32(findIndex(columnName, "sum(Quantity)"));
                    book.Price = reader.GetInt32(findIndex(columnName, "price"));
                    Store store = new Store();
                    store.Street = reader.GetString(findIndex(columnName, "Sto_Street"));
                    store.City = reader.GetString(findIndex(columnName, "Sto_City"));
                    book.Store = store;
                    book.Language = reader.GetString(findIndex(columnName, "Language"));
                    book.Publisher = reader.GetString(findIndex(columnName, "Publisher"));
                    DateTime date = DateTime.ParseExact(reader.GetString(findIndex(columnName, "Publish_year")), "yyyy", CultureInfo.InvariantCulture);
                    book.PublishYear = date;
                    book.NPage = reader.GetInt32(findIndex(columnName, "Pages"));
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return book;
        }
        public Magazine getMagaDetail(int magaId)
        {
            var conn = new MySqlConnection(connString);
            Magazine maga = new Magazine();
            try
            {
                conn.Open();
                string sqlStatement = "call getMagazineDetail(" + magaId.ToString() + ");";
                var cmd = new MySqlCommand(sqlStatement, conn);
                var reader = cmd.ExecuteReader();

                string[] columnName = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnName[i] = reader.GetName(i);
                }
                while (reader.Read())
                {
                    MagazineSeri seri = new MagazineSeri();
                    maga.Id = reader.GetInt32(findIndex(columnName, "Product_ID"));
                    maga.ImgUrl = reader.GetString(findIndex(columnName, "imgUrl"));
                    seri.Name = reader.GetString(findIndex(columnName, "Name"));
                    seri.Publisher = reader.GetString(findIndex(columnName, "Publisher"));
                    maga.SeriName = seri;
                    maga.No = reader.GetInt32(findIndex(columnName, "NO"));
                    maga.Quantiy = reader.GetInt32(findIndex(columnName, "sum(Quantity)"));
                    maga.Price = reader.GetInt32(findIndex(columnName, "price"));
                    Store store = new Store();
                    store.Street = reader.GetString(findIndex(columnName, "Sto_Street"));
                    store.City = reader.GetString(findIndex(columnName, "Sto_City"));
                    maga.Store = store;
                    maga.Language = reader.GetString(findIndex(columnName, "Language"));
                    DateTime date = DateTime.ParseExact(reader.GetString(findIndex(columnName, "Publish_date")), "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    maga.PublishDate = date;
                }

                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return maga;
        }
        public bool editBook(string proID, string imgUrl, string name, string price, string quantity,
            string city, string street, string language, string publisher, string publishYear, string pages)
        {
            MySqlConnection con = new MySqlConnection(connString);
            try
            {
                con.Open();
                string Query1 = "update products set Sto_Street = '" + street + "', Sto_City ='" + city +"', Language = '" + language + "', Price = " + price + ", imgUrl = '" + imgUrl + "', Quantity = " + quantity + " where Product_ID = " + proID + ";";
                string Query = "update books set Name = '" + name + "', Publisher = '" + publisher + "', Publish_year = " + publishYear + ", Pages = " + pages + " where Book_Pro_ID = " + proID + ";";
                MySqlCommand cmd = new MySqlCommand(Query, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                MySqlCommand cmd1 = new MySqlCommand(Query1, con);
                cmd1.CommandType = CommandType.Text;
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public bool editMaga(string proID, string imgUrl, string seriNameID, string no, string price, string quantity,
            string city, string street, string language, string publishDate)
        {
            MySqlConnection con = new MySqlConnection(connString);
            try
            {
                con.Open();
                string Query1 = "update products set Sto_Street = '" + street + "', Sto_City ='" + city + "', Language = '" + language + "', Price = " + price + ", imgUrl = '" + imgUrl + "', Quantity = " + quantity + " where Product_ID = " + proID + ";";
                string Query = "update magazines set Publish_date = STR_TO_DATE('" + publishDate +"', '%d/%m/%Y'), NO = " + no + " , Magazine_ID = " + seriNameID + " where Maga_Pro_ID = " + proID + " ;";
                MySqlCommand cmd = new MySqlCommand(Query, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                MySqlCommand cmd1 = new MySqlCommand(Query1, con);
                cmd1.CommandType = CommandType.Text;
                cmd1.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public bool removeBook(string proID)
        {
            MySqlConnection con = new MySqlConnection(connString);
            try
            {
                con.Open();
                string Query = "call removeBook(" + proID + ");";
                MySqlCommand cmd = new MySqlCommand(Query, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public bool removeMaga(string proID)
        {
            MySqlConnection con = new MySqlConnection(connString);
            try
            {
                con.Open();
                string Query = "call removeMagazine(" + proID + ");";
                MySqlCommand cmd = new MySqlCommand(Query, con);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
