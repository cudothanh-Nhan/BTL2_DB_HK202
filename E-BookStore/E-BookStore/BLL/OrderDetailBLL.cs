using E_BookStore.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.BLL
{
    class OrderDetailBLL
    {
        private OrderDetailDAO dao;
        public OrderDetailBLL()
        {
            dao = new OrderDetailDAO("localhost", 3306, "db", "root", "123456");
        }
        public void updateItemQuantity(int orderId, int productId, int quantity)
        {
            dao.updateItemQuantity(orderId, productId, quantity);
        }
    }
}
