using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.DTO
{
    class Review
    {
        private string date;
        private int rating;
        private string imgUrl;
        private string comment_text;
        private string username;
        public Review()
        {
            this.ImgUrl = this.Comment_text = this.Username = this.Date = String.Empty;
        }
        public string Date { get => date; set => date = value; }
        public int Rating { get => rating; set => rating = value; }
        public string ImgUrl { get => imgUrl; set => imgUrl = value; }
        public string Comment_text { get => comment_text; set => comment_text = value; }
        public string Username { get => username; set => username = value; }
    }
}
