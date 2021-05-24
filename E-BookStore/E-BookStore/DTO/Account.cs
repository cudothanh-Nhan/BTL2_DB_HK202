using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.DAO
{
    class Account
    {
        private string role;
        private string userName;
        private string password;

        public string Role { get => role; set => role = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public Account(string role, string userName, string password)
        {
            this.role = role;
            this.userName = userName;
            this.password = password;
        }
    }
}
