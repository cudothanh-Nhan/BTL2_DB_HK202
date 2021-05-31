using E_BookStore.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.BLL
{
    class ReviewBLL
    {
        private ReviewDAO dao;
        public ReviewBLL()
        {
            SqlConnectBLL common = new SqlConnectBLL();
            dao = new ReviewDAO(common.host, common.port, common.database, common.username, common.password);

        }
        public void insertReview(int productId, string imgUrl, string comment, int rating, int customerId)
        {
            dao.insertReview(productId, imgUrl, comment, rating, customerId);
        }
    }
}
