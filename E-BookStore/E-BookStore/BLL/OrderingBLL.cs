using E_BookStore.DTO;
using E_BookStore.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.BLL
{
    class OrderingBLL
    {
        private OrderingWindow window;
        private OrderingDAO dao;

        public OrderingBLL(OrderingWindow window)
        {
            this.window = window;
            dao = new OrderingDAO("localhost", 3306, "ebookstore", "root", "123456");
        }
        public Order getOrder(int customerId, string status)
        {
            return dao.getOrder(customerId, status);
        }
    }
}
