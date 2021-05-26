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

                    var conn_author = new MySqlConnection(connString);
                    try
                    {
                        conn_author.Open();
                        string sqlStatement_author = "call getAuthor(" + bookId.ToString() + ");";
                        var cmd_author = new MySqlCommand(sqlStatement_author, conn_author);
                        var reader_author = cmd_author.ExecuteReader();

                        string[] columnName_author = new string[reader_author.FieldCount];
                        for (int i = 0; i < reader_author.FieldCount; i++)
                        {
                            columnName_author[i] = reader_author.GetName(i);
                        }
                        while (reader_author.Read())
                        {
                            int id = reader_author.GetInt32(findIndex(columnName_author, "Author_ID"));
                            string name = reader_author.GetString(findIndex(columnName_author, "Name"));
                            DateTime dob = DateTime.ParseExact(reader_author.GetString(findIndex(columnName_author, "DOB")), "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                            Author author = new Author(id, name, dob);
                            book.Authors.Add(author);

                        }
                        conn_author.Close();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                    }
                }

                Debug.WriteLine(book.Id.ToString() + " " + book.Name + " " +
                    book.ImgUrl + " " + book.Price.ToString() + " " + 
                    book.Quantiy.ToString() + " " + book.Store.Street + " " + book.Store.City + " " +
                    book.Language + " " + book.Publisher + " " + book.PublishYear.Year.ToString() 
                    + " " + book.NPage.ToString());
                foreach (Author i in book.Authors)
                {
                    Debug.WriteLine(i.Name + " " + i.Id.ToString() + " " + i.Dob.Day.ToString() + "/" + i.Dob.Month.ToString() + "/" + i.Dob.Year.ToString());
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return book;
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
                Debug.WriteLine(maga.Id.ToString() + " " + maga.SeriName.Name 
                    + " No." + maga.No.ToString() + " " +
                    maga.ImgUrl + " " + maga.Price.ToString() 
                    + " " + maga.Quantiy.ToString() + " " + maga.Store.Street + " " + maga.Store.City 
                    + " " + maga.Language + " " + maga.SeriName.Publisher + " " + maga.PublishDate.Day.ToString() + "/" + 
                    maga.PublishDate.Month.ToString() + "/" + maga.PublishDate.Year.ToString());
                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return maga;
        }
        public List<Review> getReview(int proId)
        {
            var conn = new MySqlConnection(connString);
            List<Review> reviewList = new List<Review>();
            try
            {
                conn.Open();
                string sqlStatement = "call getReview(" + proId.ToString() + ");";
                var cmd = new MySqlCommand(sqlStatement, conn);
                var reader = cmd.ExecuteReader();
                string[] columnName = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnName[i] = reader.GetName(i);
                }
                while (reader.Read())
                {
                    Review review = new Review();
                    Customer customer = new Customer();
                    customer.FName = reader.GetString(findIndex(columnName, "Firstname"));
                    customer.LName = reader.GetString(findIndex(columnName, "Last_name"));
                    customer.Id = reader.GetInt32(findIndex(columnName, "Customer_ID"));
                    review.Customer = customer;
                    DateTime date = DateTime.ParseExact(reader.GetString(findIndex(columnName, "Date")), "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    review.Date = date;
                    review.Rating = reader.GetInt32(findIndex(columnName, "Rating"));
                    review.ImgUrl = reader.GetString(findIndex(columnName, "Image_URL"));
                    review.Comment_text = reader.GetString(findIndex(columnName, "Comment_text"));
                    DateTime dateReply = DateTime.ParseExact(reader.GetString(findIndex(columnName, "Review_Reply_Date")), "M/d/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                    review.ReviewReplyDate = dateReply;
                    review.ProId = reader.GetInt32(findIndex(columnName, "Re_Pro_ID"));
                    reviewList.Add(review);
                }
                for (int i = 0; i < reviewList.Count; i++)
                {
                    Debug.WriteLine(reviewList[i].Customer.Id.ToString() + " " + reviewList[i].Customer.FName + " " +
                        reviewList[i].Customer.LName + " " + reviewList[i].Date.Day.ToString() + "/" + reviewList[i].Date.Month.ToString()
                        + "/" + reviewList[i].Date.Year.ToString() + " " + reviewList[i].Rating.ToString()
                        + " " + reviewList[i].ImgUrl + " " + reviewList[i].Comment_text + " " + reviewList[i].ReviewReplyDate.Day.ToString()
                        + "/" + reviewList[i].ReviewReplyDate.Month.ToString() + "/" + reviewList[i].ReviewReplyDate.Year.ToString()
                        + " " + reviewList[i].ProId.ToString());
                }
                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return reviewList;
        }
    }
}
