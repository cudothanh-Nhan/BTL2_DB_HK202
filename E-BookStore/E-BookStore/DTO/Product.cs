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
        protected Store Store { get => store; set => store = value; }
        protected string ImgUrl { get => imgUrl; set => imgUrl = value; }

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
        private DateTime publishDate;
        private int no;
        public DateTime PublishDate { get => publishDate; set => publishDate = value; }
        public int No { get => no; set => no = value; }

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
}
