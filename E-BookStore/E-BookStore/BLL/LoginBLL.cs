using Login_WPF.DAO;
using Login_WPFGUI;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Login_WPF.BLL
{
    class LoginBLL
    {
        private LoginDAO dao;
        public LoginBLL()
        {
            dao = new LoginDAO("localhost", 3306, "db", "root", "lekhiettoan1");
        }
        public bool checkEmailLength(string email)
        {
            if (email.Length == 0 || email.Length > 255)
            {
                return false;
            }
            return true;
        }
        public bool checkFirstNameLength(string firstname)
        {
            if (firstname.Length == 0 || firstname.Length > 255)
            {
                return false;
            }
            return true;
        }
        public bool checkLastNameLength(string lastname)
        {
            if (lastname.Length == 0 || lastname.Length > 255)
            {
                return false;
            }
            return true;
        }
        public bool checkCityLength(string city)
        {
            if (city.Length == 0 || city.Length > 255)
            {
                return false;
            }
            return true;
        }
        public bool checkStreetLength(string street)
        {
            if (street.Length == 0 || street.Length > 255)
            {
                return false;
            }
            return true;
        }
        public bool checkEmailStyle(string email)
        {
            if (!Regex.IsMatch(email, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                return false;
            }
            return true;
        }
        public bool checkPassLength(string password)
        {
            if (password.Length == 0 || password.Length >255)
            {
                return false;
            }
            return true;
        }
        public bool checkPassLengthEno(string password)
        {
            if (password.Length < 8)
            {
                return false;
            }
            return true;
        }

        public bool checkConfirmPass(string password, string passwordConfirm)
        {
            if (password != passwordConfirm)
            {
                return false;
            }
            return true;
        }
        public bool checkInsert( string email)
        {
            MySqlCommand cmdEmail =  dao.SelectDatabase(email);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmdEmail;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                    return false;
            }
            return true;
        }


        public bool checkSelect(string email, string password)
        {
            MySqlCommand cmd = dao.SelectAccount(email,password);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count <= 0)
            {
                return false;
            }

            return true;
        }
        public DataSet checkInfo(string email, string password)
        {
            MySqlCommand cmd = dao.SelectAccount(email, password);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmd;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return dataSet;
        }

        public void Insert(string firstname, string lastname, string email, string password,
            string city, string street)
        {
            dao.InsertDatabse(firstname, lastname, email, password, city, street);
        }


    }
}
