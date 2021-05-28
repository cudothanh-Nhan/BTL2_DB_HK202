using E_BookStore.DAO;
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
            dao = new OrderingDAO("localhost", 3306, "db", "root", "123456");
        }
        public List<Order> getOrder(int customerId, string status)
        {
            return dao.getOrder(customerId, status);
        }
        public void updateStatus(int orderId, string status)
        {
            dao.updateStatus(orderId, status);
        }
        public void updateShip(int orderId, Shipment ship)
        {
            dao.updateShip(orderId, ship);
        }
        public List<Shipment> getAllShipment()
        {
            return dao.getAllShipment();
        }
        public int getProductQuantity(int productId)
        {
            return dao.getProductQuantity(productId);
        }
        public void insertCompletedTime(int orderId)
        {
            dao.insertCompletedTime(orderId);
        }
        public List<Customer> getAllCustomer()
        {
            return dao.getAllCustomer();
        }
    }
}
