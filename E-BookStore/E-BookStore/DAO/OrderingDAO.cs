using E_BookStore.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.DAO
{
    class OrderingDAO
    {
        private string connString;

        public OrderingDAO(string host, int port, string database, string username, string password)
        {
            this.connString = "Server=" + host + ";Database="
            + database
            + ";port=" + port + ";User Id=" + username + ";password=" +
            password;
        }
        private int findIndex(string[] arr, string key)
        {
            return Array.FindIndex<String>(arr, element => element.ToUpper() == key.ToUpper());
        }
        private void parseItemInfo(MySqlDataReader itemReader, Order order, ItemOfOrder item)
        {
            while(itemReader.Read())
            {
                string[] itemColumn = new string[itemReader.FieldCount];
                for (int i = 0; i < itemReader.FieldCount; i++)
                {
                    itemColumn[i] = itemReader.GetName(i);
                }
                item.Name = itemReader.GetString(findIndex(itemColumn, "Name"));
                item.UPrice = itemReader.GetInt32(findIndex(itemColumn, "Price"));
                item.Total = item.Quantity * item.UPrice;
                item.ImgUrl = itemReader.GetString(findIndex(itemColumn, "ImgUrl"));
                item.MaxQuantity = getProductQuantity(item.Id);
                //item.Type = "Book";
                order.ItemsOfOrder.Add(item);
                order.Total += item.Total;   
            }
            itemReader.Close();
        }
        private void addItemOfOrder(MySqlDataReader orderReader, Order order, String[] orderColumn)
        {
            try
            {
                ItemOfOrder item = new ItemOfOrder();
                item.Id = orderReader.GetInt32(findIndex(orderColumn, "P_Product_ID"));
                item.Quantity = orderReader.GetInt32(findIndex(orderColumn, "Order_quantity"));
                var conn = new MySqlConnection(connString);
                conn.Open();
                string sqlStatement = "call GetBookInfo("
                            + item.Id.ToString() + ");";
                var cmd = new MySqlCommand(sqlStatement, conn);
                var itemReader = cmd.ExecuteReader();
                parseItemInfo(itemReader, order, item);

                sqlStatement = "call GetMagazineInfo("
                            + item.Id.ToString() + ");";
                cmd = new MySqlCommand(sqlStatement, conn);
                itemReader = cmd.ExecuteReader();
                parseItemInfo(itemReader, order, item);
                conn.Close();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
        public void updateStatus(int orderId, string status)
        {
            var conn = new MySqlConnection(connString);
            try
            {
                conn.Open();
                string sqlStatement = "call UpdateStatus("
                        + orderId + ", \"" + status + "\");";
                var cmd = new MySqlCommand(sqlStatement, conn);
                cmd.ExecuteReader();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
        public void updateShip(int orderId, Shipment ship)
        {
            var conn = new MySqlConnection(connString);
            try
            {
                conn.Open();
                string sqlStatement = "call UpdateShip("
                        + orderId + ", \"" + ship.ShipName + "\", " + ship.ShipVal + ");";
                var cmd = new MySqlCommand(sqlStatement, conn);
                cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
        }
        public List<Order> getOrder(int customerId, string status)
        {
            var conn = new MySqlConnection(connString);
            List<Order> orderList = new List<Order>();
            try
            {
                conn.Open();
                string sqlStatement = "call GetAllOrder("
                        + customerId.ToString() + ", \"" + status + "\");";
                //string sqlStatement = "call GetAllOrderItemId(0, \"OnCart\")";
                var cmd = new MySqlCommand(sqlStatement, conn);
                var reader = cmd.ExecuteReader();
                string[] columnName = new string[reader.FieldCount];
                for(int i = 0; i < reader.FieldCount; i++)
                {
                    columnName[i] = reader.GetName(i); 
                }
                int preOrderId = -1;
                Order order = null;
                while (reader.Read())
                {
                    int orderId = reader.GetInt32(findIndex(columnName, "Order_ID"));
                    if (preOrderId != orderId) {
                        preOrderId = orderId;
                        order = new Order();
                        order.Id = orderId;
                        order.ShipCash = reader.GetInt32(findIndex(columnName, "Ship_cash"));
                        if(!reader.IsDBNull(findIndex(columnName, "Ship_Name")))
                            order.ShipName = reader.GetString(findIndex(columnName, "Ship_Name"));
                        order.Status.val = reader.GetString(findIndex(columnName, "Status"));
                        if (!reader.IsDBNull(findIndex(columnName, "Submission_Time")))
                            order.Status.submissionTime = reader.GetString(findIndex(columnName, "Submission_Time"));
                        if (!reader.IsDBNull(findIndex(columnName, "Delivering_Time")))
                            order.Status.deliveringTime = reader.GetString(findIndex(columnName, "Delivering_Time"));
                        if (!reader.IsDBNull(findIndex(columnName, "Completed_Time")))
                            order.Status.completedTime = reader.GetString(findIndex(columnName, "Completed_Time"));
                        if (!reader.IsDBNull(findIndex(columnName, "Cancelled_Time")))
                            order.Status.canceledTime = reader.GetString(findIndex(columnName, "Cancelled_Time"));
                        orderList.Add(order);
                    }
                    else
                    {
                        order = orderList[orderList.Count - 1];
                    }
                    addItemOfOrder(reader, order, columnName);
                }
                for(int i = 0; i < orderList.Count; i++)
                {
                    Debug.WriteLine("Order " + orderList[i].Id);
                    for(int k = 0; k < orderList[i].ItemsOfOrder.Count; k++)
                    {
                        Debug.WriteLine("Item Name " + orderList[i].ItemsOfOrder[k].Name);
                    }
                }
                conn.Close();
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return orderList;
        }
        public List<Shipment> getAllShipment()
        {
            List<Shipment> shipList = new List<Shipment>();
            try
            {
                var conn = new MySqlConnection(connString);
                conn.Open();
                string sqlStatement = "call GetAllShipment";
                var cmd = new MySqlCommand(sqlStatement, conn);
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    string[] column = new string[reader.FieldCount];
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        column[i] = reader.GetName(i);
                    }
                    Shipment shipment = new Shipment();
                    shipment.ShipName = reader.GetString(findIndex(column, "Name"));
                    shipment.ShipVal = reader.GetInt32(findIndex(column, "Price"));
                    shipList.Add(shipment);
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            return shipList;
        }

        public int getProductQuantity(int productId)
        {
            var conn = new MySqlConnection(connString);
            conn.Open();
            string sqlStatement = "call GetProductQuantity(" + productId + ")";
            var cmd = new MySqlCommand(sqlStatement, conn);
            var reader = cmd.ExecuteReader();
            int quantity = 0;
            while(reader.Read())
            {
                quantity = reader.GetInt32(0);
            }
            return quantity;
        }
    }
}
