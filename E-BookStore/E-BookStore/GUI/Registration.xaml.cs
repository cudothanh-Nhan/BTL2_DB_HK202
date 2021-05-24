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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient;
using System.Windows.Threading;
using System.Threading.Tasks;
using Login_WPFGUI;
using Login_WPF.BLL;

namespace Registration_WPFGUI
{
    /// <summary>  
    /// Interaction logic for Registration.xaml  
    /// </summary>  
    /// 
    
        public partial class Registration : Window
        {
        MainUIBLL bll = new MainUIBLL();
        public Registration()
        {
            InitializeComponent();
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
        public void Reset()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            textBoxCity.Text = "";
            textBoxStreet.Text = "";
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
            successmessage.Text = "";
            errormessage.Text = "";
        }
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (!bll.checkEmailLength(textBoxEmail.Text))
            {
                successmessage.Text = "";
                errormessage.Text = "";
                errormessage.Text = "Enter an email!";
                textBoxEmail.Focus();
            }
            else if (!bll.checkFirstNameLength(textBoxFirstName.Text))
            {
                successmessage.Text = "";
                errormessage.Text = "";
                errormessage.Text = "Enter your first name!";
                textBoxEmail.Focus();
            }
            else if (!bll.checkLastNameLength(textBoxLastName.Text))
            {
                successmessage.Text = "";
                errormessage.Text = "";
                errormessage.Text = "Enter your last name!";
                textBoxEmail.Focus();
            }
            else if (!bll.checkCityLength(textBoxCity.Text))
            {
                successmessage.Text = "";
                errormessage.Text = "";
                errormessage.Text = "Enter your city!";
                textBoxEmail.Focus();
            }
            else if (!bll.checkStreetLength(textBoxStreet.Text))
            {
                successmessage.Text = "";
                errormessage.Text = "";
                errormessage.Text = "Enter your street!";
                textBoxEmail.Focus();
            }

            else if (!bll.checkEmailStyle(textBoxEmail.Text))
            {
                successmessage.Text = "";
                errormessage.Text = "";
                errormessage.Text = "Enter a valid email!";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else if (!bll.checkPassLength(passwordBox1.Password))
            {
                successmessage.Text = "";
                errormessage.Text = "";
                errormessage.Text = "Enter password!";
                passwordBox1.Focus();
            }
            else if (!bll.checkPassLengthEno(passwordBox1.Password))
            {
                successmessage.Text = "";
                errormessage.Text = "";
                errormessage.Text = "Password must be longer than 8 characters!";
                passwordBox1.Password = "";
                passwordBoxConfirm.Password = "";
                passwordBox1.Focus();
            }
            else if (!bll.checkConfirmPass(passwordBox1.Password, passwordBoxConfirm.Password))
            {
                successmessage.Text = "";
                errormessage.Text = "";
                errormessage.Text = "Confirm password must be same as password!";
                passwordBoxConfirm.Focus();
            }
            else if (!bll.checkInsert(textBoxEmail.Text))
            {
                successmessage.Text = "";
                errormessage.Text = "";
                errormessage.Text = "Email exists! Please choose another!";
                textBoxEmail.Focus();
            }
            else
            {
                successmessage.Text = "";
                errormessage.Text = "";
                bll.Insert(textBoxFirstName.Text,textBoxLastName.Text,textBoxEmail.Text, passwordBox1.Password,textBoxCity.Text,textBoxStreet.Text);
                successmessage.Text = "You have registered successfully!";
                }
            }
        }
    }