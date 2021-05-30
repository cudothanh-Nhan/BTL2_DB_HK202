using E_BookStore.DAO;
using E_BookStore.DTO;
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
            SqlConnectBLL common = new SqlConnectBLL();
            dao = new OrderDetailDAO(common.host, common.port, common.database, common.username, common.password);
        }
        public void updateItemQuantity(Order order, int productId, int quantity)
        {
            dao.updateItemQuantity(order.Id, productId, quantity);
        }
    }
}
