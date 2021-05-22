using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.DTO
{
    class Customer
    {
        private int id;
        private string fName;
        private string lName;
        private string telNum;
        private string address;

        public int Id { get => id; set => id = value; }
        public string FName { get => fName; set => fName = value; }
        public string LName { get => lName; set => lName = value; }
        public string TelNum { get => telNum; set => telNum = value; }
        public string Address { get => address; set => address = value; }
        public Customer()
        {

        }
        public Customer(int id, string fName, string lName, string telNum, string address)
        {
            this.Id = id;
            this.FName = fName;
            this.LName = lName;
            this.TelNum = telNum;
            this.Address = address;
        }

    }
}
