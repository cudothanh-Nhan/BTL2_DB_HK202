using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login_WPF.DAO
{
    class LoginDAO
    {
        private string connString;

        public LoginDAO(string host, int port, string database, string username, string password)
        {
            this.connString = "Server=" + host + ";Database="
            + database
            + ";port=" + port + ";User Id=" + username + ";password=" +
            password;
        }

        public void InsertDatabse(string firstname, string lastname, string email, string password, string city, string street)
        {
            MySqlConnection con = new MySqlConnection(connString);
            con.Open();
            string Query1 = "Insert into CUSTOMERS (FirstName,Last_Name,Cus_Username,City,Street) values('" + firstname + "','" + lastname + "','" + email + "','" + city + "','" + street + "');";
            string Query = "Insert into ACCOUNTS (Username,Password,Role) values('" + email + "','" + password + "','" + "Customer" + "');";
            MySqlCommand cmd = new MySqlCommand(Query, con);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            MySqlCommand cmd1 = new MySqlCommand(Query1, con);
            cmd1.CommandType = CommandType.Text;
            cmd1.ExecuteNonQuery();
            con.Close();
            
        }
        public MySqlCommand SelectDatabase(string email)
        {
            MySqlConnection con = new MySqlConnection(connString);
            con.Open();
            MySqlCommand cmdEmail = new MySqlCommand("Select * from ACCOUNTS where Username='" + email + "'", con);
            return cmdEmail;
        }
        public MySqlCommand SelectAccount(string email,string password)
        {
            MySqlConnection con = new MySqlConnection(connString);
            con.Open(); 
            MySqlCommand cmd = new MySqlCommand("Select * from ACCOUNTS,CUSTOMERS where Username='" + email + "'  and Password='" + password + "'", con);
            return cmd;
        }

    }
}
