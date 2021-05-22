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
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using E_BookStore.DAO;
using MySql.Data.MySqlClient
namespace E_BookStore.GUI
{
    /// <summary>
    /// Interaction logic for MainUI.xaml
    /// </summary>
    public partial class MainUI : Window
    {
        public MainUI()
        {
            InitializeComponent();
        }
        public getproduct()
        {
            List<Product> product;
            string host = "localhost";
            int port = 3306;
            string database = "db";
            string username = "root";
            string password = "anhentai";
            String connString = "Server=" + host + ";Database="
            + database
            + ";port=" + port + ";User Id=" + username + ";password=" +
            password;
            try
            {
                using (var conn = new MySqlConnection(connString))
                {
                    conn.Open();

                    using (var cmd = new MySqlCommand("SELECT * FROM products", conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {


                            while (reader.Read())
                            {
                                Product p = new Product();
                                Store store;
                                store.street = reader.GetString(0);
                                store.city = reader.GetString(1);
                                p.language = reader.GetString(2);
                                p.price = reader.GetInt32(3);
                                p.quantity = reader.GetInt32(4);
                                p.Id = reader.GetInt32(5);
                                p.store = store;
                                product.Add(p);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error tut cuc");
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e) //Log out
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) //Search
        {

        }
        private void InStock_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Color selectedColor = (Color)(cmbColors.SelectedItem as PropertyInfo).GetValue(null, null);
            this.Background = new SolidColorBrush(selectedColor);
        }
    }

}
