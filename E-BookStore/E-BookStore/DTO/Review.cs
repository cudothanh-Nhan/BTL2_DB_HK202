using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.DTO
{
    class Review
    {
        private int proId;
        private DateTime date;
        private int rating;
        private string imgUrl;
        private string comment_text;
        private DateTime reviewReplyDate;
        private Customer customer;

        public Review()
        {
            customer = new Customer();
        }

        public Review(int proId, DateTime date, int rating, string imgUrl, string comment_text, DateTime reviewReplyDate, Customer customer)
        {
            this.proId = proId;
            this.Date = date;
            this.Rating = rating;
            this.ImgUrl = imgUrl;
            this.Comment_text = comment_text;
            this.ReviewReplyDate = reviewReplyDate;
            this.Customer = customer;
        }
        public int ProId { get => proId; set => proId = value; }
        public DateTime Date { get => date; set => date = value; }
        public int Rating { get => rating; set => rating = value; }
        public string ImgUrl { get => imgUrl; set => imgUrl = value; }
        public string Comment_text { get => comment_text; set => comment_text = value; }
        public DateTime ReviewReplyDate { get => reviewReplyDate; set => reviewReplyDate = value; }
        public Customer Customer { get => customer; set => customer = value; }

    }
}
