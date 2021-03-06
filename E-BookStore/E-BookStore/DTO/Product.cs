using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.DTO
{
    abstract class Product
    {
        protected int id;
        protected int price;
        protected int quantiy;
        protected string language;
        private string imgUrl;
        private Store store;

        public Product()
        {
            
        }
        public Product(int id, int price, int quantity, string language, Store store, string imgUrl)
        {
            this.Id = id;
            this.Price = price;
            this.Quantiy = quantity;
            this.Language = language;
            this.store = store;
            this.ImgUrl = imgUrl;
        }
        public int Id { get => id; set => id = value; }
        public int Price { get => price; set => price = value; }
        public int Quantiy { get => quantiy; set => quantiy = value; }
        public string Language { get => language; set => language = value; }
        public Store Store { get => store; set => store = value; }
        public string ImgUrl { get => imgUrl; set => imgUrl = value; }


        public abstract string getClassName();
    }

    class Book : Product
    {
        private string name;
        private string publisher;
        private DateTime publishYear;
        private int nPage;
        private List<Author> authors;

        public string Name { get => name; set => name = value; }
        public string Publisher { get => publisher; set => publisher = value; }
        public DateTime PublishYear { get => publishYear; set => publishYear = value; }
        public int NPage { get => nPage; set => nPage = value; }
        public List<Author> Authors { get => authors; set => authors = value; }
        public Book()
        {
            this.authors = new List<Author>();
        }

        public Book(int id, int price, int quantity, string language, Store store, string imgUrl)
            : base(id, price, quantity, language, store, imgUrl)
        {
            this.Name = this.Publisher = string.Empty;
            this.PublishYear = new DateTime();
            this.nPage = 0;
            this.authors = new List<Author>();
        }

        public override string getClassName()
        {
            return "Book";
        }
    }

    class Magazine : Product
    {
        private MagazineSeri seriName;
        private DateTime publishDate;
        private int no;
        public DateTime PublishDate { get => publishDate; set => publishDate = value; }
        public int No { get => no; set => no = value; }
        public MagazineSeri SeriName { get => seriName; set => seriName = value; }

        public Magazine()
        {

        }

        public Magazine(int id, int price, int quantity, string language, Store store, string imgUrl) 
            : base(id, price, quantity, language, store, imgUrl)
        {
            this.PublishDate = new DateTime();
            this.No = 0;
        }

        public override string getClassName()
        {
            return "Magazine";
        }
    }
    class ProductDisplay
    {
        string name;
        string imgUrl;
        int price;
        int quantity;
        int id;
        string type;
        DateTime date;
        public ProductDisplay()
        {
            this.date = new DateTime();
        }

        public ProductDisplay(string name, string imgUrl, int price, int quantity, int id, string type, DateTime date)
        {
            this.name = name;
            this.imgUrl = imgUrl;
            this.price = price;
            this.quantity = quantity;
            this.id = id;
            this.type = type;
            this.date = date;
        }

        public string Name { get => name; set => name = value; }
        public string ImgUrl { get => imgUrl; set => imgUrl = value; }
        public int Price { get => price; set => price = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public int Id { get => id; set => id = value; }
        public string Type { get => type; set => type = value; }
        public DateTime Date { get => date; set => date = value; }
    }
}
