using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.DAO
{
    class ReviewDAO
    {
        private string connString;

        public ReviewDAO(string host, int port, string database, string username, string password)
        {
            this.connString = "Server=" + host + ";Database="
            + database
            + ";port=" + port + ";User Id=" + username + ";password=" +
            password;

        }
        public void insertReview(int productId, string imgUrl, string comment, int rating, int customerId)
        {
            var conn = new MySqlConnection(connString);
            conn.Open();
            string sqlStatement = "call InsertReview(" + productId + ", '"
                                + imgUrl + "', '" + comment + "', " + rating + ", " + customerId + ");";
            var cmd = new MySqlCommand(sqlStatement, conn);
            cmd.ExecuteReader();
        }

    }
}
