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
            SqlConnectBLL sql = new SqlConnectBLL();
            dao = new OrderingDAO(sql.host, sql.port, sql.database, sql.username, sql.password);
        }
        public Order getOrder(int customerId, string status)
        {
            return dao.getOrder(customerId, status);
        }
    }
}
