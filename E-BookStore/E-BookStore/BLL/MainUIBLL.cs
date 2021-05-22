using E_BookStore.DAO;
using E_BookStore.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.BLL
{
    class MainUIBLL
    {
        MainUIDAO dao;

        public MainUIBLL ()
        {
            dao = new MainUIDAO("localhost", 3306, "db", "root", "anhentai");
        }
        public List<Book> getallBookUI()
        {
            return dao.getallBookUI();
        }
        public Book getBookDetail(int bookId)
        {
            return dao.getBookDetail(bookId);
        }
        public List<Magazine> getallMagaUI()
        {
            return dao.getallMagaUI();
        }
        public Magazine getMagaDetail(int magaId)
        {
            return dao.getMagaDetail(magaId);
        }
        public List<Review> getReview(int proId)
        {
            return dao.getReview(proId);
        }
    }
}
