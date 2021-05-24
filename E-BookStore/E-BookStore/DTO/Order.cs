using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.DAO
{
    class Status
    {
        public string val;
        public Nullable<DateTime> submissionTime;
        public Nullable<DateTime> completedTime;
        public Nullable<DateTime> deliveringTime;
        public Nullable<DateTime> canceledTime;
        
        public Status(string val)
        {
            this.val = val;
            this.submissionTime = this.canceledTime = this.completedTime = this.deliveringTime = null;
        }
    }

    class Order
    {
        
        static public string S_ON_CART = "OnCart";
        static public string S_SUBMITTED = "Submitted";
        static public string S_PROCESSING = "Processing";
        static public string S_DELIVERING = "Delivering";
        static public string S_COMPLETED = "Completed";
        static public string S_CANCELLED = "Cancelled";

        private int id;
        private Status status;
        private int shipCash;
        private List<KeyValuePair<Product, int>> orderItems;
        private Customer customer;
        public int Id { get => id; set => id = value; }
        public int ShipCash { get => shipCash; set => shipCash = value; }
        public Status Status { get => status; set => status = value; }
        public List<KeyValuePair<Product, int>> OrderItems { get => orderItems; set => orderItems = value; }
        public Customer Customer { get => customer; set => customer = value; }

        public Order(int id, Status status, int shipCash, List<KeyValuePair<Product, int>> orderItems, Customer customer)
        {
            this.Id = id;
            this.Status = status;
            this.OrderItems = orderItems;
            this.ShipCash = shipCash;
            this.Customer = customer;
        }

    }
}
