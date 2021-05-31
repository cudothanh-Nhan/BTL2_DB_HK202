using E_BookStore.DAO;
using E_BookStore.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_BookStore.BLL
{
    class InsertAndEditBLL
    {
        InsertAndEditDAO dao;
        public InsertAndEditBLL()
        {
            SqlConnectBLL sql = new SqlConnectBLL();
            dao = new InsertAndEditDAO(sql.host ,sql.port, sql.database, sql.username, sql.password);
        }
        public bool CheckEmpty(string input_string)
        {
            return (input_string.Length == 0);
        }
        public bool CheckIDExist(string id)
        {
            List<string> proID = new List<string>();
            proID = dao.getAllProID();
            foreach (var i in proID)
            {
                if (i == id) return true;
            }
            return false;
        }
        public bool CheckBookIDExist(string id)
        {
            List<string> proID = new List<string>();
            proID = dao.getAllBookID();
            foreach (var i in proID)
            {
                if (i == id) return true;
            }
            return false;
        }
        public bool CheckMagaIDExist(string id)
        {
            List<string> proID = new List<string>();
            proID = dao.getAllMagaID();
            foreach (var i in proID)
            {
                if (i == id) return true;
            }
            return false;
        }
        public bool CheckMagaSeriIDExist(string id)
        {
            List<string> proID = new List<string>();
            proID = dao.getAllMagaSeriID();
            foreach (var i in proID)
            {
                if (i == id) return true;
            }
            return false;
        }

        public bool insertBook(string proID, string imgUrl, string name, string price, string quantity,
            string city, string street, string language, string publisher, string publishYear, string pages)
        {
            return dao.insertBook(proID, imgUrl, name, price, quantity, city, street, language, publisher, publishYear, pages);
        }
        public bool insertMaga(string proID, string imgUrl, string seriNameID, string no, string price, string quantity,
            string city, string street, string language, string publishDate)
        {
            return dao.insertMaga(proID, imgUrl, seriNameID, no, price, quantity, city, street, language, publishDate);
        }

        public bool getBookDetail(string proId, ref Book book)
        {
            int id;
            if (Int32.TryParse(proId, out id))
            {
                book = dao.getBookDetail(id);
                return true;
            }
            return false;
        }
        public bool getMagaDetail(string proId, ref Magazine maga)
        {
            int id;
            if (Int32.TryParse(proId, out id))
            {
                maga = dao.getMagaDetail(id);
                return true;
            }
            return false;
        }
        public bool editBook(string proID, string imgUrl, string name, string price, string quantity,
            string city, string street, string language, string publisher, string publishYear, string pages)
        {
            return dao.editBook(proID, imgUrl, name, price, quantity, city, street, language, publisher, publishYear, pages);
        }
        public bool editMaga(string proID, string imgUrl, string seriNameID, string no, string price, string quantity,
            string city, string street, string language, string publishDate)
        {
            return dao .editMaga(proID, imgUrl, seriNameID, no, price, quantity, city, street, language, publishDate);
        }
        public bool removeBook(string proID)
        {
            return dao.removeBook(proID);
        }
        public bool removeMaga(string proID)
        {
            return dao.removeMaga(proID);
        }
    }
}
