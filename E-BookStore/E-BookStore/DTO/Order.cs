using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.DTO
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
    class ItemOfOrder
    {
        private int id;
        private string name;
        private int uPrice;
        private int quantity;
        private int total;
        private string imgUrl;
        private string type;

        public string Name { get => name; set => name = value; }
        public int UPrice { get => uPrice; set => uPrice = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public int Total { get => total; set => total = value; }
        public string ImgUrl { get => imgUrl; set => imgUrl = value; }
        public string Type { get => type; set => type = value; }
        public int Id { get => id; set => id = value; }
    }
    class Order
    {
        
        static public string S_ON_CART = "OnCart";
        static public string S_SUBMITTED = "Submitted";
        static public string S_PROCESSING = "Processing";
        static public string S_DELIVERING = "Delivering";
        static public string S_COMPLETED = "Completed";
        static public string S_CANCELED = "Canceled";

        private int id;
        private Status status;
        private int shipCash;
        private int total;
        private List<ItemOfOrder> itemsOfOrder;

        public Order()
        {
            itemsOfOrder = new List<ItemOfOrder>();
            this.total = 0;
        }

        public int Id { get => id; set => id = value; }
        public int ShipCash { get => shipCash; set => shipCash = value; }
        public int Total { get => total; set => total = value; }
        public Status Status { get => status; set => status = value; }
        public List<ItemOfOrder> ItemsOfOrder { get => itemsOfOrder; set => itemsOfOrder = value; }
    }
}
