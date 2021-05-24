﻿using System;
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
using Registration_WPFGUI;
using Welcome_WPFGUI;
using Login_WPF.BLL;

namespace Login_WPFGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class Login : Window
    {
        MainUIBLL bllLogin = new MainUIBLL();
        public Login()
        {
            InitializeComponent();
        }
        Registration registration = new Registration();
        Welcome welcome = new Welcome();

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
                    string role = "Role is: " + dataSet.Tables[0].Rows[0]["Role"].ToString();
                    string username = "Username is: " + dataSet.Tables[0].Rows[0]["Username"].ToString();
                    string customer_ID = "Customer_ID is: "+ dataSet.Tables[0].Rows[0]["Customer_ID"].ToString();
                    welcome.TextBlockCusID.Text = customer_ID;//Sending value from one form to another form.
                    welcome.TextBlockUsername.Text = username;//Sending value from one form to another form.
                    welcome.TextBlockRole.Text = role;//Sending value from one form to another form.
                    welcome.Show();
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