using E_BookStore.DAO;
using E_BookStore.DTO;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace E_BookStore.BLL
{
    class DetailBLL
    {
        private DetailDAO dao;
        public DetailBLL()
        {
            SqlConnectBLL sql = new SqlConnectBLL();
            dao = new DetailDAO(sql.host, sql.port, sql.database, sql.username, sql.password);
        }
        public bool checkQuantity(string quantity)
        {
            int x = 0;
            if (Int32.TryParse(quantity, out x))
            {
                if (x <= 0) return false;
                return true;
            }
            return false;
        }
        public bool CheckOrderExistBLL(int Customer_ID)
        {
            MySqlCommand cmdEmail = dao.CheckOrderExist(Customer_ID);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmdEmail;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        public int UpdatePPartOf(int P_Product_ID, int Order_quantity, int Or_cus_ID)
        {
            MySqlCommand cmdEmail = dao.CheckProExist(P_Product_ID, Or_cus_ID);
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            adapter.SelectCommand = cmdEmail;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                dao.UpdateOrder(P_Product_ID, Order_quantity, Or_cus_ID);
            }
            else
            {
                dao.InsertPro(P_Product_ID, Order_quantity, Or_cus_ID);
            }
            return dao.getProCate(Or_cus_ID);
        }
        public int ShowTotalProCate(int Or_cus_ID)
        {
            return dao.getProCate(Or_cus_ID);
        }
        public void InsertPPartOf(int P_Product_ID, int Order_quantity, int Or_cus_ID)
        {
            dao.InsertOrder(P_Product_ID, Order_quantity, Or_cus_ID);
        }

        public string checkType(int proId)
        {
            List<string> listBook = dao.getAllBookID();
            string id = proId.ToString();
            foreach (var i in listBook)
            {
                if (i == id) return "Book";
            }
            return "Magazine";
        }
        public Book getBookDetail(int proId)
        {
            return dao.getBookDetail(proId);
        }
        public Magazine getMagaDetail(int proId)
        {
            return dao.getMagaDetail(proId);
        }

    }
}