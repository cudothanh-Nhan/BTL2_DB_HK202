using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using E_BookStore.BLL;

namespace E_BookStore.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class Login : Window
    {
        LoginBLL bllLogin = new LoginBLL();
        public Login()
        {
            InitializeComponent();
        }
        Registration registration = new Registration();
        Welcome welcome = new Welcome();

        private void PasswordKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                button1_Click(sender, e);
        }
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (!bllLogin.checkEmailLength(textBoxEmail.Text))
            {
                errormessage.Text = "";
                errormessage.Text = "Enter an email!";
                textBoxEmail.Focus();
            }
            else if (!bllLogin.checkEmailStyle(textBoxEmail.Text))
            {
                errormessage.Text = "";
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                if (!bllLogin.checkSelect(textBoxEmail.Text, passwordBox1.Password))
                {
                    errormessage.Text = "Sorry! Please enter existing email/password!";
                }
                else
                {
                    DataSet dataSet = bllLogin.checkInfo(textBoxEmail.Text, passwordBox1.Password);
                    string role = dataSet.Tables[0].Rows[0]["Role"].ToString();
                    string username = "Username is: " + dataSet.Tables[0].Rows[0]["Username"].ToString();
                    var customer_ID = dataSet.Tables[0].Rows[0]["Customer_ID"];
                    //MessageBox.Show(customer_ID.ToString());
                    //welcome.TextBlockCusID.Text = customer_ID;//Sending value from one form to another form.
                    //welcome.TextBlockUsername.Text = username;//Sending value from one form to another form.
                    //welcome.TextBlockRole.Text = role;//Sending value from one form to another form.
                    MainUIWindow main = new MainUIWindow(role, (int)customer_ID);
                    main.Show();
                    Close();
                }                
            }
        }

        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            registration.Show();
            Close();
        }

    }
}
