using E_BookStore.DAO;
using E_BookStore.DTO;
using E_BookStore.GUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public List<Order> getOrder(int customerId, string status)
        {
            return dao.getOrder(customerId, status);
        }
        public void updateStatus(Order order, string status)
        {
            string curStatus = dao.getStatus(order.Id);
            if (status == Order.S_SUBMITTED && curStatus != Order.S_ON_CART
                || status == Order.S_PROCESSING && curStatus != Order.S_SUBMITTED
                || status == Order.S_DELIVERING && curStatus != Order.S_PROCESSING
                || status == Order.S_COMPLETED && curStatus != Order.S_DELIVERING)
                throw new Exception("Selected order may be modified. Reloaded");
            if(status == Order.S_CANCELED)
            {
                foreach(ItemOfOrder item in order.ItemsOfOrder)
                {
                    dao.updateProductQuantity(item.Id, -item.Quantity);
                }
            }
            dao.updateStatus(order.Id, status);
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
        public void submitOrder(Order order, Shipment ship)
        {
            foreach(var item in order.ItemsOfOrder)
            {
                if(item.Quantity > getProductQuantity(item.Id))
                {
                    throw new Exception("Quantiy is too large please check again");
                }
            }
            updateShip(order.Id, ship);
            updateStatus(order, Order.S_SUBMITTED);
            order.Status.val = Order.S_SUBMITTED;
            foreach(var item in order.ItemsOfOrder)
            {
                dao.updateProductQuantity(item.Id, item.Quantity);
            }
            Debug.WriteLine("Hey man" + order.Id);
        }
    }
}
