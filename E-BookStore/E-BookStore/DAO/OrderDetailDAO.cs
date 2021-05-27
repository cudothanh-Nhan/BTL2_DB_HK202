using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.DAO
{
    class OrderDetailDAO
    {
        private string connString;
        public OrderDetailDAO(string host, int port, string database, string username, string password)
        {
            this.connString = "Server=" + host + ";Database="
            + database
            + ";port=" + port + ";User Id=" + username + ";password=" +
            password;
        }
        public void updateItemQuantity(int orderId, int productId, int quantity)
        {
            var conn = new MySqlConnection(connString);
            conn.Open();
            string sqlStatement = "call UpdateOrderItemQuantity(" + orderId + ", " + productId + ", " + quantity + ")";
            var cmd = new MySqlCommand(sqlStatement, conn);
            cmd.ExecuteReader();
        }
    }
}
