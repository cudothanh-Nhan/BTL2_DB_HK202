using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace E_BookStore.DAO
{

    class DetailDAO
    {
        private string connString;

        public DetailDAO(string host, int port, string database, string username, string password)
        {
            this.connString = "Server=" + host + ";Database="
            + database
            + ";port=" + port + ";User Id=" + username + ";password=" +
            password;

        }
        public MySqlCommand CheckOrderExist(int Customer_ID)
        {
            MySqlConnection con = new MySqlConnection(connString);
            con.Open();
            MySqlCommand cmdCusID = new MySqlCommand("Select * from ORDERS,Status where Or_cus_ID=" + Customer_ID.ToString() + " and Sta_Order_ID = Order_ID and Status = 'onCart';", con);
            return cmdCusID;
        }
        public MySqlCommand CheckProExist(int P_Product_ID)
        {
            MySqlConnection con = new MySqlConnection(connString);
            con.Open();
            MySqlCommand cmdEmail = new MySqlCommand("Select * from P_PART_OF where P_Product_ID=" + P_Product_ID.ToString() + ";", con);
            return cmdEmail;
        }
        public void UpdateOrder(int P_Product_ID, int Order_quantity)
        {
            MySqlConnection con = new MySqlConnection(connString);
            con.Open();
            string Query1 = "Update P_PART_OF SET Order_quantity =Order_quantity + " + Order_quantity.ToString() + " WHERE P_Product_ID =" + P_Product_ID.ToString() + ";";
            MySqlCommand cmd1 = new MySqlCommand(Query1, con);
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();
            con.Close();

        }
        public void InsertOrder(int P_Product_ID, int Order_quantity, int Or_cus_ID)
        {
            MySqlConnection con = new MySqlConnection(connString);
            con.Open();
            string Query2 = "Insert into ORDERS(Or_cus_ID) values (" + Or_cus_ID.ToString() +
                ");";
            MySqlCommand cmd2 = new MySqlCommand(Query2, con);
            cmd2.CommandType = CommandType.Text;
            cmd2.ExecuteNonQuery();
            int x = this.getOrderID(Or_cus_ID);
            string Query1 = "Insert into P_PART_OF(P_Order_ID,P_Product_ID,Order_quantity) values (" + x.ToString() + ","
                + P_Product_ID.ToString() + "," +
                Order_quantity.ToString() + ");";
            MySqlCommand cmd1 = new MySqlCommand(Query1, con);
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();
            string Query3 = "Insert into STATUS(Sta_Order_ID,Status) values (" +
                x.ToString() +"," + "'onCart'" + ");";
            MySqlCommand cmd3 = new MySqlCommand(Query3, con);
            cmd3.CommandType = CommandType.Text;
            cmd3.ExecuteNonQuery();
            con.Close();
        }
        
        public void InsertPro(int P_Product_ID, int Order_quantity, int Or_cus_ID)
        {
            MySqlConnection con = new MySqlConnection(connString);
            con.Open();
            int x = this.getOrderID(Or_cus_ID);
            string Query1 = "Insert into P_PART_OF(P_Order_ID,P_Product_ID,Order_quantity) values (" +x.ToString()+","+
                P_Product_ID.ToString() + "," +
                Order_quantity.ToString() + ");";
            MySqlCommand cmd1 = new MySqlCommand(Query1, con);
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();
            con.Close();
        }
        private int findIndex(string[] arr, string key)
        {
            return Array.FindIndex<String>(arr, element => element.ToUpper() == key.ToUpper());
        }

        public int getProCate(int Cus_Id)
        {
            var conn = new MySqlConnection(connString);
            int res = 0;
            try
            {
                conn.Open();
                string sqlStatement = "call totalProCate(" + Cus_Id.ToString() + ");";
                var cmd = new MySqlCommand(sqlStatement, conn);
                var reader = cmd.ExecuteReader();

                string[] columnName = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnName[i] = reader.GetName(i);
                }
                while (reader.Read())
                {

                    res = reader.GetInt32(findIndex(columnName, "totall"));

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return res;
        }
        public int getOrderID(int Cus_Id)
        {
            var conn = new MySqlConnection(connString);
            int res = 0;
            try
            {
                conn.Open();
                string sqlStatement = "call OrderIDuse2(" + Cus_Id.ToString() + ");";
                var cmd = new MySqlCommand(sqlStatement, conn);
                var reader = cmd.ExecuteReader();

                string[] columnName = new string[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnName[i] = reader.GetName(i);
                }
                while (reader.Read())
                {

                    res = reader.GetInt32(findIndex(columnName, "Order_ID"));

                }
                conn.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return res;
        }
        
    }
}
