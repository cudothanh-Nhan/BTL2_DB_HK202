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
using System.Windows.Shapes;
using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace E_BookStore.GUI
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class OrderingWindow : Window
    {
        public OrderingWindow()
        {
            InitializeComponent();
            string host = "localhost";
            int port = 3306;
            string database = "ebookstore";
            string username = "root";
            string password = "123456";
            String connString = "Server=" + host + ";Database="
            + database
            + ";port=" + port + ";User Id=" + username + ";password=" +
            password;
            try
            {
                using (var conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = new MySqlCommand("SELECT * FROM books", conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var dName = reader.GetString(0);
                                var dNumber = reader.GetString(1);

                                Debug.WriteLine($"{dName} {dNumber}");
                            }
                        }
                    }
                }


            }
            catch(Exception e)
            {
                Debug.WriteLine("Error tut cuc");
            }
}
    }
}
