using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.DTO
{
    public class Status
    {
        public string val;
        public string submissionTime;
        public string completedTime;
        public string deliveringTime;
        public string canceledTime;

        public Status()
        {
            this.val = this.submissionTime = this.completedTime = this.deliveringTime = this.canceledTime = string.Empty;
        }
        public Status(Status status)
        {
            
        }
    }
    public class ItemOfOrder : INotifyPropertyChanged
    {
        private int id;
        private string name;
        private int uPrice;
        private int quantity;
        private int maxQuantity;
        private int total;
        private string imgUrl;
        private string type;
        public ItemOfOrder(ItemOfOrder item)
        {
            this.Id = item.Id;
            this.Name = item.Name;
            this.UPrice = item.UPrice;
            this.Quantity = item.Quantity;
            this.Total = item.Total;
            this.ImgUrl = item.ImgUrl;
            this.Type = item.Type;
            this.MaxQuantity = item.MaxQuantity;
        }
        public ItemOfOrder()
        {

        }
        public bool Equals(ItemOfOrder item)
        {
            return this.Quantity == item.Quantity && this.UPrice == item.UPrice;
        }
        public void Copy(ItemOfOrder item)
        {
            this.Quantity = item.Quantity;
            this.UPrice = item.UPrice;
            this.Name = item.Name;
            this.ImgUrl = item.ImgUrl;
            this.MaxQuantity = item.MaxQuantity;
        }
        public string Name { get => name; set => name = value; }
        public int UPrice { get => uPrice; set => uPrice = value; }
        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                this.Total = this.UPrice * this.Quantity;
                OnPropertyChanged("Quantity");
            }
        }
        public int Total { 
            get => total;
            set
            {
                total = value;
                OnPropertyChanged("Total");
            }
        }
        public string ImgUrl { get => imgUrl; set => imgUrl = value; }
        public string Type { get => type; set => type = value; }
        public int Id { get => id; set => id = value; }
        public int MaxQuantity { 
            get => maxQuantity;
            set
            {
                maxQuantity = value;
                OnPropertyChanged("MaxQuantity");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
    }
    public class Order : INotifyPropertyChanged
    {
        
        static readonly public string S_ON_CART = "OnCart";
        static readonly public string S_SUBMITTED = "Submitted";
        static readonly public string S_PROCESSING = "Processing";
        static readonly public string S_DELIVERING = "Delivering";
        static readonly public string S_COMPLETED = "Completed";
        static readonly public string S_CANCELED = "Canceled";
        static readonly public string S_DELETE = "Deleted";

        private int id;
        private Status status;
        private int shipCash;
        private string shipName;
        private int total;
        private List<ItemOfOrder> itemsOfOrder;
        public Order(Order refOrder)
        {
            this.Id = refOrder.Id;
            this.ShipCash = refOrder.ShipCash;
            this.ShipName = refOrder.ShipName;
            this.Total = refOrder.Total;
            this.ItemsOfOrder = new List<ItemOfOrder>();
            foreach (var item in refOrder.ItemsOfOrder)
                this.ItemsOfOrder.Add(new ItemOfOrder(item));
            this.Status = refOrder.Status;
        } 
        public Order()
        {
            itemsOfOrder = new List<ItemOfOrder>();
            this.total = 0;
            this.Status = new Status();
            this.shipCash = 0;
        }
        public bool Equals(Order order)
        {
            if (this.ItemsOfOrder.Count != order.ItemsOfOrder.Count) return false;
            foreach(var item in order.ItemsOfOrder)
            {
                if (this.ItemsOfOrder.FindIndex(element => element.Equals(item)) < 0)
                    return false;
            }
            return true;
        }
        public void Copy(Order order)
        {
            foreach(var item in order.ItemsOfOrder)
            {
                int itemIndex = this.ItemsOfOrder.FindIndex(element => element.Id == item.Id);
                if (itemIndex >= 0)
                    this.ItemsOfOrder[itemIndex].Copy(item);
            }
            this.Total = order.Total;
        }
        public int Id { get => id; set => id = value; }
        public int ShipCash { 
            get => shipCash;
            set
            {
                this.Total = this.Total - shipCash + value;
                shipCash = value;
            }
        }
        public int Total { get => total; set { total = value; OnPropertyChanged("Total"); } }
        public Status Status { get => status; set => status = value; }
        public List<ItemOfOrder> ItemsOfOrder { get => itemsOfOrder; set => itemsOfOrder = value; }
        public string ShipName { get => shipName; set => shipName = value; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
    }
}
