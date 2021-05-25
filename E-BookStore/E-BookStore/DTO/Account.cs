using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.DTO
{
    class Account
    {
        static readonly public string R_CUSTOMER = "Customer";
        static readonly public string R_MANAGER = "Manager";
        private string role;
        private string userName;
        private int customerId;

        public string Role { get => role; set => role = value; }
        public string UserName { get => userName; set => userName = value; }
        public int CustomerId { get => customerId; set => customerId = value; }

        public Account(string role, string userName, int customerId)
        {
            this.role = role;
            this.userName = userName;
            this.CustomerId = customerId;
        }
        public Account()
        {
            this.role = R_CUSTOMER;
            this.UserName = string.Empty;
            this.CustomerId = -1;
        }
    }
}
